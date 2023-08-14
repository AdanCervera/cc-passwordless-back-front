using Newtonsoft.Json;

namespace CC.Passwordless.API.Utils.Files.JSonFiles
{
    public class JsonFiles<T> : IJsonFiles<T>
    {
        public async Task<T> ReadFileToObject(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path), "The path cannot be empty or null");
            }

            try
            {
                using FileStream fileStream = new(path, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new(fileStream);
                string json = await reader.ReadToEndAsync();
                T objectReturn = JsonConvert.DeserializeObject<T>(json) ?? throw new Exception("JSON File Empty ");
                return objectReturn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in the JSON file: " + ex.Message, ex);
            }
        }
    }
}
