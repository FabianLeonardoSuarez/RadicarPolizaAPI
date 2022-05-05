using System;
using System.ComponentModel.DataAnnotations;

namespace RadicarPolizaAPI.Models;
    public class Cobertura{
    [Required]
    [Key]
    public Guid IdCobertura {get; set;}
    
    public string NombreCobertura {get; set;} = "";
    public decimal ValorCobertura {get; set;}
    }