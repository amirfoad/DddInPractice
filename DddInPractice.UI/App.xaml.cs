using DddInPractice.Logic;

namespace DddInPractice.UI
{
    public partial class App
    {
        public App()
        {
            Initer.Init(@"Server=.;Database=DddInPractice;Trusted_Connection=true;TrustServerCertificate=True");
        }
    }
}