using HostelModel.Expand;
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
    [Table("T_Hostel_PersonOrder")]
    public class PersonOrderModel : BaseModel
    {



        [Display(Name = "申请人", Order = 1)]
        [SelectControl(Type = ItemDataType.ControllerAction, Controller = "ServicePerson", Action = "Persons")]
        [JsonConverter(typeof(IntToStringConverter))]
        [TableIgnore]
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        [JsonIgnore]
        [FKeyControl]
        public virtual ServicePersonModel Person { get; set; }
        [Display(Name = "申请人", Order = 1)]
        [NotMapped]
        [FormIgnore]
        public string PersonName { get { return Person?.RealName; } set { } }



        [Display(Name = "订单名称", Order = 2)]
        [SelectControl(Type = ItemDataType.ControllerAction, Controller = "HotelOrder", Action = "Orders")]
        [TableIgnore]
        [JsonConverter(typeof(IntToStringConverter))]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        [JsonIgnore]
        [FKeyControl]
        public virtual HotelWorkOrderModel HotelOrder { get; set; }

        [Display(Name = "订单ID", Order = 2)]
        [NotMapped]
        [FormIgnore]
        public string OrderTitle
        {
            get
            {
                return HotelOrder?.Id + "";


            }
            set { }
        }

        /// <summary>
        /// 状态（1-待处理,2-预录用,3-录用，4-拒绝）
        /// </summary>
        [Display(Name = "审核状态", Order = 4)]
        [SelectControl(Type = ItemDataType.StaticItems, ItemsName = "StatusNodes")]
        [JsonConverter(typeof(IntToStringConverter))]
        [TableIgnore]
        public int Status { get; set; } = 1;

        [Display(Name = "审核状态", Order = 4)]
        [FormIgnore]
        [NotMapped]
        public string StatusStr { get { return Status == 1 ? "待处理" : Status == 2 ? "预录用" : Status == 3 ? "录用" : "拒绝"; } set { } }

        [Display(Name = "备注", Order = 9)]
        [MaxLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Mark { get; set; }


        [Display(Name ="申请数量",Order =10)]
        public int? ApplyNum { get; set; }
        [NotMapped]
        [JsonIgnore]
        public List<SelectListItem> StatusNodes
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem() { Value = "1", Text = "待处理", Selected = true },
                    new SelectListItem() { Value = "2", Text = "预录用"  },
                    new SelectListItem() { Value = "3", Text = "录用"  },
                    new SelectListItem() { Value = "4", Text = "拒绝" },
                };
            }
        }
    }
}
