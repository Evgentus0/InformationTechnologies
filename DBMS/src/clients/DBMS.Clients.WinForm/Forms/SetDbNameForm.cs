using DBMS.Clients.WinForm.DTO;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm.Forms
{
    public partial class SetDbNameForm : Form
    {
        public bool IsSet { get; set; }
        public DbSettingsDto Data { get; set; }
        private string _path;

        public SetDbNameForm()
        {
            InitializeComponent();

            IsSet = false;

            comboBoxSourceType.DataSource = Enum.GetValues(typeof(SupportedSources));


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsValid => !string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrEmpty(_path);

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                IsSet = true;
                Data = new DbSettingsDto
                {
                    Name = textBoxName.Text,
                    RootPath = _path,
                    FileSize = (long)numericUpDownFIleSize.Value,
                    DefaultSource = (SupportedSources)comboBoxSourceType.SelectedValue
                };

                Close();
            }
            else
            {
                MessageBox.Show(Constants.EnterDbNameForm.EnterData);
            }
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _path = folderBrowserDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("Incorrect path");
            }
        }
    }
}
