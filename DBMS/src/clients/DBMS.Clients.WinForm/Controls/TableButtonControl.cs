using DBMS.Clients.WinForm.Forms;
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
        }

        private List<(string name, Action<object, EventArgs> action)> MenuItemList =>
             new List<(string name, Action<object, EventArgs> action)>()
                {
                    (Constants.TableButtonControl.Select, new Action<object, EventArgs>((o, f) =>
                        {
                            FillDataGrid();
                        })),

                    (Constants.TableButtonControl.EditSchema, new Action<object, EventArgs>((o, f) =>
                        {
                            EditTableShema();
                        }))
                };

        private void EditTableShema()
        {
            new TableSchemaForm(_tableService).ShowDialog();

            _tableService.UpdateSchema();
        }

        private void FillDataGrid()
        {
            FillColumnHeaders();

            var dataGrid = SharedControls.DataGrigTable;
            var fieldCount = dataGrid.ColumnCount = _tableService.Fields.Count();
            var rows = _tableService.Select(100, 0).Count();
            if (rows < 1)
            {
                MessageBox.Show(Constants.TableButtonControl.EmptyTable);
            }
            else
            {
                dataGrid.RowCount = rows;
                int rowCount = 0;

                foreach (var row in _tableService.Select(100, 0))
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        dataGrid.Rows[rowCount].Cells[i].Value = row[i];
                    }
                    rowCount++;
                }
            }
        }

        private void FillColumnHeaders()
        {
            var dataGrid = SharedControls.DataGrigTable;
            var fieldCount = dataGrid.ColumnCount = _tableService.Fields.Count();

            for (int i = 0; i < fieldCount; i++)
            {
                dataGrid.Columns[i].Name = _tableService.Fields[i].Name;
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
    }
}
