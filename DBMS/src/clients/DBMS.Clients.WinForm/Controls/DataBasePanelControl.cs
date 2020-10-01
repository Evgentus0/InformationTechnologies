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

        private IDataBaseService _dataBaseService;
        public DataBasePanelControl(IDataBaseService dataBaseService)
        {
            _settings = new Settings();
            _dataBaseService = dataBaseService;

            _isCollapsed = true;

            InitPanel(_dataBaseService.Name);

            AddMainButton(_dataBaseService.Name);

            InitSubButtons();
        }

        private void InitSubButtons()
        {
            foreach(var table in _dataBaseService.GetTables())
            {
                AddSubButton(table);
            }
        }

        private void InitPanel(string name)
        {
            Size = new Size(_settings.LeftSideButtonWidth, _settings.DataBaseButtonHeight);
            MinimumSize = new Size(_settings.LeftSideButtonWidth, _settings.DataBaseButtonHeight);
            Dock = DockStyle.Top;
            Name = name + "Panel";
        }

        private void AddMainButton(string name)
        {
            var button = new Button();
            button.Size = new Size(_settings.LeftSideButtonWidth, _settings.DataBaseButtonHeight);
            button.Text = name;
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

        public void AddSubButton(ITableService tableService)
        {
            var button = new TableButtonControl(tableService);

            Controls.Add(button);
            MaximumSize = new Size(_settings.LeftSideButtonWidth, MaxHeight);
        }
    }
}
