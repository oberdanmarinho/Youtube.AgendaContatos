﻿using AgendaContatos.Enums;
using AgendaContatos.Helper;
using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Models;

public class UsuarioModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Digite o nome do usuário")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Digite o login do usuário")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Digite o e-mail do usuãrio")]
    [EmailAddress(ErrorMessage = "O e-mail informádo não é válido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe o perfil do usuário!")]
    public PerfilEnum? Perfil { get; set; }

    [Required(ErrorMessage = "Digite o e-mail do usuário")]
    public string Senha { get; set; }

    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public bool SenhaValida(string senha)
    {
        return Senha == senha.GerarHash();
    }

    public void SetSenhaHash()
    {
        Senha = Senha.GerarHash();
    }

    public string GerarNovaSenha()
    {
        string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
        Senha = novaSenha.GerarHash();
        return novaSenha;
    }
}
