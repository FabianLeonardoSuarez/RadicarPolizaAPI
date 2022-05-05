using RadicarPolizaAPI.Models;
using System;

namespace RadicarPolizaAPI.Data;
public static class DBInitializer{
    public static void Initialize(PolizaContext context){
        context.Database.EnsureCreated();
        if(context.Polizas.Any())
        {
            return;
        }

        var newPoliza = new Poliza{
                IdPoliza = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                PlanPoliza = "Poliza Basica",
                Cliente = new Cliente{ IdCliente = Guid.NewGuid()},
                Automotor = new Automotor{ IdAutomotor = Guid.NewGuid()}
        };

        context.Polizas.Add(newPoliza);
        context.SaveChanges();
    }
}