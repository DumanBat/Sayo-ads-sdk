using UnityEngine;
using UnityEngine.UI;
using Sayollo.Ads;

public class Caller : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Purchase _purchase; 

    private void Awake()
    {
        _button.onClick.AddListener(() => StartCoroutine(_purchase.DisplayPuchaseView()));
    }
}
