using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm.Managers
{
    public partial class SelectDbForm : Form
    {
        public bool IsSet { get; set; }
        public string DbName { get; set; }
        public SelectDbForm(List<string> dbNames)
        {
            InitializeComponent();

            IsSet = false;
            comboBoxDbName.DataSource = dbNames;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DbName = (string)comboBoxDbName.SelectedItem;
            IsSet = true;
            Close();
        }
    }
}
