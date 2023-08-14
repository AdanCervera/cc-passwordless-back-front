namespace CC.Passwordless.API.Utils.Files.JSonFiles
{
    public interface IJsonFiles<T>
    {
        Task<T> ReadFileToObject(string path);
    }
}
