using HostelModel.Expand;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebDirectiveExpand.CustomAttribute;

namespace HostelModel
{
    [Table("T_Hostel_Hotel")]
    public class HotelModel : BaseModel
    {
        public HotelModel()
        {
            Accouts = new HashSet<AccoutModel>();
            HotelOrders = new HashSet<HotelWorkOrderModel>();
        }

        [Required]
        [Display(Name = "酒店名称", Order = 1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "所属区域", Order = 1)]
        [SelectControl(Type = ItemDataType.ControllerAction, Controller = "Area", Action = "Areas")]
        [TableIgnore]
        [JsonConverter(typeof(IntToStringConverter))]
        public int AreaId { get; set; }


        [ForeignKey("AreaId")]
        [JsonIgnore]
        [FKeyControl(ValueName = "Name", PropName = "AreaName")]
        public virtual AreaModel Area { get; set; }

        [Display(Name = "所属区域", Order = 1)]
        [FormIgnore]
        [NotMapped]
        public string AreaName { get { return Area?.Name; } set { } }

        [Display(Name = "酒店星级", Order = 1)]
        [SelectControl(Type = ItemDataType.StaticItems, ItemsName = "HotelNodes")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "通信地址", Order = 2)]
        public string MailingAddress { get; set; }

        [Required]
        [Display(Name = "企业电话", Order = 3)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "信用代码", Order = 4)]
        public string CODE { get; set; }

        [Required]
        [Display(Name = "酒店地址", Order = 5)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "开户银行", Order = 6)]
        public string Bank { get; set; }

        [Required]
        [Display(Name = "开户银行所在地", Order = 7)]
        public string BankAddress { get; set; }
        [Required]
        [Display(Name = "银行账号", Order = 8)]
        public string BankAccount { get; set; }

        [Display(Name = "经度", Order = 9)]
        public string Longitude { get; set; }

        [Display(Name = "纬度", Order = 9)]
        public string Latitude { get; set; }

        [Display(Name = "排序", Order = 9)]
        public int? Sort { get; set; } = 0;

        [Display(Name = "备注", Order = 9)]
        [MaxLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Mark { get; set; }


        [NotMapped]
        [JsonIgnore]
        public List<SelectListItem> HotelNodes
        {
            get
            {
                return new List<SelectListItem>()
                {
                     new SelectListItem(){ Value="二星级",Text="二星级"},
                     new SelectListItem(){ Value="三星级",Text="三星级"},
                     new SelectListItem(){ Value="四星级",Text="四星级"},
                     new SelectListItem(){ Value="五星级",Text="五星级"},
                };
            }
        }

        [JsonIgnore]
        public virtual ICollection<AccoutModel> Accouts { set; get; }
        [JsonIgnore]
        public virtual ICollection<HotelWorkOrderModel> HotelOrders { set; get; }
    }
}
