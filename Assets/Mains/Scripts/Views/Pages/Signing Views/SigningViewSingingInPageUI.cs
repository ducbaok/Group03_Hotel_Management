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
            _accountInput = evt.newValue.Trim();
            _validAccountInput = !string.IsNullOrEmpty(_accountInput);

            if (!_validAccountInput)
            {
                _accountMessage.SetText("Account field cannot be empty.");
                return;
            }

            _accountMessage.SetText(string.Empty);
        }

        private void OnValueChanged_PasswordInputField(ChangeEvent<string> evt)
        {
            _passwordInput = evt.newValue;
            _validPasswordInput = !string.IsNullOrEmpty(_passwordInput);

            if (!_validPasswordInput)
            {
                _passwordMessage.SetText("Password cannot be empty.");
                return;
            }

            _passwordMessage.SetText(string.Empty);
        }

        private void SigningAccount()
        {
            if (!_validAccountInput || !_validPasswordInput)
            {
                _passwordMessage.SetText("Please complete all fields correctly.");
                return;
            }

            var matchedAccount = Main.Database.Accounts.Find(acc =>
                (acc.Email == _accountInput || acc.PhoneNumber == _accountInput) &&
                acc.Password == _passwordInput);

            if (matchedAccount == null)
            {
                _passwordMessage.SetText("Incorrect email/phone or password.");
                return;
            }

            _passwordMessage.SetText(string.Empty);
            Marker.OnViewPageSwitched?.Invoke(ViewType.MainView, ViewKey.MainViewHomePage, true);
        }

        private void SigningWithFacebook(PointerDownEvent evt)
        {
            
        }

        private void SignInWithGoogle(PointerDownEvent evt)
        {
            
        }

        private void RecoveryAccount(PointerDownEvent evt)
        {
            
        }
    }
}
