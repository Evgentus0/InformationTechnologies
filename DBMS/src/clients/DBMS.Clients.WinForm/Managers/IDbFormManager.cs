using DBMS.Clients.WinForm.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm.Managers
{
    public interface IDbFormManager
    {
        void OpenDb(OpenFileDialog fileDialog = null);
        void CreateDb(DbSettingsDto dbSettings);
    }
}
