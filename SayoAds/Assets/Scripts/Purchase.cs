using System.Collections;
using UnityEngine;

namespace Sayollo.Ads
{
    public class Purchase : MonoBehaviour
    {
        [SerializeField]
        private string _itemUrl;
        [SerializeField]
        private string _purchaseUrl;
        [SerializeField]
        private string _userDataNotFilledText;
        [SerializeField]
        private string _userDataSendText;

        private PurchaseView _purchaseView;

        private void Awake()
        {
            _purchaseView = GetComponent<PurchaseView>();
            _purchaseView.Close.onClick.AddListener(() => _purchaseView.SetActivePurchaseView(false));
            _purchaseView.Submit.onClick.AddListener(() => StartCoroutine(SubmitPurchase()));
            _purchaseView.AcceptButton.onClick.AddListener(() => _purchaseView.SetActiveModalWindow(false, ""));
        }

        public IEnumerator DisplayPuchaseView()
        {
            var emptyJsonData = new ItemJsonData();
            var jsonString = JsonUtility.ToJson(emptyJsonData);

            var postCd = new CoroutineWithData(this, WebRequestHandler.Post(_itemUrl, jsonString));
            yield return postCd.coroutine;

            var item = postCd.result as string;
            var text = item.Replace("'", "\"");
            var jsonData = JsonUtility.FromJson<ItemJsonData>(text);

            StartCoroutine(_purchaseView.DisplayPurchaseData(jsonData));
        }

        private IEnumerator SubmitPurchase()
        {
            if (!_purchaseView.IsCreditCardFilled || !_purchaseView.IsEmailFilled || !_purchaseView.IsExpDateFilled)
            {
                _purchaseView.SetActiveModalWindow(true, _userDataNotFilledText);
                yield break;
            }
            var userData = new UserJsonData(_purchaseView.Email.text, _purchaseView.CreditCard.text, _purchaseView.ExpDate.text);
            var userDataString = JsonUtility.ToJson(userData);
            yield return StartCoroutine(WebRequestHandler.Post(_purchaseUrl, userDataString));
            _purchaseView.SetActiveModalWindow(true, _userDataSendText);
            _purchaseView.SetActivePurchaseView(false);
        }
    }
}
