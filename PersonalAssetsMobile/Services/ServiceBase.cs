using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.Services
{
    public class ServiceBase
    {
        public string Token { get; set; }

        public ServiceBase() {
            Token = Task.Run(async () => await SecureStorage.Default.GetAsync("TOKEN")).Result;
        }
    }
}
