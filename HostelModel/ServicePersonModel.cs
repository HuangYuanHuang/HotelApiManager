using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebDirectiveExpand.CustomAttribute;

namespace HostelModel
{
    [Table("T_Hostel_ServicePerson")]
    public class ServicePersonModel : BaseModel
    {

        public ServicePersonModel()
        {
            Orders = new HashSet<PersonOrderModel>();
            Employs = new HashSet<PersonEmployModel>();
        }


      
        [Display(Name = "真实姓名", Order = 0)]
        [MaxLength(50)]
        public string RealName { set; get; }



        [Display(Name = "性别", Order = 1)]
        [MaxLength(25)]
        [RadioList(Values = "男,女", Texts = "男,女")]
        public string Sex { get; set; } = "女";


        [Display(Name = "密码", Order = 2)]
        [DataType(DataType.Password)]
        [MaxLength(250)]
        [TableIgnore]
        public string Pwd { get; set; }



        [Display(Name = "身份证", Order = 4)]
        [MaxLength(50)]
        public string IdentityCard { set; get; }

        [Required]
        [Display(Name = "手机号", Order = 5)]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(50)]
        public string Phone { get; set; }



        [Display(Name = "通讯地址", Order = 6)]

        [MaxLength(250)]
        public string Address { get; set; }


        [Display(Name = "用户头像", Order = 7)]
        [TableIgnore]
        public string Icon { get; set; }

        [TableIgnore]
        public string ICardPositive { get; set; }

        [TableIgnore]
        public string ICardBack { get; set; }

        [TableIgnore]
        public string Health { get; set; }
        [JsonIgnore]
        public int Status { get; set; } = 1;

        [Display(Name = "账号状态", Order = 11)]
        [JsonProperty("Status")]
        [FormIgnore]
        [NotMapped]
        public string StatusStr { get { return Status == 1 ? "正常" : "待审核"; } set { } }

        [JsonIgnore]
        public virtual ICollection<PersonOrderModel> Orders { set; get; }


        public virtual ICollection<PersonEmployModel> Employs { get; set; }



    }
}
