using StackExchange.Redis;

namespace RedisApi.CrossCutting
{
    public class RedisConnection
    {
        /*
         * Esta propiedad representa una instancia de la clase ConnectionMultiplexer que se utiliza para
         * establecer la conexión con el servidor de Redis.
         * ConnectionMultiplexer proporciona métodos y propiedades para administrar la conexión y realizar
         * operaciones en la base de datos de Redis.
         * Al declarar esta propiedad, se puede acceder a la instancia de ConnectionMultiplexer desde
         * otras partes del código de la clase.
        */
        public ConnectionMultiplexer Redis { get; }

        public RedisConnection()
        {
            // Crear una instancia de ConfigurationOptions para configurar la conexión a Redis
            ConfigurationOptions config = new ConfigurationOptions
            {
                // Especificar la dirección y puerto de Redis
                EndPoints = { "localhost:6379" },
                // Establecer un tiempo de espera para establecer la conexión (en milisegundos)
                ConnectTimeout = 5000,
                // Establecer un tiempo de espera para operaciones de sincronización (en milisegundos)
                SyncTimeout = 5000
            };

            // Establecer la conexión a Redis utilizando ConnectionMultiplexer
            Redis = ConnectionMultiplexer.Connect(config);
        }
    }
}
