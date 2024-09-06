using AgendaContatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaContatos.Data.Map
{
	public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
	{
		public void Configure(EntityTypeBuilder<ContatoModel> builder)
		{
			// Defini��o da chave prim�ria
			builder.HasKey(c => c.Id);

			// Propriedades obrigat�rias
			builder.Property(c => c.Nome)
				   .IsRequired();

			builder.Property(c => c.Email)
				   .IsRequired();

			builder.Property(c => c.Telefone)
				   .IsRequired();

			builder.Property(c => c.UsuarioId)
				   .IsRequired();

			// Configura��o do relacionamento com a entidade Usuario
			builder.HasOne(c => c.Usuario)
				   .WithMany(u => u.Contatos)
				   .HasForeignKey(c => c.UsuarioId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
