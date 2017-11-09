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
    [Table("T_Hostel_Accout")]
    public class AccoutModel : BaseModel
    {

        [Required]
        [Display(Name = "账号名称", Order = 1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "登录名称", Order = 2)]
        [MaxLength(50)]
        public string LoginName { get; set; }

        [Display(Name = "所属酒店", Order = 2)]
        [SelectControl(Type = ItemDataType.ControllerAction, Controller = "Hotel", Action = "Hotels")]
        [TableIgnore]
        [JsonConverter(typeof(IntToStringConverter))]
        public int HotelId { get; set; }

        [Display(Name = "所属酒店", Order = 2)]
        [NotMapped]
        [FormIgnore]
        public string HotelName { get { return Hotel?.Name; } set { } }


        [Required]
        [Display(Name = "联系人电话", Order = 3)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "账号等级", Order = 4)]
        public int? Level { get; set; } = 1;
        [MaxLength(50)]
        [JsonIgnore] 
        public string Pwd { get; set; } = "8B58887685DBE827"; //默认密码admin

        /// <summary>
        /// 最后登录时间
        /// </summary>
        ///    
        [JsonIgnore]
        public DateTime LastTime { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public int AccoutType { set; get; }

        [Display(Name = "账号状态", Order = 2, AutoGenerateField = false)]
        [NotMapped]

        public string AccoutTypeStr
        {
            get
            {
                if (AccoutType == 0)
                    return "未激活";
                return "正常";
            }
        }

        [Display(Name = "最后登录时间", Order = 2, AutoGenerateField = false)]
        [NotMapped]
        public string LastTimeStr { get { return LastTime.ToString("yyyy-MM-dd HH:mm:ss"); } }


        [Display(Name = "备注", Order = 10)]
        [MaxLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Mark { get; set; }



        [ForeignKey("HotelId")]
        [FKeyControl]
        [JsonIgnore]
        public virtual HotelModel Hotel { get; set; }


    }
}
