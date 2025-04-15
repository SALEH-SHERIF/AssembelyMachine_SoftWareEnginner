
using AssemblerMachine.DataAccess;
using System.Runtime.InteropServices;
using MachinesWrapper;
using AssemblerMachine.Model;
namespace AssemblerMachine.Services
{
    public class AssembleService : IAssembleService
    {
        private readonly ICarRepository _carRepository;
        private readonly IComponentRepository _componentRepository;
        public AssembleService(ICarRepository carRepository, IComponentRepository componentRepository)
        {
            _carRepository = carRepository;
            _componentRepository = componentRepository;
        }
        public bool AssembleCar(Car car)
        {
            // Validate availability
            var wheels = _componentRepository.GetComponents("Wheel", car.WheelType, car.WheelNumber);
            var doors = _componentRepository.GetComponents("Door", car.DoorType, car.DoorNumber);
            var glasses = _componentRepository.GetComponents("Glass", car.GlassType, car.GlassNumber);
            var motors = _componentRepository.GetComponents("Motor", car.MotorType, car.MotorNumber);

            if (wheels.Count < car.WheelNumber || doors.Count < car.DoorNumber || glasses.Count < car.GlassNumber || motors.Count < car.MotorNumber)
            {
                return false;
            }
            List<string> instructions = wheels.Concat(doors).Concat(glasses).Concat(motors).ToList();

            try
            {
                foreach (var instruction in instructions)
                {
                    // Create IntPtr from the parsed value
                    MachineDriverWrapper.Assemble(instruction);

                }

            }
            catch (Exception ex)
            {
                return false;
            }

            _componentRepository.RemoveComponents(instructions);
            _carRepository.AddCar(car);

            return true; 

        }
    }
}
