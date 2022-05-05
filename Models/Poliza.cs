using System;
using System.ComponentModel.DataAnnotations;

namespace RadicarPolizaAPI.Models;
    public class Poliza{
    [Required]
    [Key]
    public Guid IdPoliza {get; set;}
    public string PlanPoliza {get; set;} = "";
    public DateTime FechaPoliza {get; set;}
    public Cliente Cliente {get; set;} = new Cliente();
    public IEnumerable<Cobertura> Coberturas {get; set;} = Enumerable.Empty<Cobertura>();
    public Automotor Automotor {get; set;} = new Automotor();
    public decimal MaximoValorCobertura { get; set; }
    }