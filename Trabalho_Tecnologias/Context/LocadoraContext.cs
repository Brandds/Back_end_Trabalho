using Etapa_1.Model;
using Microsoft.EntityFrameworkCore;

namespace Trabalho_Tecnologias.Context
{
    public class LocadoraContext : DbContext
    {
        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
        {
        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // É uma boa prática chamar o método base primeiro.
            base.OnModelCreating(modelBuilder);

            // ---- CONFIGURAÇÃO EXPLÍCITA DAS RELAÇÕES ----

            // Relação entre Veiculo e Fabricante (Um-para-Muitos)
            modelBuilder.Entity<Veiculo>()
                .HasOne(v => v.Fabricante)          // Um Veículo tem um Fabricante.
                .WithMany(f => f.Veiculos)        // Um Fabricante tem muitos Veículos.
                .HasForeignKey(v => v.ID_Fabricante); // A chave estrangeira que liga os dois é a ID_Fabricante.

            // Relação entre Aluguel e Cliente (Um-para-Muitos)
            modelBuilder.Entity<Aluguel>()
                .HasOne(a => a.Cliente)
                .WithMany(c => c.Alugueis)
                .HasForeignKey(a => a.ID_Cliente);

            // Relação entre Aluguel e Veiculo (Um-para-Muitos)
            modelBuilder.Entity<Aluguel>()
                .HasOne(a => a.Veiculo)
                .WithMany(v => v.Alugueis)
                .HasForeignKey(a => a.ID_Veiculo);

        }
    }
}
