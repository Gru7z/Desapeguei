using System.Security.Claims;                 
using Desapeguei.Web.Data;                    
using Desapeguei.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desapeguei.Web.Controllers
{
    [Authorize]
    public class VenderController : Controller
    {
        private readonly ProdutosApiClient _produtosApi;
        private readonly ApplicationDbContext _context;   

        public VenderController(
            ProdutosApiClient produtosApi,
            ApplicationDbContext context)            
        {
            _produtosApi = produtosApi;
            _context = context;                         
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["TituloSite"] = "DESAPEGUEI";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(110_000_000)]
        public async Task<IActionResult> Publicar(
            [FromForm] string Titulo,
            [FromForm] string Descricao,
            [FromForm] string Categoria,
            [FromForm] string Preco,
            [FromForm] IFormFile? Imagem,
            [FromForm] IFormFile? Video)
        {
            // VERIFICA OS CAMPOS OBRIGATÓRIOS

            if (string.IsNullOrWhiteSpace(Titulo) ||
                string.IsNullOrWhiteSpace(Descricao) ||
                string.IsNullOrWhiteSpace(Categoria))
            {
                return BadRequest(new
                {
                    mensagem = "Preencha título, descrição e categoria."
                });
            }

            // CONVERTE O PREÇO

            var precoTexto = Preco
            .Replace("R$", "")
            .Replace(" ", "")
            .Trim();

            if (!decimal.TryParse(
                    precoTexto,
                    System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var preco) ||
                preco < 0.05m)
            {
                return BadRequest(new
                {
                    mensagem = "O preço mínimo para publicar é R$ 0,05"
                });
            }

            // IDENTIFICA O USUÁRIO LOGADO
            var vendedorId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(vendedorId))
            {
                return Unauthorized(new
                {
                    mensagem = "Não foi possível identificar o usuário."
                });
            }

            // NOME DO VENDEDOR (exibido no site/admin)
            var nomeVendedor = await _context.DadosPessoais
                .Where(d => d.UserId == vendedorId)
                .Select(d => d.Nome)
                .FirstOrDefaultAsync() ?? User.Identity?.Name;

            // PUBLICA O PRODUTO

            try
            {
                var produto = await _produtosApi.PublicarAsync(
                    Titulo,
                    Descricao,
                    Categoria,
                    preco,
                    Imagem,
                    Video,
                    vendedorId,
                    nomeVendedor);

                if (produto == null)
                {
                    return StatusCode(502, new
                    {
                        mensagem = "A API não retornou o produto."
                    });
                }

                return Ok(new
                {
                    mensagem = "Produto publicado com sucesso!",
                    id = produto.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(502, new
                {
                    mensagem = ex.Message
                });
            }
        }
    }
}