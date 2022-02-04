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
    public partial class Note : Form
    {
        private Boolean newNote; // true, если запись новая
        private String fileName;
        private Ordinary no;
        public Note(Boolean newNote, String fileName)
        {
            InitializeComponent();
            this.newNote = newNote;
            this.fileName = fileName;
            no = new Ordinary();
        }
        public Note(Boolean newNote, String fileName, String name)
        {
            InitializeComponent();
            this.newNote = newNote;
            this.fileName = fileName;
            no = new Ordinary(name);
            no.GetXml(fileName);
            no.Display(textBoxName, textBoxMain);
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
                no.SetText(textBoxName, textBoxMain);
                no.SetXml(fileName);
                newNote = false;
            }
            else
            {
                no.Change(textBoxName, textBoxMain, fileName);
                no.SetText(textBoxName, textBoxMain);
            }
        }

    }
}
