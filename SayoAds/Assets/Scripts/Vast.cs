using System;
using System.Xml;
using System.Xml.Serialization;

namespace Sayollo.Ads
{
    [Serializable]
    [XmlRoot(ElementName = "VAST")]
    public class Vast
    {
        [XmlElement(ElementName = "Ad")]
        public Ad ad;

        [XmlRoot(ElementName = "Ad")]
        public class Ad
        {
            [XmlElement(ElementName = "InLine")]
            public InLine inLine;
        }

        [XmlRoot(ElementName = "InLine")]
        public class InLine
        {
            [XmlElement(ElementName = "Error")]
            public string error;
            [XmlElement(ElementName = "Creatives")]
            public Creatives creatives;
        }

        [XmlRoot(ElementName = "Creatives")]
        public class Creatives
        {
            [XmlElement(ElementName = "Creative")]
            public Creative creative;
        }

        [XmlRoot(ElementName = "Creative")]
        public class Creative
        {
            [XmlElement(ElementName = "Linear")]
            public Linear linear;
        }

        [XmlRoot(ElementName = "Linear")]
        public class Linear
        {
            [XmlElement(ElementName = "Duration")]
            public string duration;
            [XmlElement(ElementName = "MediaFiles")]
            public MediaFiles mediaFiles;
        }

        [XmlRoot(ElementName = "MediaFiles")]
        public class MediaFiles
        {
            [XmlElement(ElementName = "Duration")]
            public string duration;
            [XmlElement(ElementName = "MediaFile")]
            public string mediaFileUrl;
        }
    }
}
