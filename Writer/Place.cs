using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Writer
{
    public partial class Place : Form
    {
        private Boolean newNote; // true, если запись новая
        private String fileName;
        private Places pl;
        public Place(Boolean newNote, String fileName)
        {
            InitializeComponent();
            this.newNote = newNote;
            this.fileName = fileName;
            pl = new Places();
        }
        public Place(Boolean newNote, String fileName, String name)
        {
            InitializeComponent();
            this.newNote = newNote;
            this.fileName = fileName;
            pl = new Places(name);
            pl.GetXml(fileName);
            pl.Display(textBoxName, textBoxMain, textBoxAddress);
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (newNote)
            {
                Close();
            }
            else
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fileName);
                XmlElement xRoot = xdoc.DocumentElement;
                foreach (XmlNode n in xRoot)
                {
                    if (textBoxName.Text == n.Attributes["name"].Value)
                    {
                        xRoot.RemoveChild(n);
                        break;
                    }
                }
                xdoc.Save(fileName);
                Close();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (newNote)
            {
                pl.SetText(textBoxName, textBoxMain, textBoxAddress);
                pl.SetXml(fileName);
                newNote = false;
            }
            else
            {
                pl.Change(textBoxName, textBoxMain, textBoxAddress, fileName);
                pl.SetText(textBoxName, textBoxMain, textBoxAddress);
            }
        }
    }
}
