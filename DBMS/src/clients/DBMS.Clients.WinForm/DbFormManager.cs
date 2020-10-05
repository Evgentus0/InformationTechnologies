using DBMS.Clients.WinForm.Controls;
using DBMS.Clients.WinForm.DTO;
using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm
{
    class DbFormManager
    {
        public DbFormManager()
        {        }

        public void OpenDb(string path)
        {
            try
            {
                IDataBaseService dataBaseService = new DataBaseService(path);

                var dbPanel = new DataBasePanelControl(dataBaseService);
                SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void CreateDb(DbSettingsDto dbSettings)
        {
            IDataBaseService dataBaseService = 
                new DataBaseService(dbSettings.Name, 
                dbSettings.RootPath, dbSettings.FileSize, 
                dbSettings.DefaultSource);

            var dbPanel = new DataBasePanelControl(dataBaseService);
            SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
        }
    }
}
