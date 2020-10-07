using DBMS_Core.Infrastructure.Validators;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DBMS.Clients.WinForm.Forms
{
    public partial class InsertForm : Form
    {
        public bool IsSet { get; set; }

        public List<List<object>> Values { get; set; }
        private List<Field> _fields;

        public InsertForm(List<Field> fields)
        {
            InitializeComponent();

            _fields = fields;
            SetHeader();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetHeader()
        {
            var fieldCount = dataGridViewData.ColumnCount = _fields.Count;

            for (int i = 0; i < fieldCount; i++)
            {
                dataGridViewData.Columns[i].Name = _fields[i].Name;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Values = new List<List<object>>();

            for (int i = 0; i < dataGridViewData.Rows.Count - 1; i++)
            {
                var row = new List<object>();

                for(var j = 0; j < dataGridViewData.Columns.Count; j++)
                {
                    object value = dataGridViewData.Rows[i].Cells[j].Value;
                    if(TypeValidator.IsValidValue(_fields[j].Type, value))
                    {
                        row.Add(value);
                    }
                    else
                    {
                        MessageBox.Show(string.Format(Constants.TableButtonControl.InsertIncorrectData, i + 1, j + 1, value));
                        return;
                    }
                }
                Values.Add(row); 
            }
        }
    }
}
