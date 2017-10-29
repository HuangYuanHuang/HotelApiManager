using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebDirectiveExpand.CustomAttribute;

namespace HostelModel
{
    [Table("T_Hostel_Schedule")]
    public class ScheduleModel : BaseModel
    {
        public ScheduleModel()
        {
            ScheduleOrders = new HashSet<HotelWorkOrderModel>();
        }
        [Required]
        [Display(Name = "排班名称", Order = 1)]
        [MaxLength(50)]
        public string Name { get; set; }

      
        [Display(Name = "开始时间", Order = 2)]
        [SelectControl(Type = ItemDataType.StaticItems, ItemsName = "TimeNodes")]
        public string Start { get; set; }

      
        [Display(Name = "结束时间", Order = 3)]
        [SelectControl(Type = ItemDataType.StaticItems, ItemsName = "TimeNodes")]
        public string End { get; set; }


    
        [Display(Name = "备注", Order = 4)]
        [MaxLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Mark { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<SelectListItem> TimeNodes
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                for (int i = 0; i < 24; i++)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = string.Format("{0:D2}:00", i),
                        Text = string.Format("{0:D2}:00", i)
                    });

                }

                return list;
            }
        }

        public virtual ICollection<HotelWorkOrderModel> ScheduleOrders { set; get; }
    }
}
