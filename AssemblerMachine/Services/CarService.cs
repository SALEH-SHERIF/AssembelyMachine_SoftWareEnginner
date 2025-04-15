
using AssemblerMachine.DataAccess;
using AssemblerMachine.Model;

namespace AssemblerMachine.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public List<Car> GetAllCars()
        {
            return _carRepository.GetAllCars();
        }
		public Car CreateCar(string wheelType, int wheelNumber, string doorType, int doorNumber,
							  string glassType, int glassNumber, string motorType, int motorNumber)
		{
			return new Car
			{
				WheelType = wheelType ?? "unselectedWheel",
				WheelNumber = wheelNumber,
				DoorType = doorType ?? "unselectedDoor",
				DoorNumber = doorNumber,
				GlassType = glassType ?? "unselectedGlass",
				GlassNumber = glassNumber,
				MotorType = motorType ?? "unselectedMotor",
				MotorNumber = motorNumber
			};
		}
	}
}
