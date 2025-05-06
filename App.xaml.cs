using Hotel_Reservation.Views;

namespace Hotel_Reservation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = new AppShell();
            shell.GoToAsync("//LoginPage"); // Điều hướng đến LoginPage
            return new Window(shell);
        }
    }
}
