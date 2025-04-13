
namespace AssemblerMachine.DataAccess
{
    public interface IComponentRepository
    {
        List<string> GetComponents(string type, string subType, int requiredCount);
        bool HasEnoughComponents(string type, string subType, int requiredCount);

        void RemoveComponents(List<string> instructions);
       
    }
}
