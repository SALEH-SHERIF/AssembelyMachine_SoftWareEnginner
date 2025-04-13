
using System.Collections.Generic;
using AssemblerMachine.DataAccess;
namespace AssemblerMachine.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();  
    }
}
