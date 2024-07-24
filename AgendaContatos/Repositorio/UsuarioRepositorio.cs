using AgendaContatos.Data;
using AgendaContatos.Models;


namespace AgendaContatos.Repositorio
{
	public class UsuarioRepositorio : IUsuarioRepositorio
	{
		private readonly BancoContent _context;

		public UsuarioRepositorio(BancoContent bancoContent)
		{
			this._context = bancoContent;
		}

		public UsuarioModel BuscarPorLogin(string login)
		{
			return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
		}

        public UsuarioModel BuscarPorEmailLogin(string email, string login)
        {
			return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

		public UsuarioModel ListarPorId(int id)
		{
			return _context.Usuarios.FirstOrDefault(x => x.Id == id);
		}

		public List<UsuarioModel> BuscarTodos()
		{
			return _context.Usuarios.ToList();
		}

		public UsuarioModel Adicionar(UsuarioModel usuario)
		{
			usuario.DataCadastro = DateTime.Now;
			usuario.SetSenhaHash();
			_context.Usuarios.Add(usuario);
			_context.SaveChanges();

			return usuario;
		}

		public UsuarioModel Editar(UsuarioModel usuario)
		{
			UsuarioModel usuarioDB = ListarPorId(usuario.Id);

			if (usuarioDB == null) throw new System.Exception("Houve um erro na atualização do usuário.");

			usuarioDB.Nome = usuario.Nome;
			usuarioDB.Email = usuario.Email;
			usuarioDB.Login = usuario.Login;
			usuarioDB.Perfil = usuario.Perfil;
			usuarioDB.DataAtualizacao = DateTime.Now;

			_context.Usuarios.Update(usuarioDB);
			_context.SaveChanges();

			return usuarioDB;
		}

		public bool Excluir(int id)
		{
			UsuarioModel usuarioDB = ListarPorId(id);

			if (usuarioDB == null) throw new System.Exception("Houve um erro ao deletar o usuário.");

			_context.Usuarios.Remove(usuarioDB);
			_context.SaveChanges();

			return true;
		}

    }
}