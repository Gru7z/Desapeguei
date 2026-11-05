
using Desapeguei.Web.Models;
using Desapeguei.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Desapeguei.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class MinhasVendasModel : PageModel
    {
        private readonly ProdutosApiClient _produtosApi;
        private readonly IConfiguration _configuration;

        public MinhasVendasModel(
            ProdutosApiClient produtosApi,
            IConfiguration configuration)
        {
            _produtosApi = produtosApi;
            _configuration = configuration;
        }

        public List<Produto> Produtos { get; set; } = new();

        public async Task OnGetAsync()
        {
            var vendedorId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(vendedorId))
                return;

            Produtos = await _produtosApi.ListarMinhasVendasAsync(
                vendedorId);
        }

        public string? ObterImagemUrl(string? imagemUrl)
        {
            if (string.IsNullOrWhiteSpace(imagemUrl))
                return null;

            if (Uri.TryCreate(
                imagemUrl,
                UriKind.Absolute,
                out _))
            {
                return imagemUrl;
            }

            var apiBaseUrl = _configuration["ApiBaseUrl"];

            if (string.IsNullOrWhiteSpace(apiBaseUrl))
                return imagemUrl;

            return $"{apiBaseUrl.TrimEnd('/')}/{imagemUrl.TrimStart('/')}";
        }
    }
}
