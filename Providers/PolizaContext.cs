using RadicarPolizaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RadicarPolizaAPI.Data;
public class PolizaContext: DbContext{
    public PolizaContext(DbContextOptions<PolizaContext> options): base(options){}

    public DbSet<Poliza> Polizas {get; set;}
    public DbSet<Cliente> Clientes {get; set;}
    public DbSet<Automotor> Automotores {get; set;}
    public DbSet<Cobertura> Coberturas {get; set;}
}