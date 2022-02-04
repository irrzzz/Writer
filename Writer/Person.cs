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
    public partial class Person : Form
    {
        private Boolean newNote; // true, если запись новая
        private String fileName;
        private Persons pers;
        public Person(Boolean newNote, String fileName)
        {
            InitializeComponent();
            this.newNote = newNote;
            this.fileName = fileName;
            pers = new Persons();
        }
        public Person(Boolean newNote, String fileName, String name)
        {
            InitializeComponent();
            this.newNote = newNote;
            this.fileName = fileName;
            pers = new Persons(name);
            pers.GetXml(fileName);
            pers.Display(textBoxName, textBoxMain, textBoxAge);
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
                pers.SetText(textBoxName, textBoxMain,textBoxAge);
                pers.SetXml(fileName);
                newNote = false;
            }
            else
            {
                pers.Change(textBoxName, textBoxMain, textBoxAge, fileName);
                pers.SetText(textBoxName, textBoxMain, textBoxAge);
            }
        }
    }
}
