using System;

namespace RedisApi.Models
{
    public class TareasModel
    {
        // Propiedad para almacenar el ID de la tarea
        public Guid Id { get; set; }

        // Propiedad para almacenar el nombre de la tarea
        public string NombreTarea { get; set; }

        // Propiedad para almacenar la descripción de la tarea
        public string DescripcionTarea { get; set; }

        // Propiedad para almacenar la fecha de creación de la tarea
        public DateTime FechaCreacion { get; set; } 
    }
}
