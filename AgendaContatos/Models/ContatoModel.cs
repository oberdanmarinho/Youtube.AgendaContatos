using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Informe o nome do contato.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "* Informe o E-mail do contato.")]
        [EmailAddress(ErrorMessage = "Endereço de E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Informe o telefone do contato.")]
        [Phone(ErrorMessage = "Número de telefone inválido")]
        public string Telefone { get; set; }

        public int? UsuarioId { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}