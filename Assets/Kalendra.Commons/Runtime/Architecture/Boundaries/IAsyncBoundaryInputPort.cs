using System.Threading.Tasks;

namespace Kalendra.Commons.Runtime.Architecture.Boundaries
{
    public interface IBoundaryInputPort
    {
        void Request();
    }

    public interface IBoundaryInputPort<in TArg> where TArg : IBoundaryRequestDTO
    {
        void Request(TArg requestDTO);
    }
    
    public interface IAsyncBoundaryInputPort
    {
        Task Request();
    }

    public interface IAsyncBoundaryInputPort<in TArg> where TArg : IBoundaryRequestDTO
    {
        Task Request(TArg requestDTO);
    }

    public interface IBoundaryRequestDTO { }
}