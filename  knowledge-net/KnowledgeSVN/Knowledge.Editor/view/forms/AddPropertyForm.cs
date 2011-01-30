using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Knowledge.Editor.View
{
    public partial class AddPropertyForm : Form
    {
        public AddPropertyForm()
        {
            InitializeComponent();
        }

        public void setPropertyForm(string name, ComboBox comboBox, ListBox listBox1, ListBox listBox2) { 
            this.Name = Name;
            foreach (object item in comboBox.Items){
                this.comboBox1.Items.Add(item.ToString());
            }
            foreach (object item in listBox1.Items)
            {
                this.listBox1.Items.Add(item.ToString());
            }
            foreach (object item in listBox2.Items)
            {
                this.listBox2.Items.Add(item.ToString());
            }
        }
    }
}