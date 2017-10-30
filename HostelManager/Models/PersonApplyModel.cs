using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    /// <summary>
    /// 申请用工对象
    /// </summary>
    public class PersonApplyModel
    {
        public int PersonId { get; set; }

        public int OrderId { get; set; }

        public string Mark { get; set; } = "无";
    }
}
