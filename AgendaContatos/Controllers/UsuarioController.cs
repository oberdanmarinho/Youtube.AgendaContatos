using AgendaContatos.Filters;
using AgendaContatos.Models;
using AgendaContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Controllers;

[PaginaRestritaSomenteAdmin]
public class UsuarioController : Controller
{
	private readonly IUsuarioRepositorio _usuarioRepositorio;
	private readonly IContatoRepositorio _contatoRepositorio;

	public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IContatoRepositorio contatorepositorio)
	{
		_usuarioRepositorio = usuarioRepositorio;
		_contatoRepositorio = contatorepositorio;
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

	public IActionResult ListarContatosPorUsuarioId(int id)
	{
		List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(id);
		return PartialView("_ContatosUsuario", contatos);
	}

	[HttpPost]
	public IActionResult Cadastrar(UsuarioModel usuario)
	{
		try
		{
			if (ModelState.IsValid)
			{
				usuario = _usuarioRepositorio.Adicionar(usuario);
				TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
				return RedirectToAction("Index");
			}

			// Se o ModelState não for válido, exiba os erros
			var erros = ModelState.Values.SelectMany(v => v.Errors);

			foreach (var erro in erros)
			{
				// Adicione erros ao TempData para exibir na view
				TempData["MensagemErro"] = erro.ErrorMessage;
			}

			return View(usuario);
		}
		catch (System.Exception erro)
		{
			TempData["MensagemErro"] = $"Erro ao cadastrar o usuário: {erro.Message}";
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
