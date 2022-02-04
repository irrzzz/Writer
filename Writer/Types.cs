using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace Writer
{
   class Ordinary
    {
       protected String name; //название записи
       protected String main; //основной текст
        public Ordinary()
        {
            name = "";
            main = "";
        }

        public Ordinary(String name)
        {
            this.name = name;
            main = "";
        }
        public virtual void Display(TextBox name, RichTextBox main)
        {
            name.Text = this.name;
            main.Text = this.main;
        }

        public virtual void GetXml(String fileName)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes["name"].Value == this.name)
                {
                    name = xnode.Attributes["name"].Value.ToString();
                    main = xnode.LastChild.InnerText;
                    break;
                }
            }
        }

        public virtual void SetXml(String fileName)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            XmlAttribute nameAttr = xdoc.CreateAttribute("name");
            XmlElement mainElem = xdoc.CreateElement("main");
            XmlText nameText = xdoc.CreateTextNode(this.name);
            XmlText mainText = xdoc.CreateTextNode(this.main);
            nameAttr.AppendChild(nameText);
            if (fileName == "chapters.xml")
            {
                XmlElement chElem = xdoc.CreateElement("chapter");
                mainElem.AppendChild(mainText);
                chElem.Attributes.Append(nameAttr);
                chElem.AppendChild(mainElem);
                xRoot.AppendChild(chElem);
                xdoc.Save(fileName);
            }
            else
            {
                XmlElement chElem = xdoc.CreateElement("note");
                mainElem.AppendChild(mainText);
                chElem.Attributes.Append(nameAttr);
                chElem.AppendChild(mainElem);
                xRoot.AppendChild(chElem);
                xdoc.Save(fileName);
            }
        }

        public virtual void SetText(TextBox name, RichTextBox main)
        {
            this.name = name.Text;
            this.main = main.Text;
        }

        public virtual void Change(TextBox name, RichTextBox main, String fileName)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            foreach (XmlNode xnode in xRoot) {
                if (xnode.Attributes["name"].Value == this.name)
                {
                    if (name.Text != this.name)
                    {
                        xnode.Attributes["name"].Value = name.Text;
                    }
                    if (main.Text != this.main)
                    {
                        xnode.LastChild.InnerText = main.Text;
                    }
                    xdoc.Save(fileName);
                    break;
                }
            }
        }
    }

    class Persons: Ordinary
    {
        private String age;
        public Persons(): base()
        {
            age = "";
        }
        public Persons(String name): base(name)
        {
            age = "";
        }
        public void Display(TextBox name, RichTextBox main, TextBox age){
            base.Display(name, main);
            age.Text = this.age;
        }

        public void SetText(TextBox name, RichTextBox main, TextBox age)
        {
            base.SetText(name, main);
            this.age = age.Text;
        }

        public override void GetXml(String fileName)
        {
            base.GetXml(fileName);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("persons.xml");
            XmlElement xRoot = xdoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes["name"].Value == this.name)
                {
                    this.age = xnode.FirstChild.InnerText;
                    break;
                }
            }
        }

        public override void SetXml(String fileName)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            XmlAttribute nameAttr = xdoc.CreateAttribute("name");
            XmlElement ageElem = xdoc.CreateElement("age");
            XmlElement mainElem = xdoc.CreateElement("main");
            XmlText nameText = xdoc.CreateTextNode(this.name);
            XmlText ageText = xdoc.CreateTextNode(this.age);
            XmlText mainText = xdoc.CreateTextNode(this.main);
            nameAttr.AppendChild(nameText);
            XmlElement chElem = xdoc.CreateElement("person");
            ageElem.AppendChild(ageText);
            mainElem.AppendChild(mainText);
            chElem.Attributes.Append(nameAttr);
            chElem.AppendChild(ageElem);
            chElem.AppendChild(mainElem);
            xRoot.AppendChild(chElem);
            xdoc.Save("persons.xml");
        }
        public void Change(TextBox name, RichTextBox main,TextBox age, String fileName)
        {
            base.Change(name, main, fileName);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes["name"].Value == this.name)
                {
                    if (age.Text != this.age)
                    {
                        xnode.FirstChild.InnerText = age.Text;
                    }
                    xdoc.Save(fileName);
                    break;
                }
            }
        }
    }

    class Places: Ordinary
    {
        private String address;
        public Places() : base()
        {
            address = "-";
        }

        public Places(String name) : base(name)
        {
            address = "-";
        }
        public void Display(TextBox name, RichTextBox main, TextBox address)
        {
            base.Display(name, main);
            address.Text = this.address;
        }

        public void SetText(TextBox name, RichTextBox main, TextBox address)
        {
            base.SetText(name, main);
            this.address = address.Text;
        }

        public override void GetXml(String fileName)
        {
            base.GetXml(fileName);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes["name"].Value == this.name)
                {
                    address = xnode.FirstChild.InnerText;
                    break;
                }
            }
        }

        public override void SetXml(String fileName)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            XmlAttribute nameAttr = xdoc.CreateAttribute("name");
            XmlElement adElem = xdoc.CreateElement("address");
            XmlElement mainElem = xdoc.CreateElement("main");
            XmlText nameText = xdoc.CreateTextNode(this.name);
            XmlText adText = xdoc.CreateTextNode(this.address);
            XmlText mainText = xdoc.CreateTextNode(this.main);
            nameAttr.AppendChild(nameText);
            XmlElement chElem = xdoc.CreateElement("place");
            adElem.AppendChild(adText);
            mainElem.AppendChild(mainText);
            chElem.Attributes.Append(nameAttr);
            chElem.AppendChild(adElem);
            chElem.AppendChild(mainElem);
            xRoot.AppendChild(chElem);
            xdoc.Save("places.xml");
        }
        public void Change(TextBox name, RichTextBox main, TextBox address, String fileName)
        {
            base.Change(name, main, fileName);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(fileName);
            XmlElement xRoot = xdoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes["name"].Value == this.name)
                {
                    if (address.Text != this.address)
                    {
                        xnode.FirstChild.InnerText = address.Text;
                    }
                    xdoc.Save(fileName);
                    break;
                }
            }
        }
    }
}
