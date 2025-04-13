

namespace AssemblerMachine.DataAccess
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddCar(Car car)
        {
           _context.Cars.Add(car);
           _context.SaveChanges();
        }

        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }
    }
}
