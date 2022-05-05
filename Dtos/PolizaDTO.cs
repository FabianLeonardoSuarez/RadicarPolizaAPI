using System.ComponentModel.DataAnnotations;

namespace RadicarPolizaAPI.Dtos;
    public record PolizaDTO{
        [Required]
        public Guid IdPoliza {get; set;}
        public string PlanPoliza {get; set;}
        public DateTime FechaPoliza {get; set;}
        public Guid IdCliente {get; set;}
        public string NombreCliente {get; set;}
        public string IdentificacionCliente {get; set;}
        public TipoIdentificacion TipoIdentificacionCliente {get; set;}
        public DateTime FechaNacimientoCliente {get; set;}
        public string CiudadResidenciaCliente {get; set;}
        public string DireccionResidenciaCliente {get; set;}
        public Guid IdAutomotor {get; set;}
        public string PlacaAutomotor {get; set;}
        public string ModeloAutomotor {get; set;}
        public bool InspeccionAutomotor {get; set;}
        public IEnumerable<CoberturaDTO> Coberturas {get; set;}
        public decimal MaximoValorCobertura { get; set; }
    }
    public enum TipoIdentificacion{
        CC = 1,
        TI = 2,
        CE = 3,
        PASS = 4
    }
