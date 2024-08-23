using AgendaContatos.Filters;
using AgendaContatos.Models;
using AgendaContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Controllers;

[PaginaParaUsuarioLogado]
public class ContatoController : Controller
{
	private readonly IContatoRepositorio _contatoRepositorio;
	private readonly ISessao _sessao;

	public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
	{
		_contatoRepositorio = contatoRepositorio;
		_sessao = sessao;
	}

	public IActionResult Index()
	{
		UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
		List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);

		return View(contatos);
	}

	public IActionResult Cadastrar()
	{
		return View();
	}

	public IActionResult Editar(int id)
	{
		ContatoModel contato = _contatoRepositorio.ListarPorId(id);
		return View(contato);
	}

	public IActionResult DeletarRegistro(int id)
	{
		ContatoModel contato = _contatoRepositorio.ListarPorId(id);
		return View(contato);
	}

	public IActionResult Excluir(int id)
	{
		try
		{
			bool excluido = _contatoRepositorio.Excluir(id);

			if (excluido)
			{
				TempData["MensagemSucesso"] = "Contato excluído com sucesso!";
			}
			else
			{
				TempData["MensagemErro"] = "Erro ao excluír contato";
			}

			return RedirectToAction("Index");
		}
		catch (System.Exception erro)
		{
			TempData["MensagemErro"] = $"Erro ao excluir contato: {erro.Message}";
			return RedirectToAction("Index");
		}
	}

	[HttpPost]
	public IActionResult Cadastrar(ContatoModel contato)
	{
		try
		{
			if (ModelState.IsValid)
			{
				UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
				contato.UsuarioId = usuarioLogado.Id;

				contato = _contatoRepositorio.Adicionar(contato);

				TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
				return RedirectToAction("Index");
			}

			return View(contato);
		}
		catch (System.Exception erro)
		{
			TempData["MensagemErro"] = $"Erro ao cadastraro contato: {erro.Message}";
			return RedirectToAction("Index");
		}
	}

	[HttpPost]
	public IActionResult Editar(ContatoModel contato)
	{
		try
		{
			if (ModelState.IsValid)
			{
				UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
				contato.UsuarioId = usuarioLogado.Id;

				contato = _contatoRepositorio.Editar(contato);
				TempData["MensagemSucesso"] = "Contato atualizado com sucesso";
				return RedirectToAction("Index");
			}

			return View(contato);
		}
		catch (System.Exception erro)
		{
			TempData["MensagemErro"] = $"Erro ao atualizar o contato: {erro.Message} ";
			throw;
		}
	}
}