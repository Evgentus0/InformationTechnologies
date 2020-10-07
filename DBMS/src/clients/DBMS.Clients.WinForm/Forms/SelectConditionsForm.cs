using DBMS.Clients.WinForm.DTO;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm.Forms
{
    public partial class SelectConditionsForm : Form
    {
        public bool IsSet { get; set; }
        public SelectConditionsDto SelectConditions { get; set; }

        private Settings _settings;
        private List<Field> _fields;

        private const string _deleteButton = "deleteButton", _validatorsButton = "validatorButton";

        private Dictionary<string, Field> _tempFields;
        public SelectConditionsForm(List<Field> fields, bool hideTopOffsetGroup)
        {
            InitializeComponent();

            groupBox1.Visible = !hideTopOffsetGroup;

            _fields = fields;
            IsSet = false;
            _settings = new Settings();
            _tempFields = new Dictionary<string, Field>();

            SelectConditions = new SelectConditionsDto();

            SetHeaders();
        }

        private void SetHeaders()
        {
            tableLayoutPanelConditions.RowStyles[0].Height = _settings.DataBaseButtonHeight;

            tableLayoutPanelConditions.Controls.Add(new Label()
            {
                Text = Constants.SelectConditions.DeleteCondition,
            });

            tableLayoutPanelConditions.Controls.Add(new Label()
            {
                Text = Constants.SelectConditions.FieldName,
                Dock = DockStyle.Fill
            });

            tableLayoutPanelConditions.Controls.Add(new Label()
            {
                Text = Constants.TableSchemaForm.Validators,
                Dock = DockStyle.Fill
            });
        }

        private void buttonAddCondition_Click(object sender, EventArgs e)
        {
            var size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonHeight);
            var guid = Guid.NewGuid();

            _tempFields.Add(guid.ToString(), new Field());

            var deleteButton = new Button
            {
                Text = Constants.TableSchemaForm.DeleteField,
                Size = size,
                Name = guid.ToString() + "." + _deleteButton
            };
            deleteButton.Click += DeleteButton_Click;

            var combobox = new ComboBox
            {
                DataSource = _fields,
                Size = size,
                Name = guid.ToString() + "." + nameof(ComboBox)
            };

            var validatorsButton = new Button
            {
                Text = Constants.TableSchemaForm.Validators,
                Size = size,
                Name = guid.ToString() + "." + _validatorsButton
            };
            validatorsButton.Click += ValidatorsButton_Click;

            tableLayoutPanelConditions.Controls.AddRange(new Control[] { deleteButton, combobox, validatorsButton });
        }

        private void ValidatorsButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var name = button.Name;
            var combobox = (ComboBox)tableLayoutPanelConditions.Controls.Find(name.Split('.').First() +"."+ nameof(ComboBox), false).First();

            var field = (Field)combobox.SelectedItem;

            var validators = SelectConditions.Validators.ContainsKey(field.Name) ?
                SelectConditions.Validators[field.Name] : new List<DBMS_Core.Interfaces.IValidator>();

            var form = new ValidatorsForm(validators, field.Type, false);
            form.ShowDialog();

            if (form.IsChanged)
            {
                if (SelectConditions.Validators.ContainsKey(field.Name))
                {
                    SelectConditions.Validators[field.Name] = form.Validators;
                }
                else
                {
                    SelectConditions.Validators.Add(field.Name, form.Validators);
                }

                if (form.Validators.Count > 0)
                {
                    combobox.Enabled = false;
                }
                else
                {
                    combobox.Enabled = true;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            IsSet = true;

            SelectConditions.Top = (int)numericUpDownTop.Value;
            SelectConditions.Offset = (int)numericUpDownOffset.Value;

            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var name = button.Name;

            var combobox = (ComboBox)tableLayoutPanelConditions.Controls.Find(name.Split('.').First() + "." + nameof(ComboBox), false).First();
            var field = (Field)combobox.SelectedItem;

            tableLayoutPanelConditions.Controls.Remove(combobox);
            tableLayoutPanelConditions.Controls.RemoveByKey(name + "." + _validatorsButton);
            tableLayoutPanelConditions.Controls.Remove(button);

            if (SelectConditions.Validators.ContainsKey(field.Name))
            {
                SelectConditions.Validators.Remove(field.Name);
            }
        }
    }
}
