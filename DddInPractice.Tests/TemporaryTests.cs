using DddInPractice.Logic;
using NHibernate;

namespace DddInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void Test()
        {
            SessionFactory.Init(@"Server=.;Database=DddInPractice;Trusted_Connection=true;TrustServerCertificate=True");

            using (ISession session = SessionFactory.OpenSession())
            {
                long id = 1;
                var snackMachine = session.Get<SnackMachine>(id);
            }
        }
    }
}