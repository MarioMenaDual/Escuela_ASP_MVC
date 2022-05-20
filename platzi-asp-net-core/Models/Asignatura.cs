using System;
using System.ComponentModel.DataAnnotations;

namespace platzi_asp_net_core.Models
{
    public class Asignatura:ObjetoEscuelaBase
    {
        [Required(ErrorMessage= "El nombre del curso es requerido")]
        [StringLength(5)]
        public override string Nombre {set;get;}
        public string CursoId{get; set;}
        public Curso Curso{get; set;}

        public List<Evaluacion> Evaluaciones { get; set; }
    }
}