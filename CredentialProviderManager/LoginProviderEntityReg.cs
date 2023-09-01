using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CredentialProviderManager
{
    internal class LoginProviderEntityReg
    {
        private RegistryKey _key;
        private Boolean _iswritable;
        public LoginProviderEntityReg(RegistryKey key, Boolean iswritable)
        {
            if(key == null)
            {
                throw new ArgumentException("Invalid Registry Key");
            }
            _iswritable = iswritable;
            _key = key;
        }

        public string type
        {
            get
            {
                return _key.View.ToString();
            }
        }

        public string name
        {
            get
            {
                Object oName = _key.GetValue("");
                if(oName == null)
                {
                    return _key.Name;
                } else
                {
                    return oName.ToString();
                }
            }
        }

        public bool enabled
        {
            get {
                Object oDisabled = _key.GetValue("Disabled");
                if(oDisabled == null)
                {
                    return true;
                } else
                {
                    Int32 disabled = (Int32)oDisabled;
                    return disabled == 0;
                }
            }
            set {
                if(_iswritable)
                {
                    Int32 disabled = value ? 0 : 1;
                    _key.SetValue("Disabled", disabled);
                }
            }
        }
    }
}
