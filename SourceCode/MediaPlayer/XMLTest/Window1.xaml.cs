using System.Windows;
using System.Collections;
using System.Xml;

namespace XMLTest {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {

		private ArrayList albumData;
		private string datafile = "cddatafile.xml";

		public Window1() {
			InitializeComponent();
			albumData = new ArrayList();
		}
        /*
         // 'Old' way - using XmlTextWiter
         
                private void SaveBtn_Click( object sender, RoutedEventArgs e ) {
                    XmlTextWriter xtw = new XmlTextWriter(datafile, null);
                    xtw.Formatting = Formatting.Indented;
                    xtw.Indentation = 4;
                    xtw.WriteStartElement("CDCollection");
                    foreach( string[] arr in albumData ) {
                        xtw.WriteStartElement("CD");
                        xtw.WriteElementString("artist", arr[0]);
                        xtw.WriteElementString("album", arr[1]);
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                    xtw.Close();
                }
        */


        private void SaveBtn_Click(object sender, RoutedEventArgs e) {
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter xmlwr;
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            xmlwr = XmlWriter.Create(datafile, settings);          
            xmlwr.WriteStartElement("CDCollection");
            foreach (string[] arr in albumData) {
                xmlwr.WriteStartElement("CD");
                xmlwr.WriteElementString("artist", arr[0]);
                xmlwr.WriteElementString("album", arr[1]);
                xmlwr.WriteEndElement();
            }
            xmlwr.WriteEndElement();
            xmlwr.Close();
        }

        private void NewBtn_Click( object sender, RoutedEventArgs e ) {
			string artist = artistTB.Text.Trim();
			string album = albumTB.Text.Trim();
			string[] newitem;
			msgTB.Clear();
			if( (artist == "") || (album == "") ) {
				msgTB.Text = "You must enter an artist name and an album name";
			} else {
				newitem = new string[] { artist, album };
				albumData.Add(newitem);
			}
		}

		private void LoadBtn_Click( object sender, RoutedEventArgs e ) {
			XmlDocument xdoc = new XmlDocument();
			XmlNodeList xmlNodes;
			XmlNode artistNode;
			XmlNode albumNode;
			string author;
			string album;
			xdoc.Load(datafile);
			xmlNodes = xdoc.SelectNodes("CDCollection/CD");
			foreach( XmlNode node in xmlNodes ) {
				artistNode = node.SelectSingleNode("artist");
				author = artistNode.InnerText;
				albumNode = node.SelectSingleNode("album");
				album = albumNode.InnerText;
				rtb.AppendText(author + " | " + album +"\n");
			}

		}
	}
}
