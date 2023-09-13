using DddInPractice.Logic;
using DddInPractice.Logic.SharedKernel;
using DddInPractice.Logic.SnackMachines;
using NHibernate;

namespace DddInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void Test()
        {
            SessionFactory.Init(@"Server=.;Database=DddInPractice;Trusted_Connection=true;TrustServerCertificate=True");


            var repository = new SnackMachineRepository();
            SnackMachine snackMachine = repository.GetById(1);

            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.BuySnack(1);
            repository.Save(snackMachine);
        }
    }
}