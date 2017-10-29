using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HostelModel
{
    [Table("T_Hostel_Department")]
    public class DepartmentModel: BaseModel
    {
        [Required]
        [Display(Name = "部门名称", Order = 1)]
        [MaxLength(50)]
        public string DepartmentName { get; set; }

        [Required]
        [Display(Name = "职责", Order = 2)]
        [MaxLength(250)]
        public string Duty { get; set; }

        [Display(Name = "备注", Order = 3)]
        [MaxLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Mark { get; set; }

        [JsonIgnore]
        public virtual ICollection<HotelWorkOrderModel> WorkTypeOrders { get; set; } = new HashSet<HotelWorkOrderModel>();
    }
}
