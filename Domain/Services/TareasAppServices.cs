using RedisApi.Domain.Entities;
using RedisApi.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace RedisApi.Domain.Services
{
    public class TareasAppServices : ITareasAppServices
    {
        private readonly ITareasRepository _tareasRepository;

        // Constructor de la clase TareasAppServices que recibe una instancia de ITareasRepository
        public TareasAppServices(ITareasRepository tareasRepository)
        {
            // Asigna la instancia recibida al campo _tareasRepository
            _tareasRepository = tareasRepository;
        }

        public Tareas GetById(Guid id)
        {
            // Llama al método GetById del repositorio para obtener una tarea por su identificador único.
            return _tareasRepository.GetById(id);
        }

        public List<Tareas> GetAll()
        {
            // Llama al método GetAll del repositorio para obtener todas las tareas existentes.
            return _tareasRepository.GetAll();
        }

        public void Add(Tareas tarea)
        {
            // Llama al método Add del repositorio para agregar una nueva tarea.
            _tareasRepository.Add(tarea);
        }

        public void Update(Tareas tarea)
        {
            // Llama al método Update del repositorio para actualizar una tarea existente.
            _tareasRepository.Update(tarea);
        }

        public void Delete(Guid id)
        {
            // Llama al método Delete del repositorio para eliminar una tarea por su identificador único.
            _tareasRepository.Delete(id);
        }
    }
}
