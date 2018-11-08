using System;
using System.Collections.Generic;

namespace AuthorizationWeb.Controllers
{
    public class ModuleActivator
    {
        //protected Dictionary<Controllers>
        private readonly Dictionary<string, Func<IModule>> _loaders = new Dictionary<string, Func<IModule>>();
        public void Register(string key, Func<IModule> loader)
        {
            _loaders[key] = loader;
        }
        
        public IModule Get(string key)
        {
            return _loaders[key]();
        }

        public List<string> GetModulesRegistered()
        {
            List<string> modules = new List<string>();
            foreach (var key in _loaders.Keys)
            {
                modules.Add(key);
            }

            return modules;
        }
    }
}