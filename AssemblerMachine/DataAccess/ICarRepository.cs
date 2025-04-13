namespace AssemblerMachine.DataAccess
{
    public interface ICarRepository
    {
        void AddCar(Car car);
        List<Car> GetAllCars();
    }
}
