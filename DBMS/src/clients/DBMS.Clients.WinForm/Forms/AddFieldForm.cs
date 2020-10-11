using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm.Forms
{
    public partial class AddFieldForm : Form
    {
        public Field Field { get; set; }
        public bool IsSet { get; set; }
        private List<IValidator> validators;

        public AddFieldForm()
        {
            InitializeComponent();

            validators = new List<IValidator>();
            comboBoxType.DataSource = Enum.GetValues(typeof(SupportedTypes));

            IsSet = false;
            Field = new Field();
        }

        private bool IsValid => !string.IsNullOrEmpty(textBoxName.Text);

        private void buttonValidators_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedItem != null)
            {
                var form = new ValidatorsForm(validators, (SupportedTypes)comboBoxType.SelectedItem, false);

                form.ShowDialog();

                if (form.IsChanged)
                {
                    validators = form.Validators;
                    if (validators.Count > 0)
                    {
                        comboBoxType.Enabled = false;
                    }
                    else
                    {
                        comboBoxType.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(Constants.TableSchemaForm.ChooseType);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                IsSet = true;

                Field.Name = textBoxName.Text;
                Field.Type = (SupportedTypes)comboBoxType.SelectedItem;
                Field.Validators = validators;

                Close();
            }

            else
            {
                MessageBox.Show(Constants.EnterNewDb.EnterData);
            }
        }
    }
}
