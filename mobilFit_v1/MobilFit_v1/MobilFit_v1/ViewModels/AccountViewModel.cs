using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.ViewModels
{
    public class AccountViewModel
    {
        public List<AccountSettings> settings { get; set; }
        public AccountViewModel()
        {
            AccountSettings();
        }

        public void AccountSettings()
        {
            settings = new List<AccountSettings>();
            settings.Add( new AccountSettings() { description = "Notificaciones" });
            settings.Add(new AccountSettings() { description = "Ajustes de cuenta" });
            settings.Add(new AccountSettings() { description = "Acerca de MobilFit" });
            settings.Add(new AccountSettings() { description = "Recomendar MobilFit" });
            settings.Add(new AccountSettings() { description = "Cerrar sesión" });
        }
    }
    public class AccountSettings
    {
        public string description { get; set; }
    }
}
