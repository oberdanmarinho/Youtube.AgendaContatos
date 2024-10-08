using AgendaContatos.Data;
using AgendaContatos.Models;
using Microsoft.EntityFrameworkCore;


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
			return _context.Usuarios
				.Include(x => x.Contatos)
				.ToList();
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

		public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
		{
			UsuarioModel usuarioDb = ListarPorId(alterarSenhaModel.Id);

			if (usuarioDb == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado");
			if (!usuarioDb.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere");
			if (usuarioDb.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da atual.");

			usuarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha);
			usuarioDb.DataAtualizacao = DateTime.Now;

			_context.Usuarios.Update(usuarioDb);
			_context.SaveChanges();

			return usuarioDb;
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