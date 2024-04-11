using AgendaContatos.Models;
using AgendaContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Controllers;
public class UsuarioController : Controller
{
	private readonly IUsuarioRepositorio _usuarioRepositorio;

	public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
	{
		_usuarioRepositorio = usuarioRepositorio;
	}

	public IActionResult Index()
	{
		List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

		return View(usuarios);
	}

	public IActionResult Cadastrar()
	{
		return View();
	}

	public IActionResult Editar(int id)
	{
		UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id); 
		return View(usuario);
	}

	public IActionResult DeletarRegistro(int id)
	{
		UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
		return View(usuario);
	}

	public IActionResult Excluir(int id)
	{
		try
		{
			bool excluido = _usuarioRepositorio.Excluir(id);

			if (excluido)
			{
				TempData["MensagemSucesso"] = "Usuário excluído com sucesso!";
			}
			else
			{
				TempData["MensagemErro"] = "Erro ao excluír usuário";
			}

			return RedirectToAction("Index");
		}
		catch (System.Exception erro)
		{
			TempData["MensagemErro"] = $"Erro ao excluir usuário: {erro.Message}";
			return RedirectToAction("Index");
		}
	}

	[HttpPost]
	public IActionResult Cadastrar(UsuarioModel usuario	)
	{
		try
		{
			if (ModelState.IsValid)
			{
				_usuarioRepositorio.Adicionar(usuario);

				TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
				return RedirectToAction("Index");
			}

			return View(usuario);
		}
		catch (System.Exception erro)
		{
			TempData["MensagemErro"] = $"Erro ao cadastraro usuário: {erro.Message}";
			return RedirectToAction("Index");
		}
	}

	[HttpPost]
	public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
	{
		try
		{
			UsuarioModel usuario = null;
			
			if (ModelState.IsValid)
			{
				usuario = new UsuarioModel()
				{
					Id = usuarioSemSenhaModel.Id,
					Nome = usuarioSemSenhaModel.Nome,
					Login = usuarioSemSenhaModel.Login,
					Email = usuarioSemSenhaModel.Email,
					Perfil = usuarioSemSenhaModel.Perfil
				};

				usuario = _usuarioRepositorio.Editar(usuario);
				TempData["MensagemSucesso"] = "Usuario atualizado com sucesso!";

				return RedirectToAction("Index");
			}

			return View(usuario);
		}
		catch (System.Exception erro)
		{
			TempData["MensagemErro"] = $"Erro ao atualizar o usuário: {erro.Message} ";
			throw;
		}
	}
}
