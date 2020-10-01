using DBMS.Clients.WinForm.Controls;
using DBMS.Clients.WinForm.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm
{
    public partial class Main : Form
    {
        private Dictionary<string, List<(string name, Action<object, EventArgs> action)>> _menuItemsList;

        private DbFormManager _formManager;

        public Main()
        {
            InitializeComponent();
            InitMenu();
            SharedControls.FlowLayoutPanelMenu = flowLayoutPanelMenu;
            SharedControls.DataGrigTable = dataGridViewMain;

            _formManager = new DbFormManager();
        }

        private void InitMenu()
        {
            _menuItemsList = GetMenuList();

            foreach (var item in _menuItemsList)
            {
                var outItem = new ToolStripMenuItem(item.Key);
                foreach (var inItem in item.Value)
                {
                    var insideItem = new ToolStripMenuItem(inItem.name);
                    insideItem.Click += inItem.action.Invoke;

                    outItem.DropDownItems.Add(insideItem);
                }

                menuStripTop.Items.Add(outItem);
            }
        }

        private Dictionary<string, List<(string name, Action<object, EventArgs> action)>> GetMenuList()
        {
            return new Dictionary<string, List<(string name, Action<object, EventArgs> action)>>()
            {
                [Constants.MainForm.File] = new List<(string name, Action<object, EventArgs> action)>
                {
                    (Constants.MainForm.Open, new Action<object, EventArgs>((o, f) =>
                    {
                        DialogResult result = openFileDialog.ShowDialog();
                        if (result == DialogResult.OK) 
                        {
                           string file = openFileDialog.FileName;

                            _formManager.OpenDb(file);
                        }
                    })),
                    (Constants.MainForm.CreateNew, new Action<object, EventArgs>((o, f) =>
                    {
                        var form = new SetDbNameForm();

                        form.ShowDialog();
                        if (form.IsSet)
                        {
                            _formManager.CreateDb(form.Data);
                        }
                    }))
                },

                [Constants.MainForm.About] = new List<(string name, Action<object, EventArgs> action)>
                {
                    (Constants.MainForm.Instruction, new Action<object, EventArgs>((o, f) =>
                    {
                        MessageBox.Show("test1");
                    }))
                },
            };
        }
    }
}
