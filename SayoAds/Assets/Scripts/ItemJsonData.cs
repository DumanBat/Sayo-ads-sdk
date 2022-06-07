namespace Sayollo.Ads
{
    [System.Serializable]
    public class ItemJsonData
    {
        public string title;
        public string item_id;
        public string item_name;
        public string item_image;
        public float price;
        public string currency;
        public string currency_sign;
        public string status;
        public int error_code;

        public ItemJsonData()
        {
            title = "";
            item_id = "";
            item_name = "";
            item_image = "";
            price = 0.0f;
            currency = "";
            currency_sign = "";
            status = "";
            error_code = 0;
        }
    }
}
