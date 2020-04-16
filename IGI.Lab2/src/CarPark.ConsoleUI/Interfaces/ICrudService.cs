namespace CarPark.ConsoleUI.Interfaces
{
    public interface ICrudService<T>
        where T : class
    {
        void Add();

        void Remove();

        void Edit();

        T Create();
    }
}