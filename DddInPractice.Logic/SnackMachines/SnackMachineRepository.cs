using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;

namespace DddInPractice.Logic.SnackMachines;

public class SnackMachineRepository:Repository<SnackMachine>
{
    public IReadOnlyList<SnackMachine> GetAllWithSnack(Snack snack)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<SnackMachine> GetAllWithMoneyInside(Money money)
    {
        throw new NotImplementedException();
    }
}