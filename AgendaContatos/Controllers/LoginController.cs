using AgendaContatos.Models;
using AgendaContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AgendaContatos.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		private readonly IUsuarioRepositorio _usuarioRepositorio;

		public LoginController(IUsuarioRepositorio usuarioRepositorio)
		{
			_usuarioRepositorio = usuarioRepositorio;
		}

		[HttpPost]
		public IActionResult Entrar(LoginModel loginModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

					if (usuario != null)
					{
						if(usuario.SenhaValida(loginModel.Senha))
						{
							return RedirectToAction("Index", "Home");
						}

						TempData["MensagemErro"] = $"Usuário e/ou senha invalido(s). Por favor, tente novamente.";
					}

					TempData["MensagemErro"] = $"Usuário e/ou senha invalido(s). Por favor, tente novamente.";
				}

				return View("Index");
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Nâo foi possível realizar login. Tente novamente! {erro.Message}";
				return RedirectToAction("Index");
			}
		}
	}
}
