using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Sayollo.Ads
{
    public class PurchaseView : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;
        [SerializeField]
        private Button _close;

        [Header("Item View")]
        [SerializeField]
        private Text _title;
        [SerializeField]
        private Text _itemName;
        [SerializeField]
        private Text _priceDisplay;
        [SerializeField]
        private RawImage _itemImage;

        [Header("User data")]
        [SerializeField]
        private InputField _email;
        [SerializeField]
        private InputField _creditCard;
        [SerializeField]
        private InputField _expDate;
        [SerializeField]
        private Button _submit;

        [Header("Modal window")]
        [SerializeField]
        private Transform _modalRoot;
        [SerializeField]
        private Text _modalWindowText;
        [SerializeField]
        private Button _acceptButton;

        public Button Close => _close;
        public Button Submit => _submit;
        public Button AcceptButton => _acceptButton;
        public InputField Email => _email;
        public InputField CreditCard => _creditCard;
        public InputField ExpDate => _expDate;
        public bool IsEmailFilled => !string.IsNullOrEmpty(_email.text);
        public bool IsCreditCardFilled => !string.IsNullOrEmpty(_creditCard.text);
        public bool IsExpDateFilled => !string.IsNullOrEmpty(_expDate.text);

        public IEnumerator DisplayPurchaseData(ItemJsonData jsonData)
        {
            _title.text = jsonData.title;
            _itemName.text = jsonData.item_name;
            _priceDisplay.text = $"{jsonData.currency}: {jsonData.price}{jsonData.currency_sign}";
            var textureCd = new CoroutineWithData(this, WebRequestHandler.GetTexture(jsonData.item_image));
            yield return textureCd.coroutine;
            _itemImage.texture = textureCd.result as Texture;

            SetActivePurchaseView(true);
        }

        public void SetActivePurchaseView(bool val) => _root.gameObject.SetActive(val);

        public void SetActiveModalWindow(bool val, string text)
        {
            _modalWindowText.text = text;
            _modalRoot.gameObject.SetActive(val);
        }
    }
}