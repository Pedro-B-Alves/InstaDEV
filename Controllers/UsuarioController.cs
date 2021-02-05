using System;
using System.IO;
using Back_End.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route ("Usuario")]
    public class UsuarioController : Controller
    {

        Usuario usuarioModel = new Usuario();

        [Route ("MostrarUsuario")]
        public IActionResult Index()
        {
            return View();
        }

        

        [Route ("CadastrarUsuario")]
        public IActionResult CadastrarUsuario (IFormCollection form)
        {
            Usuario novoUsuario = new Usuario();

            novoUsuario.IdUsuario = Guid.NewGuid();
            novoUsuario.Email = form["Email"];
            novoUsuario.Nome = form["Nome"];
            novoUsuario.Username = form["Username"];
            novoUsuario.Senha = form["Senha"];
            novoUsuario.DataNascimento = DateTime.Parse(form["DataNascimento"]);
            novoUsuario.Foto = form["Foto"];
            
            if (form.Files.Count > 0)
            {
                
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgCadastro");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgCadastro/", folder, file.FileName);

                using(var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novoUsuario.Foto = file.FileName;
            }else{
                novoUsuario.Foto = "padrao.png";
            }

            if(novoUsuario.Nome != null && novoUsuario.Email != null && novoUsuario.Senha != null && novoUsuario.Username != null)
            {
                usuarioModel.CadastrarUsuario(novoUsuario);
                // ViewBag.Usuario = usuarioModel.MostrarUsuario();
            }else{
                usuarioModel.Mensagem = "Preencha todos os campos!";
            }

            return LocalRedirect("~/Login");

            


        }


    }
}