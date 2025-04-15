
using System.Collections.Generic;
using AssemblerMachine.Model;
namespace AssemblerMachine.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car CreateCar(string wheelType, int wheelNumber, string doorType, int doorNumber,
							  string glassType, int glassNumber, string motorType, int motorNumber);

	}
}
