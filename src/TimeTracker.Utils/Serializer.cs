using Jil;

namespace TimeTracker.Utils
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string json);
    }

    public class Serializer : ISerializer
    {
        public string Serialize<T>(T obj)
        {
            return JSON.Serialize(obj);
        }

        public T Deserialize<T>(string json)
        {
            return JSON.Deserialize<T>(json);
        }
    }
}