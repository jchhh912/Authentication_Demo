using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication_Demo
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class AzureAdOption
    {
        public string ClientId { get; set; }

        public string Instance { get; set; }

        public string Domain { get; set; }

        public string TenantId { get; set; }

        public string CallbackPath { get; set; }
    }
}
