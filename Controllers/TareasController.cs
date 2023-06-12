using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisApi.Domain.Entities;
using RedisApi.Domain.Interfaces;
using RedisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareasAppServices _tareaAppService;

        public TareasController(ITareasAppServices tareaAppService)
        {
            _tareaAppService = tareaAppService;
        }

        [HttpGet("{id}")]
        public ActionResult<TareasModel> GetById(Guid id)
        {
            // Obtener la tarea por ID utilizando el servicio de aplicación
            Tareas tarea = _tareaAppService.GetById(id);
            if (tarea == null)
            {
                return NotFound();
            }
            // Crear un modelo de tarea a partir de la entidad encontrada
            TareasModel tareaModel = new TareasModel
            {
                Id = tarea.Id,
                NombreTarea = tarea.NombreTarea,
                DescripcionTarea = tarea.DescripcionTarea,
                FechaCreacion = tarea.FechaCreacion
            };
            return Ok(tareaModel);
        }

        [HttpPost]
        public IActionResult Add(TareasModel tareaModel)
        {
            // Crear una nueva tarea a partir del modelo recibido
            Tareas tarea = new Tareas
            {
                Id = tareaModel.Id,
                NombreTarea = tareaModel.NombreTarea,
                DescripcionTarea = tareaModel.DescripcionTarea,
                FechaCreacion = tareaModel.FechaCreacion
            };
            // Agregar la tarea utilizando el servicio de aplicación
            _tareaAppService.Add(tarea);
            // Devolver un resultado HTTP 201 (Created) con la ubicación del recurso creado
            return CreatedAtAction(nameof(GetById), new { id = tarea.Id }, tarea);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, TareasModel tareaModel)
        {
            // Obtener la tarea existente por ID utilizando el servicio de aplicación
            Tareas existingTarea = _tareaAppService.GetById(id);
            if (existingTarea == null)
            {
                return NotFound();
            }
            // Actualizar los campos de la tarea existente con los valores del modelo recibido
            existingTarea.Id = tareaModel.Id;
            existingTarea.NombreTarea = tareaModel.NombreTarea;
            existingTarea.DescripcionTarea = tareaModel.DescripcionTarea;
            existingTarea.FechaCreacion = tareaModel.FechaCreacion;
            // Actualizar la tarea utilizando el servicio de aplicación
            _tareaAppService.Update(existingTarea);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Obtener la tarea existente por ID utilizando el servicio de aplicación
            Tareas existingTarea = _tareaAppService.GetById(id);
            if (existingTarea == null)
            {
                return NotFound();
            }
            // Eliminar la tarea utilizando el servicio de aplicación
            _tareaAppService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<TareasModel>> GetAll()
        {
            // Obtener todas las tareas utilizando el servicio de aplicación
            List<Tareas> tareas = _tareaAppService.GetAll();
            // Crear una lista de modelos de tareas a partir de las entidades encontradas
            List<TareasModel> tareaModels = tareas.Select(tarea => new TareasModel
            {
                Id = tarea.Id,
                NombreTarea = tarea.NombreTarea,
                DescripcionTarea = tarea.DescripcionTarea,
                FechaCreacion = tarea.FechaCreacion
            }).ToList();

            return Ok(tareaModels);
        }

    }
}
