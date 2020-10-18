using DBMS.Clients.WinForm.Controls;
using DBMS.Clients.WinForm.DTO;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Interfaces;
using DBSM.Manager.Factories;
using DBSM.Manager.Interfaces;
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
                IDbManager dataBaseService = DbManagerFactory.GetDbManagerLocal(path);

                var dbPanel = new DataBasePanelControl(dataBaseService);
                SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CreateDb(DbSettingsDto dbSettings)
        {
            IDbManager dataBaseService = DbManagerFactory.GetDbManagerLocal(dbSettings.Name,
                dbSettings.RootPath, dbSettings.FileSize,
                dbSettings.DefaultSource);

            var dbPanel = new DataBasePanelControl(dataBaseService);
            SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
        }
    }
}
