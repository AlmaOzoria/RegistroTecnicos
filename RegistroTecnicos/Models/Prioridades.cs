﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RegistroTecnicos.Models;

public class Prioridades
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor o igual  que 1 ")]
    public int PrioridadesId { get; set; }

    [Required(ErrorMessage = " La descripcion es obligatoria.")]
    public string Descripcion { get; set; } 

    [Required(ErrorMessage = " El tiempo es obligatoria.")]
    public int Tiempo { get; set; }

    public ICollection<Trabajos> Trabajos { get; set; } = new List<Trabajos>();

}
