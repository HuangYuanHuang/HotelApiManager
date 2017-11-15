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
    [Table("T_Hostel_WorkOrder")]
    public class HotelWorkOrderModel : BaseModel
    {


        [NotMapped]
        public string Title { get { return $"{DepartName}-{WorkTypeName}-{ScheduleName}"; } set { } }
        public HotelWorkOrderModel()
        {
            Orders = new HashSet<PersonOrderModel>();
        }

        [Display(Name = "酒店", Order = 0)]
        [SelectControl(Type = ItemDataType.ControllerAction, Controller = "Hotel", Action = "Hotels")]
        [TableIgnore]
        [JsonConverter(typeof(IntToStringConverter))]
        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        [JsonIgnore]
        [FKeyControl]
        public virtual HotelModel Hotel { get; set; }
        [Display(Name = "酒店", Order = 0)]
        [NotMapped]
        [FormIgnore]
        public string HotelName { get { return Hotel?.Name; } set { } }
        [Display(Name = "部门", Order = 1)]
        [SelectControl(Type = ItemDataType.ControllerAction, Controller = "Department", Action = "Departments")]
        [TableIgnore]

        [JsonConverter(typeof(IntToStringConverter))]
        public int DepartID { get; set; }

        [Display(Name = "部门", Order = 1)]
        [NotMapped]
        [FormIgnore]
        public string DepartName { set { } get { return Department?.DepartmentName; } }

        [ForeignKey("DepartID")]
        [JsonIgnore]
        [FKeyControl]
        public virtual DepartmentModel Department { get; set; }


        [Display(Name = "工作排班", Order = 3)]
        [SelectControl(Type = ItemDataType.ControllerAction, Controller = "Schedule", Action = "Schedules")]
        [TableIgnore]
        [JsonConverter(typeof(IntToStringConverter))]
        public int ScheduleId { get; set; }


        [ForeignKey("ScheduleId")]
        [JsonIgnore]
        [FKeyControl]
        public virtual ScheduleModel Schedule { get; set; }

        [Display(Name = "工作排班", Order = 3)]
        [FormIgnore]
        [NotMapped]
        public string ScheduleName { get { return Schedule?.Name; } set { } }

        [Display(Name = "用工类型", Order = 4)]
        [SelectControl(Type = ItemDataType.ControllerAction, Controller = "WorkType", Action = "WorkTypes")]
        [TableIgnore]
        [JsonConverter(typeof(IntToStringConverter))]

        public int WorkTypeId { get; set; }


        [Display(Name = "用工类型", Order = 4)]
        [FormIgnore]
        [NotMapped]
        public string WorkTypeName { get { return WorkType?.Name; } set { } }

        [ForeignKey("WorkTypeId")]
        [JsonIgnore]
        [FKeyControl]
        public virtual WorkTypeModel WorkType { get; set; }

        [Required]
        [Display(Name = "人数", Order = 5)]

        public int Num { get; set; }

        [Display(Name = "开始时间", Order = 5)]
        [Required]
        [JsonIgnore]
        public DateTime Start { get; set; } = DateTime.Now.AddDays(1);


        [Display(Name = "结束时间", Order = 6)]
        [Required]
        [JsonIgnore]
        public DateTime End { get; set; } = DateTime.Now.AddDays(2);

        [JsonProperty("Start")]
        [NotMapped]
        public string StartStr { get { return Start.ToString("yyyy-MM-dd HH:mm:ss"); } }

        [JsonProperty("End")]
        [NotMapped]
        public string EndStr { get { return End.ToString("yyyy-MM-dd HH:mm:ss"); } }

        /// <summary>
        /// 计费方式
        /// </summary>
        [Required]
        [Display(Name = "计费方式", Order = 8)]
        public string Billing { get; set; }

        [Display(Name = "状态", Order = 9)]
        [SelectControl(Type = ItemDataType.StaticItems, ItemsName = "StatusNodes")]
        [TableIgnore]
        [JsonConverter(typeof(IntToStringConverter))]
        public int? Status { get; set; }

        [Display(Name = "状态", Order = 9)]
        [FormIgnore]
        [JsonProperty("StatusStr")]
        [NotMapped]
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
        [Display(Name = "关键字", Order = 10)]
        public string KeyWord { get; set; }


        [Display(Name = "特殊说明", Order = 10)]
        [DataType(DataType.MultilineText)]
        public string Mark { get; set; }

        [JsonIgnore]
        public virtual ICollection<PersonOrderModel> Orders { set; get; }

        [NotMapped]
        [JsonIgnore]
        public List<SelectListItem> StatusNodes
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){ Selected=true,Text="待审核",Value="0"},
                    new SelectListItem(){ Text="审核通过",Value="1"},
                    new SelectListItem(){ Text="审核未通过",Value="2"},
                    new SelectListItem(){ Text="上线",Value="3"},
                    new SelectListItem(){ Text="下线",Value="4"}

                };
            }
        }


        [Display(Name = "审核意见", Order = 11)]
        [DataType(DataType.MultilineText)]
        public string Examine { get; set; }
    }
}
