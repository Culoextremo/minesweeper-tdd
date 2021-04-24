namespace Kalendra.BoardCore.Tests
{
    public interface IEventListenerMock
    {
        void Call();
    }
    
    public interface IEventListenerMock<in T>
    {
        void Call(T param);
    }
}