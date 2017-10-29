using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class PersonComment
    {
        /// <summary>
        /// 用工对酒店的评价等级 1-5
        /// </summary>
        public int? HotelEvaluate { get; set; } = 1;

        /// <summary>
        /// 用工对酒店的评论
        /// </summary>
        public string HotelComment { get; set; }
    }

    public class HotelComment
    {
        /// <summary>
        /// 酒店对用工的评价 1-5
        /// </summary>
        public int? Evaluate { get; set; } = 1;

       

        /// <summary>
        /// 酒店对用工的评论
        /// </summary>
        public string Comment { get; set; }
    }
}
