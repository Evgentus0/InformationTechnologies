using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBMS_Core.Extentions;
using DBMS_Core.Infrastructure.Factories;

namespace DBMS.Clients.WinForm.Forms
{
    public partial class AddValidatorForm : Form
    {
        private SupportedTypes _type;
        public IValidator Validator { get; set; }
        public bool IsSet { get; set; }
        private object _value;
        public AddValidatorForm(SupportedTypes type)
        {
            InitializeComponent();

            _type = type;
            IsSet = false;

            var tempValidator = (IValidator)Activator.CreateInstance(Type.GetType(type.GetValidatorType()));
            comboBoxOperation.DataSource = Enum.GetValues(Type.GetType(tempValidator.OperationType));
        }

        private bool IsValid()
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxValue.Text))
                    throw new Exception();

                var validator = (IValidator)Activator.CreateInstance(Type.GetType(_type.GetValidatorType()));

                _value = Convert.ChangeType(textBoxValue.Text, Type.GetType(validator.ValueType));

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                IsSet = true;

                Validator = ValidatorsFactory.GetValidator(_type, (int)comboBoxOperation.SelectedItem, _value);

                Close();
            }
            else
            {
                MessageBox.Show(Constants.EnterNewDb.EnterData);
            }
        }
    }
}
