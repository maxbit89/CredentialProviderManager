using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CredentialProviderManager
{
    internal class LoginProvidersModel
    {
        private String _registryLoginProviderBaseKey;

        private LoginProviderEntityReg open(RegistryKey baseKey, String regPath)
        {
            Boolean writeable = true;
            RegistryKey key;
            try
            {
                key = baseKey.OpenSubKey(regPath, writeable);
            } catch (SecurityException)
            {
                writeable = false;
                key = baseKey.OpenSubKey(regPath, writeable);
            }
            if(key == null)
            {
                throw new ArgumentException("Invalid Key: " + regPath);
            }
            return new LoginProviderEntityReg(key, writeable);
        }

        public List<LoginProviderEntityReg> getEntries() {
            var baseKey32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            var baseKey64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            List<LoginProviderEntityReg> entries = new List<LoginProviderEntityReg>();
            if (baseKey32 == null && baseKey64 == null)
            {
                throw new ArgumentException("Invalid baseKey: " + _registryLoginProviderBaseKey);
            }
            if (baseKey64 != null)
            {
                RegistryKey baseKey = baseKey64.OpenSubKey(_registryLoginProviderBaseKey);
                entries.AddRange(baseKey.GetSubKeyNames().Select(x => open(baseKey64, _registryLoginProviderBaseKey + "\\" + x)));
            }
            /*
            if (baseKey32 != null)
            {
                RegistryKey baseKey = baseKey32.OpenSubKey(_registryLoginProviderBaseKey);
                entries.AddRange(baseKey.GetSubKeyNames().Select(x => open(baseKey32, _registryLoginProviderBaseKey + "\\" + x)));
            }
            */
            return entries;
        }
        public LoginProvidersModel(String registryLoginProviderBaseKey)
        {
            _registryLoginProviderBaseKey = registryLoginProviderBaseKey;
        }

    }
}
