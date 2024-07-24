using AgendaContatos.Models;

namespace AgendaContatos.Repositorio
{
	public interface IUsuarioRepositorio
	{
		UsuarioModel BuscarPorLogin(string login);
		UsuarioModel BuscarPorEmailLogin(string email, string login);
		
		List<UsuarioModel> BuscarTodos();
		UsuarioModel ListarPorId(int id);
		UsuarioModel Adicionar(UsuarioModel usuario);
		UsuarioModel Editar(UsuarioModel usuario);
		bool Excluir(int id);
	}
}