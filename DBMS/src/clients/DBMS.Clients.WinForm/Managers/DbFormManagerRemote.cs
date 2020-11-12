using DBMS.Clients.WinForm.Controls;
using DBMS.Clients.WinForm.DTO;
using DBSM.Manager.Factories;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm.Managers
{
    class DbFormManagerRemote : IDbFormManager
    {
        public void CreateDb(DbSettingsDto dbSettings)
        {
            IDbManager dataBaseService = DbManagerFactory.GetDbManagerRest(dbSettings.Name,
                dbSettings.FileSize, dbSettings.DefaultSource);

            var dbPanel = new DataBasePanelControl(dataBaseService);
            SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
        }

        public void OpenDb(OpenFileDialog fileDialog = null)
        {
            var dbNames = DbManagerFactory.GetRemoteDbsList();

            var form = new SelectDbForm(dbNames);
            form.ShowDialog();

            if (form.IsSet)
            {
                IDbManager dataBaseService = DbManagerFactory.GetDbManagerRest(form.DbName);

                var dbPanel = new DataBasePanelControl(dataBaseService);
                SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
            }
        }
    }
}
