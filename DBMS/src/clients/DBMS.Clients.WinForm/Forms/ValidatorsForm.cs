using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace DBMS.Clients.WinForm.Forms
{
    public partial class ValidatorsForm : Form
    {
        public bool IsChanged { get; set; }
        public List<IValidator> Validators { get; set; }
        private SupportedTypes _type;
        private Settings _settings;

        private bool _isDisabled;

        public ValidatorsForm(List<IValidator> validators, SupportedTypes type, bool isDisabled)
        {
            InitializeComponent();
            Validators = validators ?? new List<IValidator>();
            _settings = new Settings();
            _type = type;
            IsChanged = false;
            _isDisabled = isDisabled;

            SetValidators();

            if (_isDisabled)
            {
                foreach (var control in tableLayoutPanelMain.Controls)
                {
                    ((Control)control).Enabled = false;
                }
            }
        }

        private void SetValidators()
        {
            tableLayoutPanelMain.Controls.Clear();

            InsertHeader();

            Validators.ForEach(x => AddValidatorsToTable(x));
        }

        private void InsertHeader()
        {
            tableLayoutPanelMain.RowStyles[0].Height = _settings.DataBaseButtonHeight;

            var addVlidatorButton = new Button()
            {
                Text = Constants.ValidatorsForm.AddValidator,
                Size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonWidth)
            };

            addVlidatorButton.Click += AddValidator_Click;
            tableLayoutPanelMain.Controls.Add(addVlidatorButton);


            tableLayoutPanelMain.Controls.Add(new Label()
            {
                Text = Constants.ValidatorsForm.Operation,
                Dock = DockStyle.Fill
            });
            tableLayoutPanelMain.Controls.Add(new Label()
            {
                Text = Constants.ValidatorsForm.Value,
                Dock = DockStyle.Fill
            });
        }

        private void AddValidatorsToTable(IValidator validator)
        {
            var size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonHeight);

            var deleteButton = new Button()
            {
                Text = Constants.ValidatorsForm.DeleteValidator,
                Tag = validator,
                Size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonWidth),
            };
            deleteButton.Click += DeleteButton_Click;

            var textboxOperation = new TextBox()
            {
                Text = Enum.ToObject(Type.GetType(validator.OperationType), validator.Operation).ToString(),
                Size = size,
                Enabled = false
            };
            var textboxValue = new TextBox() { Text = validator.Value.ToString(), Size = size, Enabled = false };

            tableLayoutPanelMain.Controls.AddRange(new Control[] { deleteButton, textboxOperation, textboxValue });
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var validator = (IValidator)((Button)sender).Tag;

            Validators.Remove(validator);
            IsChanged = true;

            SetValidators();
        }

        private void AddValidator_Click(object sender, EventArgs e)
        {
            var form = new AddValidatorForm(_type);
            form.ShowDialog();

            if (form.IsSet)
            {
                IsChanged = true;
                Validators.Add(form.Validator);

                SetValidators();
            }
        }
    }
}
