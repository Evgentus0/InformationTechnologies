using DBMS_Core.Comparers;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DBMS.Clients.WinForm.Forms
{
    public partial class UnionTablesForm : Form
    {
        private IDataBaseService _dataBaseService;
        private Settings _settings;
        private List<ITableService> _tables;
        private ITableService _tableService;

        public bool IsSet { get; set; }
        public List<List<object>> Data { get; set; }
        public UnionTablesForm(IDataBaseService dataBaseService, ITableService tableService)
        {
            InitializeComponent();
            _settings = new Settings();

            tableLayoutPanel.RowStyles[0].Height = _settings.DataBaseButtonHeight;
            Data = new List<List<object>>();

            _tableService = tableService;
            _dataBaseService = dataBaseService;
            IsSet = false;
            _tables = new List<ITableService>();
        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            var size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonHeight);
            var guid = Guid.NewGuid();

            var combobox = new ComboBox()
            {
                DataSource = _dataBaseService.GetTables().ToList(),
                Size = size,
                Name = guid.ToString() + "." + nameof(ComboBox)
            };

            var buttonDelete = new Button()
            {
                Text = Constants.UnionForm.DeleteTable,
                Size = size,
                Name = guid.ToString() + "." + nameof(Button)
            };
            buttonDelete.Click += ButtonDelete_Click;

            tableLayoutPanel.Controls.AddRange(new Control[] { combobox, buttonDelete });
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var nameGuid = button.Name.Split('.').First();

            tableLayoutPanel.Controls.Remove(button);
            tableLayoutPanel.Controls.RemoveByKey(nameGuid + "." + nameof(ComboBox));
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            var combobxes = tableLayoutPanel.Controls
                .Cast<Control>()
                .Where(x => x is ComboBox)
                .Select(x => (ComboBox)x)
                .ToList();

            if(combobxes.Count > 0)
            {
                combobxes.ForEach(x => _tables.Add((ITableService)x.SelectedItem));
                _tables = _tables.Distinct(new TableServiceComparer()).ToList();

                _tables.RemoveAll(x => x.Table.Name == _tableService.Table.Name);

                try
                {
                    Data = _tableService.Union(_tables.Select(x => x.Table).ToArray());
                    IsSet = true;
                    Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
    }
}
