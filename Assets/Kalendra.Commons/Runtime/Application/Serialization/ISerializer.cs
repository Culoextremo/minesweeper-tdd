namespace Kalendra.Commons.Runtime.Application.Serialization
{
    public interface ISerializer<T>
    {
        string Serialize(T source);
        T Deserialize(string source);
    }
}