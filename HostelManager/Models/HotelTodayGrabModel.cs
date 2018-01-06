using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class HotelGrabGroupModel
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string DepartMentName { get; set; }

        /// <summary>
        /// 订单列表
        /// </summary>
        public IEnumerable<HotelTodayGrabModel> Orders { get; set; }
    }
    public class HotelTodayGrabModel
    {
        /// <summary>
        /// 部门
        /// </summary>
        [JsonIgnore]
        public string DepartMentName { get; set; }


        public int OrderId { get; set; }

        public string OrderGUID { get; set; }

        /// <summary>
        /// 抢单房间数
        /// </summary>
        public int RoomNum { get; set; }

      
        public string Start { get; set; }

        /// <summary>
        /// 人员详情
        /// </summary>
        public List<PersonOrderDetail> POrders { get; set; }
    }
}
