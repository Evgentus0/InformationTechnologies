using DBMS.Clients.WinForm.Controls;
using DBMS.Clients.WinForm.Forms;
using DBMS.Clients.WinForm.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm
{
    public partial class Main : Form
    {
        private IDbFormManager _formManager;

        public Main()
        {
            InitializeComponent();
            InitMenu();
            SharedControls.FlowLayoutPanelLeftMenu = flowLayoutPanelMenu;
            SharedControls.GroupBoxData = groupBoxGrid;
            SharedControls.FlowLayoutPanelTopMenu = flowLayoutPanelTopMenu;

            _formManager = new DbFormManagerLocal();
        }

        private void InitMenu()
        {
            foreach (var item in TopMenuList)
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

        private Dictionary<string, List<(string name, Action<object, EventArgs> action)>> TopMenuList =>
            new Dictionary<string, List<(string name, Action<object, EventArgs> action)>>()
            {
                [Constants.MainForm.File] = new List<(string name, Action<object, EventArgs> action)>
                {
                    (Constants.MainForm.Open, new Action<object, EventArgs>((o, f) =>
                    {

                         _formManager.OpenDb(openFileDialog);

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
