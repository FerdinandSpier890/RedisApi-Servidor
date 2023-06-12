using System;

namespace RedisApi.Domain.Entities
{
    public class Tareas
    {
        // Representa el identificador único de la tarea.
        public Guid Id { get; set; }

        // Representa el nombre de la tarea.
        public string NombreTarea { get; set; }

        // Representa la descripción de la tarea.
        public string DescripcionTarea { get; set; }

        // Representa la fecha de creación de la tarea.
        public DateTime FechaCreacion { get; set; }
    }
}
