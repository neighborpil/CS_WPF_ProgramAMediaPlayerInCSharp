using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;

namespace XMLTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<AlbumData> _albumData;

        public MainWindow()
        {
            InitializeComponent();

            _albumData = new List<AlbumData>();
        }

        private void NewCdButton_Click(object sender, RoutedEventArgs e)
        {
            string artist = AlbumTextBox.Text.Trim();
            string album = AlbumTextBox.Text.Trim();
            
            MessageTextBox.Clear();

            if (CheckAlbumDataIsEmpty(artist, album))
                return;

            _albumData.Add(new AlbumData(artist, album));
        }

        private bool CheckAlbumDataIsEmpty(string artist, string album)
        {
            if (string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(album))
            {
                MessageTextBox.Text = "You must enter an artist name and an album name";
                return true;
            }

            return false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var xmlWriter = XmlWriter.Create(Settings.CdDataFileName, Settings.XmlWriterSettings))
            {
                xmlWriter.WriteStartElement("CDCollection");
                _albumData.ForEach(data =>
                {
                    xmlWriter.WriteStartElement("CD");
                    xmlWriter.WriteElementString("Artist", data.Artist);
                    xmlWriter.WriteElementString("Album", data.Album);
                    xmlWriter.WriteEndElement();
                });
                xmlWriter.WriteEndElement();
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var document = new XmlDocument();
            try
            {
                document.Load(Settings.CdDataFileName);

                XmlNodeList xmlNodes = document.SelectNodes("CDCollection/CD");
                foreach (XmlNode node in xmlNodes)
                {
                    var artist = GetNodeText(node, "Artist");
                    var album = GetNodeText(node, "Album");

                    AppendToMainRichTextBox(artist, album);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't load album data[{ex.Message}]");
            }
        }

        private static string GetNodeText(XmlNode node, string name)
        {
            var singleNode = node.SelectSingleNode(name);
            string innerText = "-";
            if (singleNode != null)
                innerText = singleNode.InnerText;

            return innerText;
        }

        private void AppendToMainRichTextBox(string artist, string album)
        {
            MainRichTextBox.AppendText($"Artist:{artist}|Album:{album}\n");
        }
    }
}
