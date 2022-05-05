
using System.ComponentModel.DataAnnotations;

namespace RadicarPolizaAPI.Dtos;
    public record CoberturaDTO{
        [Required]
        public Guid IdCobertura {get; set;}
        public string NombreCobertura {get; set;}
        public decimal ValorCobertura {get; set;}
    }