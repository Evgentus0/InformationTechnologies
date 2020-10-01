using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBMS_Core.Extentions;
using System.Linq;

namespace DBMS.Clients.WinForm.Controls
{
    class TableButtonControl : Button
    {
        private Settings _settings;
        private List<(string name, Action<object, EventArgs> action)> _menuItemsList;

        private ITableService _tableService;

        public TableButtonControl(ITableService tableService)
        {
            _settings = new Settings();

            _tableService = tableService;

            _menuItemsList = GetMenuItemList();
                
            Text = tableService.Name;

            InitButton();
        }

        private List<(string name, Action<object, EventArgs> action)> GetMenuItemList()
        {
            return new List<(string name, Action<object, EventArgs> action)>()
            {
                (Constants.TableButtonControl.Select, new Action<object, EventArgs>((o, f) =>
                    {
                        FillDataGrid();
                    })),

                (Constants.TableButtonControl.EditSchema, new Action<object, EventArgs>((o, f) =>
                    {
                        MessageBox.Show("test2");
                    }))
            };
        }

        private void FillDataGrid()
        {
            var dataGrid = SharedControls.DataGrigTable;
            var fieldCount = dataGrid.ColumnCount = _tableService.Fields.Count();

            for(int i=0;i< fieldCount; i++)
            {
                dataGrid.Columns[i].Name = _tableService.Fields[i].Name;
            }

            dataGrid.RowCount = 100;
            int rowCount = 0;

            foreach(var row in _tableService.Select(100, 0))
            {
                for (int i = 0; i < fieldCount; i++)
                {
                    dataGrid.Rows[rowCount].Cells[i].Value = row[i];
                }
                rowCount++;
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
            var contextMenu = new ContextMenuStrip();

            foreach (var menuItem in _menuItemsList)
            {
                var toolStripMenuItem = new ToolStripMenuItem(menuItem.name);
                toolStripMenuItem.Click += menuItem.action.Invoke;

                contextMenu.Items.Add(toolStripMenuItem);
            }

            ContextMenuStrip = contextMenu;
        }
    }
}
