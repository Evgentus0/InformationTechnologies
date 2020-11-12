using DBMS.Clients.WinForm.Controls;
using DBMS.Clients.WinForm.DTO;
using DBMS.Manager.Factories;
using DBSM.Manager.Factories;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace DBMS.Clients.WinForm.Managers
{
    class DbFormManagerRemote : IDbFormManager
    {
        private IServiceProvider _serviceProvider;
        private IDbManagerFactory _dbManagerFactory;

        public DbFormManagerRemote(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _dbManagerFactory = _serviceProvider.GetService<IDbManagerFactory>();
        }

        public void CreateDb(DbSettingsDto dbSettings)
        {
            IDbManager dataBaseService = _dbManagerFactory.GetDbManagerRest(dbSettings.Name,
                dbSettings.FileSize, dbSettings.DefaultSource);

            var dbPanel = new DataBasePanelControl(dataBaseService, _serviceProvider);
            SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
        }

        public void OpenDb(OpenFileDialog fileDialog = null)
        {
            var dbNames = _dbManagerFactory.GetRemoteDbsList();

            var form = new SelectDbForm(dbNames);
            form.ShowDialog();

            if (form.IsSet)
            {
                IDbManager dataBaseService = _dbManagerFactory.GetDbManagerRest(form.DbName);

                var dbPanel = new DataBasePanelControl(dataBaseService, _serviceProvider);
                SharedControls.FlowLayoutPanelLeftMenu.Controls.Add(dbPanel);
            }
        }
    }
}
