using MobilFit_v1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.ViewModels
{
    class RegisterPersonalDataViewModel
    {
        Usuario usuario;
        public RegisterPersonalDataViewModel()
        {
            usuario = new Usuario();
        }

        public void SendNewUsuario(Usuario objUsuario)
        {
            RegisterPhysicalConditionViewModel rp = new RegisterPhysicalConditionViewModel();
            rp.ReceiveNewUser(objUsuario);
        }
    }
}
