using System;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SigningViewSingingInPageUI : ViewPageUI, ICollectible
    {
        private TextField _accountInputField;
        private Label _accountMessage;
        private TextField _passwordInputField;
        private Label _passwordMessage;

        private Button _signingButton;
        private VisualElement _recoveryButton;

        private VisualElement _signInWithFacebookButton;
        private VisualElement _signInWithGoogleButton;

        private string _accountInput;
        private string _passwordInput;
        private bool _validAccountInput;
        private bool _validPasswordInput;
        private bool _validEmailInput;

        protected override void VirtualAwake()
        {
            Marker.OnSystemStart += Collect;
        }

        private void OnDestroy()
        {
            Marker.OnSystemStart -= Collect;
        }

        public void Collect()
        {
            var signingInputField = Root.Q("SigningInputField");

            _accountInputField = signingInputField.Q("AccountField").Q("TextField") as TextField;
            _accountInputField.RegisterValueChangedCallback(OnValueChanged_AccountInputField);

            _accountMessage = signingInputField.Q("AccountField").Q("Message") as Label;

            _passwordInputField = signingInputField.Q("PasswordField").Q("TextField") as TextField;
            _passwordInputField.RegisterValueChangedCallback(OnValueChanged_PasswordInputField);

            _passwordMessage = signingInputField.Q("PasswordField").Q("Message") as Label;

            _signInWithFacebookButton = Root.Q("SigningMethod").Q("FacebookSigning");
            _signInWithFacebookButton.RegisterCallback<PointerDownEvent>(SigningWithFacebook);

            _signInWithGoogleButton = Root.Q("SigningMethod").Q("GoogleSigning");
            _signInWithGoogleButton.RegisterCallback<PointerDownEvent>(SignInWithGoogle);

            _signingButton = signingInputField.Q("SigningButton").Q("Button") as Button;
            _signingButton.clicked += SigningAccount;

            _recoveryButton = signingInputField.Q("RecoveryButton");
            _recoveryButton.RegisterCallback<PointerDownEvent>(RecoveryAccount);

            Initialize();
        }

        private void Initialize()
        {
            _accountMessage.SetText(string.Empty);
            _passwordMessage.SetText(string.Empty);
        }

        private void OnValueChanged_AccountInputField(ChangeEvent<string> evt)
        {
            _accountInput = evt.newValue;

            if (_accountInput == string.Empty)
            {
                _accountMessage.SetText(string.Empty);
                _validAccountInput = false;
                return;
            }

            _validEmailInput = Extension.Validator.ValidateEmail(_accountInput);
            var validPhoneInput = Extension.Validator.ValidatePhoneNumber(_accountInput);

            _validAccountInput = false;

            if (!_validEmailInput && !validPhoneInput)
            {
                _accountMessage.SetText("Email or Phone number is not valid!");
                return;
            }

            _accountMessage.SetText(string.Empty);
            _validAccountInput = true;
        }

        private void OnValueChanged_PasswordInputField(ChangeEvent<string> evt)
        {
            _passwordInput = evt.newValue;

            if (_passwordInput == string.Empty)
            {
                _passwordMessage.SetText(string.Empty);
                _validPasswordInput = false;
                return;
            }

            if (_passwordInput.Length < 8)
            {
                _passwordMessage.SetText("Password must be at least 8 characters.");
                _validPasswordInput = false;
                return;
            }

            _passwordMessage.SetText(string.Empty);
            _validPasswordInput = true;
        }

        private void SigningWithFacebook(PointerDownEvent evt)
        {
            // Handle Facebook sign-in logic
        }

        private void SignInWithGoogle(PointerDownEvent evt)
        {
            // Handle Google sign-in logic
        }

        private void SigningAccount()
        {
            if (!_validAccountInput || !_validPasswordInput)
            {
                _passwordMessage.SetText("Please correct the inputs before signing in.");
                return;
            }

            Account foundAccount = null;
            foreach (var acc in Main.Database.Accounts)
            {
                if ((_validEmailInput && acc.Email == _accountInput) ||
                    (!string.IsNullOrEmpty(acc.PhoneNumber) && acc.PhoneNumber == _accountInput))
                {
                    foundAccount = acc;
                    break;
                }
            }

            if (foundAccount == null || foundAccount.Password != _passwordInput)
            {
                _passwordMessage.SetText("Incorrect account or password.");
                return;
            }

            _passwordMessage.SetText(string.Empty);
            Marker.OnViewPageSwitched?.Invoke(ViewType.MainView, ViewKey.MainViewHomePage, true);
        }

        private void RecoveryAccount(PointerDownEvent evt)
        {
            // Handle password recovery logic
        }
    }
}
