using Newtonsoft.Json;
using RedisApi.CrossCutting;
using RedisApi.Domain.Entities;
using RedisApi.Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace RedisApi.Infraestructure.Repositories
{
    public class RedisTareasRepository : ITareasRepository
    {
        // Es una interfaz en la biblioteca StackExchange.Redis que define los métodos
        // y propiedades para interactuar con una base de datos en Redis.
        private readonly IDatabase _redisDatabase;

        public RedisTareasRepository(RedisConnection redisConnection)
        {
            // Obtener la instancia de IDatabase de la conexión Redis proporcionada
            _redisDatabase = redisConnection.Redis.GetDatabase();
        }

        public Tareas GetById(Guid id)
        {
            // Obtener el valor de Redis correspondiente a la clave "Tarea:{id}"
            RedisValue tareaJson = _redisDatabase.StringGet($"Tarea:{id}");
            if (!tareaJson.IsNull)
            {
                // Si se encuentra un valor no nulo, deserializarlo en un objeto Tareas y devolverlo
                return JsonConvert.DeserializeObject<Tareas>(tareaJson);
            }
            // Si no se encuentra el valor, devolver null
            return null;
        }

        public void Add(Tareas tarea)
        {
            // Serializar el objeto Tareas en formato JSON
            string tareaJson = JsonConvert.SerializeObject(tarea);
            // Almacenar el valor JSON en Redis utilizando la clave "Tarea:{tarea.Id}"
            _redisDatabase.StringSet($"Tarea:{tarea.Id}", tareaJson);
        }

        public void Update(Tareas tarea)
        {
            // Serializar el objeto Tareas en formato JSON
            string tareaJson = JsonConvert.SerializeObject(tarea);
            // Actualizar el valor JSON en Redis utilizando la misma clave "Tarea:{tarea.Id}"
            _redisDatabase.StringSet($"Tarea:{tarea.Id}", tareaJson);
        }

        public void Delete(Guid id)
        {
            // Eliminar la clave correspondiente a la tarea en Redis
            _redisDatabase.KeyDelete($"Tarea:{id}");
        }

        public List<Tareas> GetAll()
        {
            // Obtener todas las claves en Redis que sigan el patrón "Tarea:*"
            var keys = _redisDatabase.Multiplexer.GetServer(_redisDatabase.Multiplexer.GetEndPoints()[0]).Keys(pattern: "Tarea:*");
            var tareas = new List<Tareas>();
            foreach (var key in keys)
            {
                // Para cada clave, obtener el valor asociado en Redis
                RedisValue tareaJson = _redisDatabase.StringGet(key);
                if (!tareaJson.IsNull)
                {
                    // Si se encuentra un valor no nulo, deserializarlo en un objeto Tareas y agregarlo a la lista
                    var tarea = JsonConvert.DeserializeObject<Tareas>(tareaJson);
                    tareas.Add(tarea);
                }
            }
            // Devolver la lista de tareas obtenidas de Redis
            return tareas;
        }
    }

}
