using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class HotelOrderDetaiModel
    {
        public int Min { get; set; }

        public int Max { get; set; }
        public string DepartMentName { get; set; }

        public string HotelName { get; set; }

        public string ScheduleName { get; set; }

        public int DepartID { get; set; }
        public int ScheduleId { get; set; }
        public int WorkTypeId { get; set; }

        public string WorkTypeName { get; set; }

        public int Num { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string Billing { get; set; }

        public string Mark { get; set; }

        public int Id { get; set; }

        public string GUID { get; set; }

        public string CreateTime { get; set; }

        /// <summary>
        /// 已申请数量（人数|房间数量）
        /// </summary>
        public int AppliedNum { get; set; }

        /// <summary>
        /// 新申请人数
        /// </summary>
        public int NewApply { get; set; }

        [JsonIgnore]
        public int? Status { set; get; }

        [JsonProperty("Status")]
        public string StatusStr
        {
            get
            {
                if (Status == 1)
                    return "审核通过";
                else if (Status == 2)
                    return "审核未通过";
                else if (Status == 3)
                    return "上线";
                else if (Status == 4)
                    return "下线";
                return "待审核";

            }
            set { }
        }
        /// <summary>
        /// 已经录用人数
        /// </summary>
        public int EmployNum { get; set; }
        public string Examine { get; set; }
    }


    public class HotelAreaOrderModel : HotelOrderDetaiModel
    {
        public int HotelId { get; set; }

        public string KeyWord { get; set; }
        public int? Sort { get; set; } = 0;
        public string HotelGUID { get; set; }
        public string AreaName { get; set; }

        public int AreaId { get; set; }

        public float HotelEvaluate { get; set; }

    }
}
