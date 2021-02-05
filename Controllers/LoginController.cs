using System.Collections.Generic;
using                           ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End
{
    public class LoginController : Controller
    {
        Usuario usuarioModel = new Usuario();
        public const string PATH = "Database/Usuarios.csv";
        public IActionResult Logar(IFormCollection form)
        {
            List<string> csv = usuarioModel.ReadAllLinesCSV(PATH);

            var logado =
            csv.Find(
                x =>
                x.Split(";")[4] == form["Email"] || x.Split(";")[5] == form["Username"] &&
                x.Split(";")[6] == form["Senha"]
            );

            if (logado != null)
            {
                HttpContext.Session.SetString("IdLogado", logado.Split(";")[0]);
                ViewBag.logado = HttpContext.Session.GetString("IdLogado");
                return LocalRedirect("~/");
            }

            usuarioModel.Mensagem = "Dados incorretos, tente novamente...";
            return LocalRedirect("~/Login");
        }

        public List<Usuario> UsuariosLogados(){
            List<Usuario> usuarioslogados = ViewBag.logado;
            
            return usuarioslogados;
        }

        public IActionResult Index()
        {
            return View();
        }

    }

    



}