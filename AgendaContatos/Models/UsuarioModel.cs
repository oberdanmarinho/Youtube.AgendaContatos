using AgendaContatos.Enums;
using AgendaContatos.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Models
{
	public class UsuarioModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "O nome é obrigatório.")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O login é obrigatório.")]
		public string Login { get; set; }

		[Required(ErrorMessage = "O email é obrigatório.")]
		[EmailAddress(ErrorMessage = "O email não é válido.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Informe o perfil do usuário!")]
		public PerfilEnum? Perfil { get; set; }

		[Required(ErrorMessage = "Digite a senha do usuário")]
		public string Senha { get; set; }

		public DateTime DataCadastro { get; set; }
		public DateTime? DataAtualizacao { get; set; }
		public virtual List<ContatoModel> Contatos { get; set; }

		// Valida se a senha informada é igual à senha atual (criptografada)
		public bool SenhaValida(string senha) => Senha == senha.GerarHash();

		// Define a senha atual com hash
		public void SetSenhaHash() => Senha = Senha.GerarHash();

		// Define uma nova senha e aplica o hash
		public void SetNovaSenha(string novaSenha) => Senha = novaSenha.GerarHash();

		// Gera uma nova senha aleatória e retorna, salvando o hash da nova senha
		public string GerarNovaSenha()
		{
			var novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
			SetNovaSenha(novaSenha);
			return novaSenha;
		}
	}
}
