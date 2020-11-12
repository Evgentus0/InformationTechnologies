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

        private bool IsValid => !string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrEmpty(textBoxPath.Text);

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                IsSet = true;
                Data = new DbSettingsDto
                {
                    Name = textBoxName.Text,
                    RootPath = textBoxPath.Text,
                    FileSize = (long)numericUpDownFIleSize.Value,
                    DefaultSource = (SupportedSources)comboBoxSourceType.SelectedValue
                };

                Close();
            }
            else
            {
                MessageBox.Show(Constants.EnterNewDb.EnterData);
            }
        }
    }
}
