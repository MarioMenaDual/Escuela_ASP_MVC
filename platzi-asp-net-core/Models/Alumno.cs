using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace platzi_asp_net_core.Models
{
    public class Alumno: ObjetoEscuelaBase
    {

       [Display(Prompt="Nombre", Name="Nombre")]
        [Required(ErrorMessage = "Se requiere incluir un nombre")]
        [MinLength(3, ErrorMessage="La longitud mínima de la dirección es 3")]
        public override string Nombre {set;get;}

          public string CursoId{get; set;}
        public Curso Curso{get; set;}
        public List<Evaluacion> Evaluaciones { get; set; }
    }
}