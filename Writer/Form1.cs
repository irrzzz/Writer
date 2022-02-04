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
using Writer;

namespace Writer
{
    public partial class Form1 : Form
    {
        private String fileName;
        public Form1()
        {
            InitializeComponent();
            fileName = "";
        }

        //заполнение списка записей
        public void fullDGV()
        {
            dataGridView1.Rows.Clear();
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.Load(fileName);
                XmlElement xRoot = xdoc.DocumentElement;
                int i = 0;
                foreach (XmlNode xnode in xRoot)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = xnode.Attributes["name"].Value.ToString();
                    i++;
                }
            }
            catch
            {
                XmlTextWriter textWritter = new XmlTextWriter(fileName, null);
                textWritter.WriteStartDocument();
                textWritter.WriteStartElement("elem");
                textWritter.WriteEndElement();
                textWritter.Close();
            }
        }

        private void chapter_Click(object sender, EventArgs e)
        {

            fileName = "chapters.xml";
            fullDGV();
            buttonAdd.Enabled = true;
            buttonDelete.Enabled = true;
            chapter.Enabled = false;
            place.Enabled = true;
            person.Enabled = true;
            note.Enabled = true;
        }

        private void person_Click(object sender, EventArgs e)
        {
            fileName = "persons.xml";
            fullDGV();
            buttonAdd.Enabled = true;
            buttonDelete.Enabled = true;
            person.Enabled = false;
            chapter.Enabled = true;
            place.Enabled = true;
            note.Enabled = true;
        }

        private void place_Click(object sender, EventArgs e)
        {
            fileName = "places.xml";
            fullDGV();
            buttonAdd.Enabled = true;
            buttonDelete.Enabled = true;
            place.Enabled = false;
            chapter.Enabled = true;
            person.Enabled = true;
            note.Enabled = true;
        }

        private void note_Click(object sender, EventArgs e)
        {
            fileName = "notes.xml";
            fullDGV();
            buttonAdd.Enabled = true;
            buttonDelete.Enabled = true;
            note.Enabled = false;
            chapter.Enabled = true;
            person.Enabled = true;
            place.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (fileName == "chapters.xml")
                {
                    Chapter a = new Chapter(false, fileName, dataGridView1[0, e.RowIndex].Value.ToString());
                    a.ShowDialog();
                }
                else if (fileName == "persons.xml")
                {
                    Person a = new Person(false, fileName, dataGridView1[0, e.RowIndex].Value.ToString());
                    a.ShowDialog();
                }
                else if (fileName == "places.xml")
                {
                    Place a = new Place(false, fileName, dataGridView1[0, e.RowIndex].Value.ToString());
                    a.ShowDialog();
                }
                else
                {
                    Note a = new Note(false, fileName, dataGridView1[0, e.RowIndex].Value.ToString());
                    a.ShowDialog();
                }
            fullDGV();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if(fileName == "chapters.xml")
            {
                Chapter a = new Chapter(true, fileName);
                a.ShowDialog();
            }
            else if(fileName == "persons.xml")
            {
                Person a = new Person(true, fileName);
                a.ShowDialog();
            }
            else if(fileName == "places.xml")
            {
                Place a = new Place(true,fileName);
                a.ShowDialog();
            }
            else
            {
                Note a = new Note(true,fileName);
                a.ShowDialog();
            }
            fullDGV();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {

                    foreach (XmlNode n in xRoot)
                    {
                        if (dataGridView1[0,row.Index].Value.ToString() == n.Attributes["name"].Value)
                        {
                            xRoot.RemoveChild(n);
                            break;
                        }
                    }
                }
            }
            xdoc.Save(fileName);
            fullDGV();
        }
    }
}
