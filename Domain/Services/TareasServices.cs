using RedisApi.Domain.Entities;
using RedisApi.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace RedisApi.Domain.Services
{
    public class TareasServices : ITareasServices
    {
        private readonly ITareasRepository _tareaRepository;

        // Constructor de la clase TareasServices que recibe una instancia de ITareasRepository
        public TareasServices(ITareasRepository tareaRepository)
        {
            // Asigna la instancia recibida al campo _tareaRepository
            _tareaRepository = tareaRepository;
        }

        // Implementación del método GetById de ITareasServices
        public Tareas GetById(Guid id)
        {
            // Llama al método GetById de la instancia de ITareasRepository
            return _tareaRepository.GetById(id);
        }

        // Implementación del método Add de ITareasServices
        public void Add(Tareas tarea)
        {
            // Establece la fecha de creación de la tarea como la fecha y hora actual
            tarea.FechaCreacion = DateTime.Now;
            // Llama al método Add de la instancia de ITareasRepository
            _tareaRepository.Add(tarea);
        }

        // Implementación del método Update de ITareasServices
        public void Update(Tareas tarea)
        {
            // Llama al método Update de la instancia de ITareasRepository
            _tareaRepository.Update(tarea);
        }

        // Implementación del método Delete de ITareasServices
        public void Delete(Guid id)
        {
            // Llama al método Delete de la instancia de ITareasRepository
            _tareaRepository.Delete(id);
        }

        // Implementación del método GetAll de ITareasServices
        public List<Tareas> GetAll()
        {
            // Llama al método GetAll de la instancia de ITareasRepository
            return _tareaRepository.GetAll();
        }
    }
}
