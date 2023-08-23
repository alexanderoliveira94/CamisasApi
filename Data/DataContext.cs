using Microsoft.EntityFrameworkCore;
using CamisasApi.Models.Enuns;
using CamisasApi.Models;

namespace CamisasApi.Data

{
public class DataContext : DbContext
    {   

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    
        public DbSet<Camisas> Camisas {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            modelBuilder.Entity<Camisas>()
            .Property(c => c.Foto)
            .HasMaxLength(255);
            
            modelBuilder.Entity<Camisas>().HasData
            (
            new Camisas() { Id = 1, Nome = "Corinthians", Valor=500, Tamanho="GG",  Classe=ClasseEnum.Boa },
            new Camisas() { Id = 2, Nome = "Palmeiras", Valor=50, Tamanho="P",  Classe=ClasseEnum.Malhada },
            new Camisas() { Id = 3, Nome = "Vasco", Valor=150, Tamanho="G",  Classe=ClasseEnum.Perfeita },
            new Camisas() { Id = 4, Nome = "São Paulo", Valor=250, Tamanho="M",  Classe=ClasseEnum.Boa },
            new Camisas() { Id = 5, Nome = "Santos", Valor=70, Tamanho="GG",  Classe=ClasseEnum.Malhada }
            
            );

        }
    }

}   