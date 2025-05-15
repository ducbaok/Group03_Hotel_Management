using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public class MainViewAccountPageUI : ViewPageUI, ICollectible
    {
        private VisualElement _profilePicture;
        private Label _nameText;
        private VisualElement _changeNameButton;
        private Label _phoneField;
        private Label _emailField;
        private VisualElement _languageField;
        private Label _languageText;
        private VisualElement _locationField;
        private Label _locationText;
        private VisualElement _qaFieldField;
        private VisualElement _policyField;
        private VisualElement _versionField;
        private Label _versionText;
        private VisualElement _contactField;
        private VisualElement _signOutField;

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


            var contentContainer = Root.Q("ContentScroll").Q("unity-content-container");

            var accountField = contentContainer.Q("AccountField");

            _profilePicture = accountField.Q("ProfilePicture");

            var infoField = accountField.Q("InfoField");

            _nameText = infoField.Q("NameText") as Label;
            _phoneField = infoField.Q("PhoneField") as Label;
            _emailField = infoField.Q("EmailField") as Label;

            _changeNameButton = infoField.Q("ChangeButton");
            _changeNameButton.RegisterCallback<PointerDownEvent>(OnClicked_ChangeNameButton);

            var settingsField = contentContainer.Q("SettingsField");

            _languageField = settingsField.Q("LanguageField");
            _languageText = _languageField.Q("Text") as Label;
            _languageField?.RegisterCallback<PointerDownEvent>(OnClicked_LanguageField);

            _locationField = settingsField.Q("LocationField");
            _locationText = _locationField.Q("Text") as Label;
            _locationField?.RegisterCallback<PointerDownEvent>(OnClicked_LocationField);

            var informationsField = contentContainer.Q("InformationsField");

            _qaFieldField = informationsField.Q("QAField");
            _qaFieldField?.RegisterCallback<PointerDownEvent>(OnClicked_QandAField);

            _policyField = informationsField.Q("PolicyField");
            _policyField?.RegisterCallback<PointerDownEvent>(OnClicked_PolicyField);

            _versionField = informationsField.Q("VersionField");
            _versionField?.RegisterCallback<PointerDownEvent>(OnClicked_VersionField);

            _contactField = informationsField.Q("ContactField");
            _contactField?.RegisterCallback<PointerDownEvent>(OnClicked_ContactField);

            _signOutField = informationsField.Q("SignOutField");
            _signOutField?.RegisterCallback<PointerDownEvent>(OnClicked_SignOutField);
        }

        private void OnClicked_ChangeNameButton(PointerDownEvent evt)
        {

        }

        private void OnClicked_LanguageField(PointerDownEvent evt)
        {

        }

        private void OnClicked_LocationField(PointerDownEvent evt)
        {

        }

        private void OnClicked_QandAField(PointerDownEvent evt)
        {

        }

        private void OnClicked_PolicyField(PointerDownEvent evt)
        {

        }

        private void OnClicked_VersionField(PointerDownEvent evt)
        {

        }

        private void OnClicked_ContactField(PointerDownEvent evt)
        {

        }

        private void OnClicked_SignOutField(PointerDownEvent evt)
        {

        }
    }
}