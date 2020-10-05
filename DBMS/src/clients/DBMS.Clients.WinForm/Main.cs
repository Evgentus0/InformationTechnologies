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
        private DbFormManager _formManager;

        public Main()
        {
            InitializeComponent();
            InitMenu();
            SharedControls.FlowLayoutPanelLeftMenu = flowLayoutPanelMenu;
            SharedControls.DataGrigTable = dataGridViewMain;
            SharedControls.FlowLayoutPanelTopMenu = flowLayoutPanelTopMenu;

            _formManager = new DbFormManager();
        }

        private void InitMenu()
        {
            foreach (var item in MenuList)
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

        private Dictionary<string, List<(string name, Action<object, EventArgs> action)>> MenuList =>
            new Dictionary<string, List<(string name, Action<object, EventArgs> action)>>()
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
                        MessageBox.Show("Eugene DBMS");
                    }))
                },
            };
        
    }
}
