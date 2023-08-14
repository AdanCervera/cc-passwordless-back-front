using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Passwordless.Utils.Files
{
    public static class HtmlFiles
    {
        public static string LoadHtmlFromFile(string filePath)
        {
            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"El archivo {filePath} no se encuentra.");
                }

                // Leer el contenido HTML del archivo
                string htmlContent;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    htmlContent = reader.ReadToEnd();
                }

                return htmlContent;
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí (registros de errores, etc.)
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                return null;
            }
        }
    }
}
