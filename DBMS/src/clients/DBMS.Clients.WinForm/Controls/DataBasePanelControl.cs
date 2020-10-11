using DBMS.Clients.WinForm.Forms;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm.Controls
{
    class DataBasePanelControl: Panel
    {
        private bool _isCollapsed;
        private Settings _settings;

        private List<(string name, Action<object, EventArgs> action)> _menuItemsList;

        private IDataBaseService _dataBaseService;
        public DataBasePanelControl(IDataBaseService dataBaseService)
        {
            _settings = new Settings();
            _dataBaseService = dataBaseService;

            _isCollapsed = true;

            _menuItemsList = GetMenuItemList();

            InitPanel();

            AddMainButton();

            InitSubButtons();

            InitContextMenu();
        }

        private void InitSubButtons()
        {
            foreach(var table in _dataBaseService.GetTables())
            {
                AddSubButton(table);
            }
        }

        private List<(string name, Action<object, EventArgs> action)> GetMenuItemList()
        {
            return new List<(string name, Action<object, EventArgs> action)>()
            {
                (Constants.DbPanelControl.AddNewTable, new Action<object, EventArgs>((o, f) =>
                    {
                        AddNewTable();
                    }))
            };
        }

        private void AddNewTable()
        {
            var form = new InputForm(Constants.DbPanelControl.NewTableName);

            form.ShowDialog();

            if (form.IsSet)
            {
                _dataBaseService.AddTable(form.Value);
                var tableService = _dataBaseService[form.Value];

                AddSubButton(tableService);
            }
        }

        private void InitPanel()
        {
            Size = new Size(_settings.LeftSideButtonWidth, _settings.DataBaseButtonHeight);
            MinimumSize = new Size(_settings.LeftSideButtonWidth, _settings.DataBaseButtonHeight);
            Dock = DockStyle.Top;
            Name = _dataBaseService.Name + "Panel";
        }

        private void AddMainButton()
        {
            var button = new Button();
            button.Size = new Size(_settings.LeftSideButtonWidth, _settings.DataBaseButtonHeight);
            button.Text = _dataBaseService.Name;
            button.Dock = DockStyle.Top;
            button.Click += MainButton_Click;

            Controls.Add(button);
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            if (_isCollapsed)
            {
                Height = MaximumSize.Height;

                _isCollapsed = false;

            }
            else
            {
                Height = MinimumSize.Height;

                _isCollapsed = true;
            }
        }

        private int MaxHeight => _settings.DataBaseButtonHeight + (Controls.Count - 1) * _settings.SubButtonHeght;

        private void InitContextMenu()
        {
            var contextMenu = CommonControlsHelper.GetContextMenuStrip(_menuItemsList);

            ContextMenuStrip = contextMenu;
        }

        public void AddSubButton(ITableService tableService)
        {
            var button = new TableButtonControl(tableService, _dataBaseService);

            Controls.Add(button);
            MaximumSize = new Size(_settings.LeftSideButtonWidth, MaxHeight);
        }
    }
}
