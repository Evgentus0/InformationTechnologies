using DBMS.Clients.WinForm.Controls;
using DBMS.Clients.WinForm.DTO;
using DBMS.Clients.WinForm.Forms;
using DBMS.Manager.Factories;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Interfaces;
using DBSM.Manager.Factories;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace DBMS.Clients.WinForm.Managers
{
    class DbFormManagerLocal: IDbFormManager
    {
        private IServiceProvider _serviceProvider;
        private IDbManagerFactory _dbManagerFactory;

        public DbFormManagerLocal(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _dbManagerFactory = _serviceProvider.GetService<IDbManagerFactory>();
        }

        public void OpenDb(OpenFileDialog fileDialog)
        {
            try
            {
                string path = "";
                var form = new InputForm(Constants.DbPanelControl.EnterPathForDb);
                form.ShowDialog();
                if (form.IsSet)
                {
                    path = form.Value;
                }

                IDbManager dataBaseService = _dbManagerFactory.GetDbManagerLocal(path);

                var dbPanel = new DataBasePanelControl(dataBaseService, _serviceProvider);
                SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CreateDb(DbSettingsDto dbSettings)
        {
            IDbManager dataBaseService = _dbManagerFactory.GetDbManagerLocal(dbSettings.Name,
                dbSettings.RootPath, dbSettings.FileSize,
                dbSettings.DefaultSource);

            var dbPanel = new DataBasePanelControl(dataBaseService, _serviceProvider);
            SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
        }
    }
}
