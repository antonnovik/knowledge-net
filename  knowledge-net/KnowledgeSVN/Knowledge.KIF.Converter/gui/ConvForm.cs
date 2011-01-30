using System;
using System.Windows.Forms;
using EnvDTE80;

namespace Knowledge.KIF.Converter.gui {

    public partial class ConvForm : Form {

        private string _filePath = null;
        private DTE2 _environment;
        public ConvForm(string filePath, DTE2 environment) {
            InitializeComponent();
            _filePath = filePath;
            _environment = environment;
        }

        public DTE2 Environment {
            get { return _environment; }
        }

        private void outputFormatBox_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}