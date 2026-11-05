// Models\DadosPessoais.cs
using System.ComponentModel.DataAnnotations;

namespace Desapeguei.Web.Models
{
    public class DadosPessoais
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } = "";

        public string Nome { get; set; } = "";

        public string Telefone { get; set; } = "";

        public string Endereco { get; set; } = "";
    } 
}

/*

< div class= "perfil-menu" >

    < a asp - page = "./Index" class= "perfil-menu-link" >
        Perfil
    </ a >

    < a asp - page = "./Email" class= "perfil-menu-link" >
        E - mail
    </ a >

    < a asp - page = "./Senha" class= "perfil-menu-link" >
        Senha
    </ a >

    < a asp - page = "./DadosPessoais" class= "perfil-menu-link ativo" >
        Dados pessoais
    </ a >

    < a asp - page = "./MinhasVendas" class= "perfil-menu-link" >
        Minhas vendas
    </ a >

</ div >*/