using AgendaContatos.Models;

namespace AgendaContatos.Controllers
{
	public interface ISessao
	{
		void CiarSessaoUsuarUsuario(UsuarioModel usuario);
		void RemoverSessaoUsuario();
		UsuarioModel BuscarSessaoUsuario();
	}
}