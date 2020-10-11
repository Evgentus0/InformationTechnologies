using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Infrastructure.Validators;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
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
                    if(_fields[j].Type == SupportedTypes.Picture)
                    {
                        var cell = (DataGridViewImageCell)dataGridViewData.Rows[i].Cells[j];

                        var picture = new Picture
                        {
                            Description = cell.Description,
                            Path = (string)cell.Tag
                        };
                        row.Add(picture);
                    }

                    else
                    {
                        string value = dataGridViewData.Rows[i].Cells[j].Value.ToString();
                        try
                        {
                            row.Add(SupportedTypesFactory.GetTypeInstance(_fields[j].Type, value));
                        }
                        catch
                        {
                            Values.Clear();
                            IsSet = false;
                            MessageBox.Show(string.Format(Constants.TableButtonControl.InsertIncorrectData, i + 1, j + 1, value));
                            return;
                        }
                    }
                }
                Values.Add(row); 
            }
            IsSet = true;

            Close();
        }

        private void dataGridViewData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dataGrid = (DataGridView)sender;

            var cell = dataGrid.CurrentCell;

            if (_fields[cell.ColumnIndex].Type == SupportedTypes.Picture)
            {
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        string file = openFileDialog.FileName;
                        var image = Bitmap.FromFile(file);


                        var imageCell = new DataGridViewImageCell();
                        imageCell.Value = image;
                        imageCell.Tag = file;

                        var form = new InputForm(Constants.TableButtonControl.EnterDescription);
                        while (!form.IsSet)
                        {
                            form.ShowDialog();
                        }
                        imageCell.Description = form.Value;
                        dataGrid.Rows[cell.RowIndex].Cells[cell.ColumnIndex] = imageCell;

                        dataGridViewData.Columns[imageCell.ColumnIndex].Width = image.Width;
                        dataGridViewData.Rows[imageCell.RowIndex].Height = image.Height;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(Constants.TableButtonControl.CantOpenFile);
                }
            }
        }
    }
}
