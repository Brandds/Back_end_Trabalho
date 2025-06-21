using Etapa_1.Model;

namespace Trabalho_Tecnologias.Context
{
    public static class DataSeeder
    {
        public static void Seed(LocadoraContext context)
        {
            if (context.Fabricantes.Any())
            {
                return; 
            }

            var toyota = new Fabricante { Nome = "Toyota" };
            var honda = new Fabricante { Nome = "Honda" };
            var ford = new Fabricante { Nome = "Ford" };
            context.Fabricantes.AddRange(toyota, honda, ford);
            context.SaveChanges();

            var corolla = new Veiculo { Modelo = "Corolla", Ano = 2022, Quilomentragens = 15000, Fabricante = toyota };
            var civic = new Veiculo { Modelo = "Civic", Ano = 2023, Quilomentragens = 8000, Fabricante = honda };
            var fusion = new Veiculo { Modelo = "Fusion", Ano = 2020, Quilomentragens = 45000, Fabricante = ford };
            context.Veiculos.AddRange(corolla, civic, fusion);

            var joao = new Cliente { Nome = "João Silva", Email = "joao@email.com", Telefone = "3199998888" };
            var maria = new Cliente { Nome = "Maria Oliveira", Email = "maria@email.com", Telefone = "2188887777" };
            context.Clientes.AddRange(joao, maria);

            context.SaveChanges();
        }
    }
}
