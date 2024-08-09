using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Models;

public class AlterarSenhaModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Digite a senha atual do usuaário.")]
    public string SenhaAtual { get; set; }

    [Required(ErrorMessage = "Digite a nova senha do usuaário.")]
    public string NovaSenha { get; set; }
    
    [Required(ErrorMessage = "Confirme a nova senha do usuaário.")]
    [Compare("NovaSenha", ErrorMessage ="A senha não confere com a nova senha.")]
    public string ConfirmarNovaSenha { get; set; }
}
