using DBMS.Clients.WinForm.Forms;
using DBMS_Core.Extentions;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using DBSM.Manager.Interfaces;
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

        private ITableManager _tableService;
        private IDbManager _dataBaseService;

        private OpenFileDialog openFile;
        private DataGridView dataGrid;

        public TableButtonControl(ITableManager tableService, IDbManager dataBaseService)
        {
            _settings = new Settings();

            _tableService = tableService;
            _dataBaseService = dataBaseService;
            openFile = new OpenFileDialog();

            dataGrid = new DataGridView();
            dataGrid.CellBeginEdit += DataGrigTable_CellBeginEdit;
            dataGrid.Dock = DockStyle.Fill;
            dataGrid.AllowUserToAddRows = false;

            Text = tableService.Table.Name;

            InitButton();

            InitContextMeniForDataGrid();

            Click += TableButtonControl_Click;
        }

        private void TableButtonControl_Click(object sender, EventArgs e)
        {
            SelectTable();
        }

        private void SelectTable()
        {
            SharedControls.GroupBoxData.Controls.Clear();
            SharedControls.GroupBoxData.Controls.Add(dataGrid);

            AddTopMenuButtons();
            FillColumnHeaders(_tableService.Table.Schema.Fields.Select(x => x.Name).ToList());
            FillDataGrid(_tableService.Select(100, 0));
        }

        private void DataGrigTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dataGridCurrent = (DataGridView)sender;

            var cell = dataGridCurrent.CurrentCell;

            if (_tableService.Table.Schema.Fields[cell.ColumnIndex - 1].Type == SupportedTypes.Picture)
            {
                var result = openFile.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        string file = openFile.FileName;
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
                        dataGridCurrent.Rows[cell.RowIndex].Cells[cell.ColumnIndex] = imageCell;

                        dataGrid.Columns[imageCell.ColumnIndex].Width = image.Width;
                        dataGrid.Rows[imageCell.RowIndex].Height = image.Height;
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

        private List<(string name, Action<object, EventArgs> action)> MenuItemList =>
             new List<(string name, Action<object, EventArgs> action)>()
                {
                    (Constants.TableButtonControl.Select, new Action<object, EventArgs>((o, f) =>
                        {
                            SelectTable();
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
                for (int i = 0; i < _tableService.Table.Schema.Fields.Count; i++)
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
            dataGrid.Rows.Clear();

            var fields = _tableService.Table.Schema.Fields;
            var fieldCount = fields.Count() + 1;
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
                        if (i > 0 && fields[i - 1].Type == SupportedTypes.Picture && row[i] != null)
                        {
                            var picture = (Picture)SupportedTypesFactory.GetTypeInstance(SupportedTypes.Picture, row[i].ToString());
   
                            var image = Bitmap.FromFile(picture.Path);
                            var imageCell = new DataGridViewImageCell();
                            imageCell.Value = image;

                            dataGrid.Rows[rowCount].Cells[i] = imageCell;

                            dataGrid.Rows[rowCount].Height = image.Height;
                            dataGrid.Columns[i].Width = image.Width;
                        }
                        else
                        {
                            dataGrid.Rows[rowCount].Cells[i].Value = row[i];
                        }
                    }
                    rowCount++;
                }
                dataGrid.Columns[0].Visible = false;
            }
        }

        private void FillColumnHeaders(List<string> headers)
        {
            var columnCount = headers.Count();
            dataGrid.ColumnCount = headers.Count + 1;

            for (int i = 0; i < columnCount; i++)
            {
                dataGrid.Columns[i + 1].Name = headers[i];
            }
            dataGrid.Columns[0].Visible = false;
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

            dataGrid.ContextMenuStrip = contextMenu;
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

            for (int i = 0; i < dataGrid.SelectedRows.Count; i++)
            {
                var row = new List<object>();
                row.Add(Guid.Parse(dataGrid.SelectedRows[i].Cells[0].Value.ToString()));

                for (var j = 1; j < dataGrid.Columns.Count; j++)
                {
                    if (_tableService.Table.Schema.Fields[j - 1].Type == SupportedTypes.Picture && dataGrid.SelectedRows[i].Cells[j].Value != null)
                    {

                        var imageCell = (DataGridViewImageCell)dataGrid.SelectedRows[i].Cells[j];

                        var picture = new Picture
                        {
                            Description = imageCell.Description,
                            Path = (string)imageCell.Tag
                        };
                        row.Add(picture);

                    }
                    else
                    {
                        string value = dataGrid.SelectedRows[i].Cells[j].Value?.ToString();
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
                }
                values.Add(row);
            }

            _tableService.UpdateRows(values);
            MessageBox.Show(Constants.TableButtonControl.UpdatedSuccess);
        }

        private void DeleteSelectedRows()
        {
            var ids = dataGrid.SelectedRows.Cast<DataGridViewRow>().Select(x => Guid.Parse(x.Cells[0].Value.ToString()));

            foreach (DataGridViewRow row in dataGrid.SelectedRows)
            {
                dataGrid.Rows.Remove(row);
            }

            _tableService.DeleteRows(ids.ToList());
        }
    }
}
