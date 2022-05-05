using System;
using System.Collections.Generic;
using RadicarPolizaAPI.Models;
using RadicarPolizaAPI.Data;
using RadicarPolizaAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace RadicarPolizaAPI.Data;
    public class PolizaProvider: IPoliza
    {
        private PolizaContext _context;

        public PolizaProvider(PolizaContext context){
            _context = context;
        }
        public Poliza GetPolizaByPlacaOrIdPoliza(string SearchKey){
            string sqlgetPoliza = $"EXEC SP_GetPolizaByPlacaOrIdPoliza '{SearchKey}'";
            var polizafounded = _context.Polizas.FromSqlRaw<Poliza>(sqlgetPoliza).AsEnumerable().FirstOrDefault();

            string sqlgetCliente = $"SELECT Cliente.* FROM Cliente Inner Join Polizas ON Polizas.ClienteIdCliente = Cliente.IdCliente where IdPoliza = '{polizafounded.IdPoliza}'";
            var clientefounded = _context.Clientes.FromSqlRaw<Cliente>(sqlgetCliente).AsEnumerable().FirstOrDefault();
            
            string sqlgetAutomotor = $"SELECT Automotor.* FROM Automotor Inner Join Polizas ON Polizas.AutomotorIdAutomotor = Automotor.IdAutomotor where IdPoliza = '{polizafounded.IdPoliza}'";
            var automotorfounded = _context.Automotores.FromSqlRaw<Automotor>(sqlgetAutomotor).AsEnumerable().FirstOrDefault();

            polizafounded.Automotor = automotorfounded;
            polizafounded.Cliente = clientefounded;
            return polizafounded;
        }
        public bool CreatePoliza(Poliza newpoliza){
            if(newpoliza.FechaPoliza < DateTime.Now)
                return false;
            else{
                string sql = @$"EXEC SP_CreatePoliza '{newpoliza.IdPoliza}', '{newpoliza.Cliente.IdCliente}','{newpoliza.Cliente.NombreCliente}',
                                '{newpoliza.Cliente.IdentificacionCliente}','{newpoliza.Cliente.TipoIdentificacionCliente}',
                                '{newpoliza.Cliente.FechaNacimientoCliente}','{newpoliza.FechaPoliza}','{newpoliza.MaximoValorCobertura}',
                                '{newpoliza.PlanPoliza}','{newpoliza.Cliente.CiudadResidenciaCliente}','{newpoliza.Automotor.IdAutomotor}',
                                '{newpoliza.Automotor.PlacaAutomotor}','{newpoliza.Automotor.ModeloAutomotor}',
                                '{newpoliza.Automotor.InspeccionAutomotor}'";
                _context.Database.ExecuteSqlRaw(sql);
                return true;
            }
        }
    }