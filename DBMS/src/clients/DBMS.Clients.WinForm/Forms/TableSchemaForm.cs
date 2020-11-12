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
using System.Linq;
using DBMS_Core.Extentions;
using DBSM.Manager.Interfaces;

namespace DBMS.Clients.WinForm.Forms
{
    public partial class TableSchemaForm : Form
    {
        private ITableManager _tableService;
        private Settings _settings;

        private IServiceProvider _serviceProvider;

        public TableSchemaForm(ITableManager tableService, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            _settings = new Settings();

            _tableService = tableService;

            SetFields();
        }

        private void SetFields()
        {
            tableLayoutPanelMain.Controls.Clear();

            InsertHeader();

            foreach (var field in _tableService.Table.Schema.Fields)
            {
                AddExistingFieldRedactor(field);
            }
        }

        private void InsertHeader()
        {
            tableLayoutPanelMain.RowStyles[0].Height = _settings.DataBaseButtonHeight;

            var addFieldButton = new Button()
            {
                Text = Constants.TableSchemaForm.AddField,
                Size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonWidth)
            };

            addFieldButton.Click += AddFieldButton_Click;
            tableLayoutPanelMain.Controls.Add(addFieldButton);


            tableLayoutPanelMain.Controls.Add(new Label() 
            { 
                Text = Constants.TableSchemaForm.FieldName, 
                Dock = DockStyle.Fill 
            });
            tableLayoutPanelMain.Controls.Add(new Label() 
            { 
                Text = Constants.TableSchemaForm.FieldType, 
                Dock = DockStyle.Fill 
            });
            tableLayoutPanelMain.Controls.Add(new Label() 
            { 
                Text = Constants.TableSchemaForm.FieldValidators, 
                Dock = DockStyle.Fill 
            });
        }

        private void AddFieldButton_Click(object sender, EventArgs e)
        {
            var form = new AddFieldForm(_serviceProvider);

            form.ShowDialog();

            if (form.IsSet)
            {
                _tableService.AddNewField(form.Field);
                AddExistingFieldRedactor(form.Field);
            }
        }

        private void AddExistingFieldRedactor(Field field)
        {
            var size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonHeight);

            var delteButton = new Button() { Text = Constants.TableSchemaForm.DeleteField, Tag = field, 
                Size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonWidth) };
            delteButton.Click += DeleteButton_Click;

            var textbox = new TextBox() {Name="Name"+field.Name, Text = field.Name, Size = size, Tag = field};
            textbox.LostFocus += Textbox_LostFocus;

            var textboxType = new TextBox() { Text = field.Type.GetName(), Size = size, Enabled = false};

            var button = new Button() { Text = Constants.TableSchemaForm.Validators, Tag = field, Size = size };
            button.Click += ValidatorsButton_Click;
                
            tableLayoutPanelMain.Controls.AddRange(new Control[] { delteButton, textbox, textboxType, button });
        }

        private void Textbox_LostFocus(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;

            var field = (Field)(textbox).Tag;

            field.Name = textbox.Text;
        }

        private void ValidatorsButton_Click(object sender, EventArgs e)
        {
            var field = (Field)((Button)sender).Tag;

            var form = new ValidatorsForm(field.Validators, field.Type, true, _serviceProvider);

            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;

            var field = (Field)button.Tag;

            _tableService.DeleteField(field.Name);

            SetFields();
        }
    }
}
