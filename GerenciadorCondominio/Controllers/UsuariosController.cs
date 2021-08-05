using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using GerenciadorCondominio.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsuariosController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registro() 
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModels model, IFormFile foto)
        {
            if (ModelState.IsValid) 
            {
                if (foto != null) 
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create)) 
                    {
                        await foto.CopyToAsync(fileStream);
                        model.Foto = "~/Images/" + nomeFoto;
                    }
                }


                User usuario = new User();
                IdentityResult usuarioCriado;


                if (_userRepository.VerifyRegister() == 0) 
                {
                    usuario.UserName = model.Nome;
                    usuario.CPF = model.CPF;
                    usuario.Email = model.Email;
                    usuario.PhoneNumber = model.Telefone;
                    usuario.Foto = model.Foto;
                    usuario.PrimeiroAcesso = false;
                    usuario.Status = StatusConta.Aprovado;

                    usuarioCriado = await _userRepository.CreateUser(usuario, model.Senha);

                    if (usuarioCriado.Succeeded) 
                    {
                        await _userRepository.IncludeUserInFunction(usuario, "Administrador");
                        await _userRepository.LoginUser(usuario, false);
                        return RedirectToAction("Index", "Usuario");
                    }
                }


                usuario.UserName = model.Nome;
                usuario.CPF = model.CPF;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;
                usuario.Foto = model.Foto;
                usuario.PrimeiroAcesso = true;
                usuario.Status = StatusConta.Analisando;

                usuarioCriado = await _userRepository.CreateUser(usuario, model.Senha);

                if (usuarioCriado.Succeeded)
                {
                    return View("Analise", usuario.UserName);
                }
                else 
                {
                    foreach (IdentityError erro in usuarioCriado.Errors) 
                    {
                        ModelState.AddModelError("", erro.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Analise(string nome) 
        {
            return View(nome);
        }
    }
}
