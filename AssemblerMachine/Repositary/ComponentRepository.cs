

using Microsoft.EntityFrameworkCore;

namespace AssemblerMachine.DataAccess
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly ApplicationDbContext _context;
        public ComponentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<string> GetComponents(string type, string subType, int requiredCount)
        {
           return _context.Components
          .Where(c => c.Type == type && c.SubType == subType)
          .Take(requiredCount)
          .Select(c => c.Instructions)
          .ToList();
        }

        public bool HasEnoughComponents(string type, string subType, int requiredCount)
        {
            return _context.Components
           .Count(c => c.Type == type && c.SubType == subType) >= requiredCount;
        }

        public void RemoveComponents(List<string> instructions)
        {
            // Remove components from the database based on the instructions
            _context.Components.RemoveRange(
                _context.Components.Where(c => instructions.Contains(c.Instructions))
            );
        }
    }
}
