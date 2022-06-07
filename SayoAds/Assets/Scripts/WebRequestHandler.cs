using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Sayollo.Ads
{
    public static class WebRequestHandler
    {
        public enum RequestType
        {
            Text,
            Data
        }

        public static IEnumerator GetTexture(string url)
        {
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    Debug.Log(webRequest.error);
                    yield break;
                }
                yield return ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            }
        }

        public static IEnumerator GetRequest(string url, RequestType type)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    Debug.Log(webRequest.error);
                    yield break;
                }

                if (type == RequestType.Text)
                    yield return webRequest.downloadHandler.text;
                else
                    yield return webRequest.downloadHandler.data;
            }
        }

        public static IEnumerator Post(string url, string bodyJsonString)
        {
            var webRequest = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
                yield break;
            }

            yield return webRequest.downloadHandler.text;
        }
    }
}
