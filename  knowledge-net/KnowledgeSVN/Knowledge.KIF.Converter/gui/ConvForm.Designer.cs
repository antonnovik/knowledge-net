using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EnvDTE;
using Knowledge.KIF.Converter.converters;
using Knowledge.KIF.Converter.formatters;
using Knowledge.KIF.Converter.model;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.gui {
    partial class ConvForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._outputFormatLabel = new System.Windows.Forms.Label();
            this._outputPathLabel = new System.Windows.Forms.Label();
            this._outputFormatBox = new System.Windows.Forms.ComboBox();
            this._browseButton = new System.Windows.Forms.Button();
            this._outputPathField = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(163, 104);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 0;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;

            this._okButton.Click += new System.EventHandler(this.okButtonClick);

            // 
            // _cancelButton
            // 
            this._cancelButton.Location = new System.Drawing.Point(244, 104);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _outputFormatLabel
            // 
            this._outputFormatLabel.AutoSize = true;
            this._outputFormatLabel.Location = new System.Drawing.Point(12, 24);
            this._outputFormatLabel.Name = "_outputFormatLabel";
            this._outputFormatLabel.Size = new System.Drawing.Size(74, 13);
            this._outputFormatLabel.TabIndex = 2;
            this._outputFormatLabel.Text = "Output format:";
            // 
            // _outputPathLabel
            // 
            this._outputPathLabel.AutoSize = true;
            this._outputPathLabel.Location = new System.Drawing.Point(12, 63);
            this._outputPathLabel.Name = "_outputPathLabel";
            this._outputPathLabel.Size = new System.Drawing.Size(66, 13);
            this._outputPathLabel.TabIndex = 3;
            this._outputPathLabel.Text = "Output path:";
            // 
            // _outputFormatBox
            // 
      //      this._outputFormatBox.FormattingEnabled = true;
        /*    this._outputFormatBox.Items.AddRange(new object[] {
                    "Ontolingua",
                    "KIF"
            });*/
            this._outputFormatBox.Items.Add("KIF");
            this._outputFormatBox.Items.Add("Ontolingua");

            this._outputFormatBox.DropDownStyle = ComboBoxStyle.DropDownList;
            //TODO: disable text box _outputFormatBox.ge

            this._outputFormatBox.Location = new System.Drawing.Point(89, 21);
            this._outputFormatBox.Name = "_outputFormatBox";
            this._outputFormatBox.Size = new System.Drawing.Size(230, 21);
            this._outputFormatBox.TabIndex = 4;
            this._outputFormatBox.SelectedIndexChanged += new System.EventHandler(this.outputFormatBox_SelectedIndexChanged);
            this._outputFormatBox.SelectedIndex = 0;
//            this._outputFormatBox.Ena; TODO: disable editing

            // 
            // _browseButton
            // 
            this._browseButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._browseButton.Location = new System.Drawing.Point(295, 59);
            this._browseButton.Name = "_browseButton";
            this._browseButton.Size = new System.Drawing.Size(24, 23);
            this._browseButton.TabIndex = 5;
            this._browseButton.Text = "...";
            this._browseButton.UseVisualStyleBackColor = true;

            this._browseButton.Click += new System.EventHandler(this.browseButtonClick);

          //  this._browseButton.MouseEnter += new System.EventHandler(this.Buttons_OnMouseEnter);
          //  this._browseButton.MouseLeave += new System.EventHandler(this.Buttons_OnMouseLeave);
            // 
            // _outputPathField
            // 
            this._outputPathField.Location = new System.Drawing.Point(89, 60);
            this._outputPathField.Name = "_outputPathField";
            this._outputPathField.Size = new System.Drawing.Size(200, 20);
            this._outputPathField.TabIndex = 6;
            // 
            // ConvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 141);
            this.Controls.Add(this._outputPathField);
            this.Controls.Add(this._browseButton);
            this.Controls.Add(this._outputFormatBox);
            this.Controls.Add(this._outputPathLabel);
            this.Controls.Add(this._outputFormatLabel);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);

            //my
            this.AcceptButton = _okButton;
            this.CancelButton = _cancelButton;
            //my

            this.Name = "ConvForm";
            this.Text = "Convert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void browseButtonClick(object sender, EventArgs e) {
            OpenFileDialog ofn = new OpenFileDialog();
            ofn.Filter = "KIF Files (*.kif)|*.kif|All Files (*.*)|*.*";
            ofn.Title = "Choose output file";
            ofn.CheckFileExists = false;

            if (ofn.ShowDialog() == DialogResult.OK) {
                _outputPathField.Text = ofn.FileName;
                //MessageBox.Show(ofn.FileName, "File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string[] CONVERTER_TYPES = { ModelBuilderFactory.KIF, ModelBuilderFactory.ONTOLINGUA };//TODO:

        private void okButtonClick(object sender, EventArgs e) {
            //TODO: validate();
            string fileName = _outputPathField.Text;

            string converterType = CONVERTER_TYPES[_outputFormatBox.SelectedIndex];
//            IConverter converter = ConverterFactory.getInstance().getConverter(converterType);

            //TODO: ask rewrite if exists
            IOUtils.createOrRewriteFile(fileName);
  /*          IWriter writer = WriterFactory.createFileWriter(fileName);
            writer.open();*/
            StreamWriter writer = new StreamWriter(fileName);

            IModelBuilder modelBuilder = ModelBuilderFactory.getInstance().createBuilder(converterType);
            converters.Converter converter = new converters.Converter(modelBuilder);
            IModel model = converter.convert(ExpComp.dataFrames.Values);
            SimpleKifFormatter formatter = new SimpleKifFormatter();
            formatter.write(model, writer);            
            
            
//            ICollection<Object> convertedItems = converter.convert(getSourceModel());

//            writer.write(convertedItems);
            writer.Flush();
            writer.Close();
            Close();
            //ACTION

            DialogResult reply = MessageBox.Show("Would you like to view generated file?",
                       "Transformation finished", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//TODO:

            if (reply == DialogResult.Yes) {
                Window textEditor = _environment.OpenFile(Constants.vsViewKindCode, fileName);
                textEditor.Visible = true;
                textEditor.Activate();
            } 
        }

        private ICollection getSourceModel() {
            //TODO: 
            ExpComp.Clear();
            Scanner scanner = new Scanner(_filePath);
            ExpComp.parser = new Parser(scanner);
            ExpComp.parser.Parse();

            return ExpComp.dataFrames.Values;
        }

        private void Buttons_OnMouseEnter(object sender, EventArgs e) {
            Button btn = (Button) sender;
            btn.BackColor = Color.LightGray;
        }

        private void Buttons_OnMouseLeave(object sender, EventArgs e) {
            Button btn = (Button) sender;
            btn.BackColor = SystemColors.Control;
        }

        private Button _okButton;
        private Button _cancelButton;
        private Label _outputFormatLabel;
        private Label _outputPathLabel;
        private ComboBox _outputFormatBox;
        private Button _browseButton;
        private TextBox _outputPathField;
    }
}