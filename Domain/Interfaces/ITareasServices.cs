using RedisApi.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RedisApi.Domain.Interfaces
{
    public interface ITareasServices
    {
        // Obtiene una tarea por su identificador único.
        Tareas GetById(Guid id);

        // Agrega una nueva tarea.
        void Add(Tareas tarea);

        // Actualiza una tarea existente.
        void Update(Tareas tarea);

        // Elimina una tarea por su identificador único.
        void Delete(Guid id);

        // Obtiene todas las tareas existentes.
        List<Tareas> GetAll();
    }
}
