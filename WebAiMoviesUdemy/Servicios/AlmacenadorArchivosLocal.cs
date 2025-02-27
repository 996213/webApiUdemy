﻿namespace WebApiMoviesUdemy.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        //Trae la ruta del directorio wwwroot
        private readonly IWebHostEnvironment env;
        //Trae la ruta http
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task BorrarArchivo(string contenedor, string ruta)
        {
            if(ruta!= null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                var directorioArchivo = Path.Combine(env.WebRootPath, ruta, nombreArchivo);
                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }
            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(contenedor, ruta);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, nombreArchivo);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string ruta = Path.Combine(folder, nombreArchivo);

            await File.WriteAllBytesAsync(ruta, contenido);

            var urlActual = $"{ httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host }";
            var urlParaDB = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\", "/");
            return urlParaDB;

                
        }
    }
}
