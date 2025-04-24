namespace HotelReservation
{
    public class AccountManager
    {
        public UID AccountID = new();
        public List<Account>? Accounts;

        public void SignUpAccount(AccountVerificationType type, string input, string password)
        {
            Account? account;

            if (type == AccountVerificationType.Email)
            {
                if (!Handler.Validator.ValidateEmail(input))
                {
                    Event.OnAccountVerificated?.Invoke(AccountVerificationResult.SignUpEmailNotValid);
                    return;
                }
                if ((account = Accounts?.Find(i => i.Email == input)) == null)
                {
                    Event.OnAccountVerificated?.Invoke(AccountVerificationResult.SignUpEmailHasExisted);
                    return;
                }

                Console.WriteLine($"Account ID: {AccountID = account.ID}");
            }
            else if (type == AccountVerificationType.PhoneNumber)
            {
                if (!Handler.Validator.ValidatePhoneNumber(input))
                {
                    Event.OnAccountVerificated?.Invoke(AccountVerificationResult.SignUpPhoneNumberNotValid);
                    return;
                }
                if ((account = Accounts?.Find(i => i.PhoneNumber == input)) == null)
                {
                    Event.OnAccountVerificated?.Invoke(AccountVerificationResult.SignUpPhoneNumberHasExisted);
                    return;
                }

                Console.WriteLine($"Account ID: {AccountID = account.ID}");
            }
        }

        public void AssignAccount(UID id)
        {

        }
    }
}
