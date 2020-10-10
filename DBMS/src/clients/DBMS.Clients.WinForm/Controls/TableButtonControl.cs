using DBMS.Clients.WinForm.Forms;
using DBMS_Core.Extentions;
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
        private IDataBaseService _dataBaseService;

        public TableButtonControl(ITableService tableService, IDataBaseService dataBaseService)
        {
            _settings = new Settings();

            _tableService = tableService;
            _dataBaseService = dataBaseService;

            Text = tableService.Table.Name;

            InitButton();

            InitContextMeniForDataGrid();
        }

        private List<(string name, Action<object, EventArgs> action)> MenuItemList =>
             new List<(string name, Action<object, EventArgs> action)>()
                {
                    (Constants.TableButtonControl.Select, new Action<object, EventArgs>((o, f) =>
                        {
                            AddTopMenuButtons();
                            FillColumnHeaders(_tableService.Table.Schema.Fields.Select(x => x.Name).ToList());
                            FillDataGrid(_tableService.Select(100, 0));
                        })),

                    (Constants.TableButtonControl.EditSchema, new Action<object, EventArgs>((o, f) =>
                        {
                            EditTableShema();
                        }))
                };

        private void AddTopMenuButtons()
        {
            SharedControls.FlowLayoutPanelTopMenu.Controls
                .Cast<Control>()
                .Where(x => x.Name.Contains(nameof(TableButtonControl)))
                .ToList().ForEach(x => SharedControls.FlowLayoutPanelTopMenu.Controls.Remove(x));
            
            var size = new Size(_settings.TopMenuButtonWidth, _settings.SubButtonHeght);

            var buttonConditions = new Button
            {
                Text = Constants.TableButtonControl.AddConditions,
                Size = size,
                Name = nameof(TableButtonControl)
            };
            buttonConditions.Click += ButtonConditions_Click;

            var buttonInsert = new Button
            {
                Text = Constants.TableButtonControl.InsertData,
                Size = size,
                Name = nameof(TableButtonControl)
            };
            buttonInsert.Click += ButtonInsert_Click;

            var buttonDelete = new Button
            {
                Text = Constants.TableButtonControl.DeleteData,
                Size = size,
                Name = nameof(TableButtonControl)
            };
            buttonDelete.Click += ButtonDelete_Click;

            var buttonUnion = new Button
            {
                Text = Constants.TableButtonControl.UnionTables,
                Size = size,
                Name = nameof(TableButtonControl)
            };
            buttonUnion.Click += ButtonUnion_Click; ;

            SharedControls.FlowLayoutPanelTopMenu.Controls.AddRange(new Control[] 
            { 
                buttonConditions, 
                buttonInsert, 
                buttonDelete, 
                buttonUnion 
            });
        }

        private void ButtonUnion_Click(object sender, EventArgs e)
        {
            var form = new UnionTablesForm(_dataBaseService, _tableService);
            form.ShowDialog();
            if (form.IsSet)
            {
                List<string> headers = new List<string>();
                for(int i=0;i< _tableService.Table.Schema.Fields.Count; i++)
                {
                    headers.Add($"Column {i + 1}({_tableService.Table.Schema.Fields[i].Type.GetName()})");
                }

                FillColumnHeaders(headers);
                FillDataGrid(form.Data);
            }
        }

        private void ButtonConditions_Click(object sender, EventArgs e)
        {
            var form = new SelectConditionsForm(_tableService.Table.Schema.Fields, false);
            form.ShowDialog();

            if (form.IsSet)
            {
                FillColumnHeaders(_tableService.Table.Schema.Fields.Select(x => x.Name).ToList());

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
            var form = new SelectConditionsForm(_tableService.Table.Schema.Fields, true);
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
            var form = new InsertForm(_tableService.Table.Schema.Fields);
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

            var fieldCount = _tableService.Table.Schema.Fields.Count() + 1;
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

        private void FillColumnHeaders(List<string> headers)
        {
            var dataGrid = SharedControls.DataGrigTable;
            var columnCount = headers.Count();
            dataGrid.ColumnCount = headers.Count + 1;

            for (int i = 0; i < columnCount; i++)
            {
                dataGrid.Columns[i + 1].Name = headers[i];
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
                        row.Add(SupportedTypesFactory.GetTypeInstance(_tableService.Table.Schema.Fields[j - 1].Type, value));
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
