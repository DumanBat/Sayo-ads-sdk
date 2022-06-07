namespace Sayollo.Ads
{
    [System.Serializable]
    public class UserJsonData
    {
        public string email;
        public string credit_card;
        public string expiration_date;

        public UserJsonData()
        {
            email = "";
            credit_card = "";
            expiration_date = "";
        }

        public UserJsonData(string email, string creditCard, string expDate)
        {
            this.email = email;
            this.credit_card = creditCard;
            this.expiration_date = expDate;
        }
    }
}
