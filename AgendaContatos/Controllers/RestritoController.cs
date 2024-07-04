using Microsoft.AspNetCore.Mvc;
using AgendaContatos.Filters;

namespace AgendaContatos.Controllers;

[PaginaParaUsuarioLogado]
public class RestritoController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
