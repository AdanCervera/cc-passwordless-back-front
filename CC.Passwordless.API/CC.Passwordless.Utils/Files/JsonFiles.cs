using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Passwordless.Utils.Files
{
    public static class JsonFiles<T>
    {
        public static async Task<T> ReadFileToObject(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path), "La ruta del archivo no puede ser nula o vacía.");
            }

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string json = await reader.ReadToEndAsync();
                        T objectReturn = JsonConvert.DeserializeObject<T>(json);
                        return objectReturn;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer/deserializar el archivo JSON: " + ex.Message, ex);
            }
        }
    }
}
