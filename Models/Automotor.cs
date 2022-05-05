using System;
using System.ComponentModel.DataAnnotations;

namespace RadicarPolizaAPI.Models;
    public class Automotor{
    [Required]
    [Key]
    public Guid IdAutomotor {get; set;}
    [MaxLength(6)]
    public string PlacaAutomotor {get; set;}= "";
    public string ModeloAutomotor {get; set;}= "";
    public bool InspeccionAutomotor {get; set;}
    }
