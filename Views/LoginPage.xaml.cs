using Fonts;
using Microsoft.Maui.Controls;
using System;

namespace Hotel_Reservation.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnShowPasswordClicked(object sender, EventArgs e)
        {
            // Toggle password visibility
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            ShowPasswordButton.ImageSource = new FontImageSource
            {
                FontFamily = "FluentSystemIconsRegular",
                Glyph = PasswordEntry.IsPassword ? FluentUI.eye_16_regular : FluentUI.eye_off_16_regular,
                Color = Colors.Black,
                Size = 18
            };
        }

        private void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            // Navigate to forgot password page
            DisplayAlert("Forgot Password", "Navigate to password reset", "OK");
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            // Validate and process login
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            bool rememberMe = RememberMeCheckbox.IsChecked;

            // Add your authentication logic here
            DisplayAlert("Login", $"Logging in with {email}", "OK");
        }

        private void OnGoogleLoginClicked(object sender, EventArgs e)
        {
            // Implement Google authentication
            DisplayAlert("Google Login", "Logging in with Google", "OK");
        }

        private void OnFacebookLoginClicked(object sender, EventArgs e)
        {
            // Implement Facebook authentication
            DisplayAlert("Facebook Login", "Logging in with Facebook", "OK");
        }

        private void OnSignUpTapped(object sender, EventArgs e)
        {
            // Navigate to sign up page
            DisplayAlert("Sign Up", "Navigate to registration page", "OK");
        }
    }
}