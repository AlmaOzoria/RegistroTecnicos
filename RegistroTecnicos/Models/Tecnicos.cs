﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]

    [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor o igual que 1.")]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage ="El Campo Nombre es obligatorio")]
    public string? Nombre { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El campo sueldo por hora debe ser mayor que cero.")]
    public double SueldoHora { get; set; }
    public TiposTecnicos? TiposTecnicos { get; set; }

    [ForeignKey("tiposTecnicos")]
    public int TiposTecnicosId { get; set; }


}

