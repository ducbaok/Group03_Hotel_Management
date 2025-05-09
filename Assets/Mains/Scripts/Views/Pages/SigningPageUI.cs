using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public class SingingPageUI : MonoBehaviour, IInitializable
    {
        private VisualElement _root;

        private VisualElement _confirmPasswordField;

        private TextField _accountInputField;
        private TextField _passwordInputField;
        private TextField _confirmInputField;

        private VisualElement _signInWithFacebookButton;
        private VisualElement _signInWithGoogleButton;

        private VisualElement _recoveryButton;
        private Button _signingButton;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            var signingInputField = _root.Q("SigningInputField");

            _confirmPasswordField = signingInputField.Q("ConfirmField");

            _accountInputField = signingInputField.Q("AccountField").Q("TextField") as TextField;
            _accountInputField.RegisterValueChangedCallback(OnValueChanged_AccountInputField);

            _passwordInputField = signingInputField.Q("PasswordField").Q("TextField") as TextField;
            _passwordInputField.RegisterValueChangedCallback(OnValueChanged_PasswordInputField);

            _confirmInputField = signingInputField.Q("ConfirmField").Q("TextField") as TextField;
            _confirmInputField.RegisterValueChangedCallback(OnValueChanged_ConfirmInputField);

            _signInWithFacebookButton = _root.Q("SigningMethod").Q("FacebookSigning");
            _signInWithFacebookButton.RegisterCallback<PointerDownEvent>(SigningWithFacebook);

            _signInWithGoogleButton = _root.Q("SigningMethod").Q("GoogleSigning");
            _signInWithGoogleButton.RegisterCallback<PointerDownEvent>(SignInWithGoogle);

            _signingButton = signingInputField.Q("SigningButton") as Button;
            _signingButton.clicked += SigningAccount;

            _recoveryButton = signingInputField.Q("RecoveryButton");
            _recoveryButton.RegisterCallback<PointerDownEvent>(RecoveryAccount);
        }

        private void OnValueChanged_AccountInputField(ChangeEvent<string> evt)
        {

        }

        private void OnValueChanged_PasswordInputField(ChangeEvent<string> evt)
        {

        }

        private void OnValueChanged_ConfirmInputField(ChangeEvent<string> evt)
        {

        }

        private void SigningWithFacebook(PointerDownEvent evt)
        {

        }

        private void SignInWithGoogle(PointerDownEvent evt)
        {

        }

        private void SigningAccount()
        {

        }

        private void RecoveryAccount(PointerDownEvent evt)
        {

        }
    }
}
