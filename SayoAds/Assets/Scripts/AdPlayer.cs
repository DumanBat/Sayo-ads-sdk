using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;

namespace Sayollo.Ads
{
    [RequireComponent(typeof(VideoPlayer))]
    public class AdPlayer : MonoBehaviour
    {
        [SerializeField]
        private string _vastUrl;
        [SerializeField]
        private string _videoFileName;
        [SerializeField]
        private string _videoFileFormat;
        [SerializeField]
        private RenderTexture _renderTexture;
        [SerializeField]
        private RawImage _displayImage;

        private VideoPlayer _videoPlayer;
        private string _dataPath;

        private void Awake()
        {
            _dataPath = Application.persistentDataPath + "/";
            _videoPlayer = GetComponent<VideoPlayer>();
        }

        void Start()
        {
            StartCoroutine(LoadAd());
        }

        private IEnumerator LoadAd()
        {
            var vastCd = new CoroutineWithData(this, WebRequestHandler.GetRequest(_vastUrl, WebRequestHandler.RequestType.Text));
            yield return vastCd.coroutine;

            var link = GetMediaLinkFromVast(vastCd.result as string);
            var media = new CoroutineWithData(this, WebRequestHandler.GetRequest(link, WebRequestHandler.RequestType.Data));
            yield return media.coroutine;

            SaveVideo(media.result);
            StartCoroutine(PlayVideoByUrl(_videoFileName));
        }

        private string GetMediaLinkFromVast(string vastString)
        {
            string savePath = _dataPath + "vast.xml";
            File.WriteAllText(savePath, vastString);
            var vast = Deserialize(savePath);

            var link = vast.ad.inLine.creatives.creative.linear.mediaFiles.mediaFileUrl;
            return link;
        }

        private void SaveVideo(object video)
        {
            var path = $"{_dataPath}{_videoFileName}{_videoFileFormat}";
            if (File.Exists(path))
                return;

            File.WriteAllBytes(path, video as byte[]);
        }

        public IEnumerator PlayVideoByUrl(string name)
        {
            var renderTexture = new RenderTexture(_renderTexture);

            _displayImage.texture = renderTexture;
            _videoPlayer.targetTexture = renderTexture;
            _videoPlayer.source = VideoSource.Url;
            _videoPlayer.url = $"{_dataPath}{name}{_videoFileFormat}";
            _videoPlayer.Prepare();

            while (_videoPlayer.isPrepared == false)
                yield return null;

            _videoPlayer.Play();
        }

        public Vast Deserialize(string path)
        {
            var serializer = new XmlSerializer(typeof(Vast));
            var stream = new FileStream(path, FileMode.Open);
            var container = serializer.Deserialize(stream) as Vast;
            stream.Close();
            return container;
        }
    }
}
