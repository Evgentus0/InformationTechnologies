using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm
{
    class CommonControlsHelper
    {
        public static ContextMenuStrip GetContextMenuStrip(List<(string name, 
            Action<object, EventArgs> action)> menuItemsList)
        {
            var contextMenu = new ContextMenuStrip();

            foreach (var menuItem in menuItemsList)
            {
                var toolStripMenuItem = new ToolStripMenuItem(menuItem.name);
                toolStripMenuItem.Click += menuItem.action.Invoke;

                contextMenu.Items.Add(toolStripMenuItem);
            }

            return contextMenu;
        }
    }
}
