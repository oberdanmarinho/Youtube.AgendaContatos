using AgendaContatos.Models;

namespace AgendaContatos.Controllers
{
	public interface ISessao
	{
		void CriarSessaoUsuarUsuario(UsuarioModel usuario);
		void RemoverSessaoUsuario();
		UsuarioModel BuscarSessaoUsuario();
	}
}