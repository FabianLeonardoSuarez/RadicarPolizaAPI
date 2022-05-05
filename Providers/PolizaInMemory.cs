using System;
using System.Collections.Generic;
using RadicarPolizaAPI.Models;
using RadicarPolizaAPI.Data;

namespace RadicarPolizaAPI.Data;
    public class PolizaInMemory: IPoliza
    {
        private List<Poliza> _Polizas;
        private PolizaContext _context;

        public PolizaInMemory(){
            _Polizas = new() { new Poliza{
                IdPoliza = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                PlanPoliza = "Poliza Basica",
                Cliente = new Cliente{ IdCliente = Guid.NewGuid()},
                Automotor = new Automotor{ IdAutomotor = Guid.NewGuid()}
            }};
        }
        public Poliza GetPolizaByPlacaOrIdPoliza(string SearchKey){
            return _Polizas.Where(poliza => poliza.IdPoliza.ToString() == SearchKey).FirstOrDefault();
        }
        public bool CreatePoliza(Poliza newpoliza){
            return false;
        }
    }