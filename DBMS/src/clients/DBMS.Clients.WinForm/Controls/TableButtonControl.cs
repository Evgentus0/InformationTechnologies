using DBMS.Clients.WinForm.Forms;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm.Controls
{
    internal class TableButtonControl : Button
    {
        private Settings _settings;

        private ITableService _tableService;

        public TableButtonControl(ITableService tableService)
        {
            _settings = new Settings();

            _tableService = tableService;

            Text = tableService.Name;

            InitButton();

            InitContextMeniForDataGrid();
        }

        private List<(string name, Action<object, EventArgs> action)> MenuItemList =>
             new List<(string name, Action<object, EventArgs> action)>()
                {
                    (Constants.TableButtonControl.Select, new Action<object, EventArgs>((o, f) =>
                        {
                            AddTopMenuButtons();
                            FillDataGrid(_tableService.Select(100, 0));
                        })),

                    (Constants.TableButtonControl.EditSchema, new Action<object, EventArgs>((o, f) =>
                        {
                            EditTableShema();
                        }))
                };

        private void AddTopMenuButtons()
        {
            SharedControls.FlowLayoutPanelTopMenu.Controls.Clear();

            var size = new Size(_settings.TopMenuButtonWidth, _settings.SubButtonHeght);

            var buttonConditions = new Button
            {
                Text = Constants.TableButtonControl.AddConditions,
                Size = size
            };
            buttonConditions.Click += ButtonConditions_Click;

            var buttonInsert = new Button
            {
                Text = Constants.TableButtonControl.InsertData,
                Size = size,
            };
            buttonInsert.Click += ButtonInsert_Click;

            var buttonDelete = new Button
            {
                Text = Constants.TableButtonControl.DeleteData,
                Size = size
            };
            buttonDelete.Click += ButtonDelete_Click;

            SharedControls.FlowLayoutPanelTopMenu.Controls.AddRange(new Control[] { buttonConditions, buttonInsert, buttonDelete });
        }

        private void ButtonConditions_Click(object sender, EventArgs e)
        {
            var form = new SelectConditionsForm(_tableService.Fields, false);
            form.ShowDialog();

            if (form.IsSet)
            {
                if (form.SelectConditions.Validators.Any())
                {
                    FillDataGrid(_tableService.Select(form.SelectConditions.Top,
                        form.SelectConditions.Offset, form.SelectConditions.Validators));
                }
                else
                {
                    FillDataGrid(_tableService.Select(form.SelectConditions.Top,
                        form.SelectConditions.Offset));
                }
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var form = new SelectConditionsForm(_tableService.Fields, true);
            form.ShowDialog();

            if (form.IsSet)
            {
                if (form.SelectConditions.Validators.Any())
                {
                    _tableService.DeleteRows(form.SelectConditions.Validators);

                    FillDataGrid(_tableService.Select(100, 0));
                }
            }
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            var form = new InsertForm(_tableService.Fields);
            form.ShowDialog();

            if (form.IsSet)
            {
                _tableService.InsertDataRange(form.Values);

                FillDataGrid(_tableService.Select(100, 0));
            }
        }

        private void EditTableShema()
        {
            new TableSchemaForm(_tableService).ShowDialog();

            _tableService.UpdateSchema();
        }

        private void FillDataGrid(IEnumerable<List<object>> rows)
        {
            var dataGrid = SharedControls.DataGrigTable;
            dataGrid.Rows.Clear();

            FillColumnHeaders();

            var fieldCount = _tableService.Fields.Count() + 1;
            var rowsCount = rows.Count();
            if (rowsCount < 1)
            {
                MessageBox.Show(Constants.TableButtonControl.EmptyTable);
            }
            else
            {
                dataGrid.RowCount = rowsCount;
                int rowCount = 0;

                foreach (var row in rows)
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        dataGrid.Rows[rowCount].Cells[i].Value = row[i];
                    }
                    rowCount++;
                }
                dataGrid.Columns[0].Visible = false;
            }
        }

        private void FillColumnHeaders()
        {
            var dataGrid = SharedControls.DataGrigTable;
            var fieldCount = _tableService.Fields.Count();
            dataGrid.ColumnCount = fieldCount + 1;

            for (int i = 0; i < fieldCount; i++)
            {
                dataGrid.Columns[i + 1].Name = _tableService.Fields[i].Name;
            }
        }

        private void InitButton()
        {
            Size = new Size(_settings.LeftSideButtonWidth, _settings.SubButtonHeght);
            Dock = DockStyle.Bottom;

            InitContextMenu();
        }

        private void InitContextMenu()
        {
            var contextMenu = CommonControlsHelper.GetContextMenuStrip(MenuItemList);

            ContextMenuStrip = contextMenu;
        }

        private void InitContextMeniForDataGrid()
        {
            var contextMenu = CommonControlsHelper.GetContextMenuStrip(DataGridMenuList);

            SharedControls.DataGrigTable.ContextMenuStrip = contextMenu;
        }

        private List<(string name, Action<object, EventArgs> action)> DataGridMenuList =>
            new List<(string name, Action<object, EventArgs> action)>
            {
                (Constants.MainForm.DeleteSelectedRows, new Action<object, EventArgs>((o, f) =>
                    {
                        DeleteSelectedRows();
                    })),
                (Constants.MainForm.UspdateRow, new Action<object, EventArgs>((o, f) =>
                    {
                        UpdateSelectedRows();
                    })),
            };

        private void UpdateSelectedRows()
        {
            var values = new List<List<object>>();

            var dataGridViewData = SharedControls.DataGrigTable;

            for (int i = 0; i < dataGridViewData.SelectedRows.Count; i++)
            {
                var row = new List<object>();
                row.Add(Guid.Parse(dataGridViewData.SelectedRows[i].Cells[0].Value.ToString()));

                for (var j = 1; j < dataGridViewData.Columns.Count; j++)
                {
                    string value = dataGridViewData.SelectedRows[i].Cells[j].Value.ToString();
                    try
                    {
                        row.Add(SupportedTypesFactory.GetTypeInstance(_tableService.Fields[j - 1].Type, value));
                    }
                    catch
                    {
                        MessageBox.Show(string.Format(Constants.TableButtonControl.InsertIncorrectData, i + 1, j, value));
                        return;
                    }
                }
                values.Add(row);
            }

            _tableService.UpdateRows(values);
        }

        private void DeleteSelectedRows()
        {
            var dataGrid = SharedControls.DataGrigTable;

            var ids = dataGrid.SelectedRows.Cast<DataGridViewRow>().Select(x => Guid.Parse(x.Cells[0].Value.ToString()));

            foreach (DataGridViewRow row in dataGrid.SelectedRows)
            {
                dataGrid.Rows.Remove(row);
            }

            _tableService.DeleteRows(ids.ToList());
        }
    }
}
