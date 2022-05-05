
using System;
using System.ComponentModel.DataAnnotations;

namespace RadicarPolizaAPI.Models;
    public class Cliente{
    [Required]
    [Key]
    public Guid IdCliente {get; set;}
    public string NombreCliente {get; set;}= "";
    public string IdentificacionCliente {get; set;}= "";
    public TipoIdentificacion TipoIdentificacionCliente {get; set;}
    public DateTime FechaNacimientoCliente {get; set;}
    public string CiudadResidenciaCliente {get; set;}= "";
    public string DireccionResidenciaCliente {get; set;}= "";
    }

    public enum TipoIdentificacion{
        CC = 1,
        TI = 2,
        CE = 3,
        PASS = 4
    }