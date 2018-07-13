﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS2Drive.Config
{
    public class Configuration
    {
        public String Path;

        public static Configuration Load(String ConfigurationFilePath)
        {
            if (File.Exists(ConfigurationFilePath))
            {
                try
                {
                    var C = JsonConvert.DeserializeObject<Configuration>(Tools.Unprotect(File.ReadAllText(ConfigurationFilePath)));
                    C.Path = ConfigurationFilePath;
                    return C;
                }
                catch
                {
                    return new Configuration() { IsConfigured = false, HTTPProxyMode = 0, UseProxyAuthentication = false, ProxyLogin = "", ProxyPassword = "", ProxyURL = "", Path = ConfigurationFilePath };
                }
            }
            else
            {
                return new Configuration() { IsConfigured = false, HTTPProxyMode = 0, UseProxyAuthentication = false, ProxyLogin = "", ProxyPassword = "", ProxyURL = "", Path = ConfigurationFilePath };
            }
        }

        public void Save()
        {
            File.WriteAllText(Path, Tools.Protect(JsonConvert.SerializeObject(this)));
        }

        [JsonIgnore]
        public bool IsConfigured { get; set; } = true;

        //Startup
        public bool AutoMount { get; set; }
        public bool AutoStart { get; set; }

        //Drive Parameter
        public String DriveLetter { get; set; }
        public String ServerURL { get; set; }
        public Int32? ServerType { get; set; }
        public String ServerLogin { get; set; }
        public String ServerPassword { get; set; }
        public Int32? KernelCacheMode { get; set; }
        public Int32? FlushMode { get; set; }
        public bool? SyncOps { get; set; }
        public bool? PreLoading { get; set; }

        //Proxy
        public Int16 HTTPProxyMode { get; set; } = 0; //0 = no proxy, 1 = default proxy, 2 = user defined proxy
        public String ProxyURL { get; set; }
        public bool UseProxyAuthentication { get; set; }
        public String ProxyLogin { get; set; }
        public String ProxyPassword { get; set; }
    }
}
