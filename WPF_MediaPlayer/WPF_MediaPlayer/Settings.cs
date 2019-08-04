using System.Xml;

namespace WPF_MediaPlayer
{
    public static class Settings
    {
        public static string CdDataFileName = "CdDataFile.xml";

        public static XmlWriterSettings XmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = true,
            NewLineOnAttributes = true,
        };
    }
}