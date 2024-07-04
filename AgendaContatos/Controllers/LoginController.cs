using AgendaContatos.Models;
using AgendaContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Controllers;

public class LoginController : Controller
{
	public IActionResult Index()
	{
		// se o usuãrio estiver logado, redirecionar para a Home
		if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");

		return View();
	}
	private readonly IUsuarioRepositorio _usuarioRepositorio;
	private readonly ISessao _sessao;

	public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
	{
		_usuarioRepositorio = usuarioRepositorio;
		_sessao = sessao;
	}

	public IActionResult Sair()
	{
		_sessao.RemoverSessaoUsuario();

		return RedirectToAction("Index", "Login");
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
					if (usuario.SenhaValida(loginModel.Senha))
					{
						_sessao.CiarSessaoUsuarUsuario(usuario);
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
			TempData["MensagemErro"] = $"Não foi poss�vel realizar login. Tente novamente! {erro.Message}";
			return RedirectToAction("Index");
		}
	}
}
