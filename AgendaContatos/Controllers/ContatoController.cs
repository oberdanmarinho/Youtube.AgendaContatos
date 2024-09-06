using AgendaContatos.Filters;
using AgendaContatos.Models;
using AgendaContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Controllers
{
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

		private UsuarioModel ObterUsuarioLogado() => _sessao.BuscarSessaoUsuario();

		public IActionResult Index()
		{
			var usuarioLogado = ObterUsuarioLogado();
			var contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
			return View(contatos);
		}

		public IActionResult Cadastrar() => View();

		public IActionResult Editar(int id)
		{
			var contato = _contatoRepositorio.ListarPorId(id);
			return View(contato);
		}

		public IActionResult DeletarRegistro(int id)
		{
			var contato = _contatoRepositorio.ListarPorId(id);
			return View(contato);
		}

		public IActionResult Excluir(int id)
		{
			try
			{
				if (_contatoRepositorio.Excluir(id))
					TempData["MensagemSucesso"] = "Contato excluído com sucesso!";
				else
					TempData["MensagemErro"] = "Erro ao excluir contato";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Erro ao excluir contato: {erro.Message}";
				return RedirectToAction(nameof(Index));
			}
		}

		[HttpPost]
		public IActionResult Cadastrar(ContatoModel contato)
		{
			try
			{
				contato.UsuarioId = ObterUsuarioLogado().Id;

				if (ModelState.IsValid)
				{
					_contatoRepositorio.Adicionar(contato);
					TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
					return RedirectToAction(nameof(Index));
				}

				return View(contato);
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Erro ao cadastrar contato: {erro.Message}";
				return RedirectToAction(nameof(Index));
			}
		}

		[HttpPost]
		public IActionResult Editar(ContatoModel contato)
		{
			try
			{
				if (ModelState.IsValid)
				{
					contato.UsuarioId = ObterUsuarioLogado().Id;
					_contatoRepositorio.Editar(contato);
					TempData["MensagemSucesso"] = "Contato atualizado com sucesso";
					return RedirectToAction(nameof(Index));
				}

				return View(contato);
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Erro ao atualizar contato: {erro.Message}";
				return RedirectToAction(nameof(Index));
			}
		}
	}
}
