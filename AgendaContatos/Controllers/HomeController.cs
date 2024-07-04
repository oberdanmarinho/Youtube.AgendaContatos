using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AgendaContatos.Models;
using AgendaContatos.Filters;

namespace AgendaContatos.Controllers;

[PaginaParaUsuarioLogado]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        HomeModel home = new HomeModel();

        home.Nome = "Oberdan Marinho";
        home.Email = "oberdanmarinho@gmail.com"; 
        
        return View(home);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
