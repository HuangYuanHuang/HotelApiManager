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
    [Table("T_Hostel_Area")]
    public partial class AreaModel : BaseModel
    {
        public AreaModel()
        {
            Hotels = new HashSet<HotelModel>();
        }

        [Required]
        [Display(Name = "区域名称", Order = 1)]
        [MaxLength(255)]
        public string Name { get; set; }


        [Display(Name = "城市", Order = 2)]
        [SelectControl(Type = ItemDataType.StaticItems, ItemsName = "CityNodes")]
        [MaxLength(255)]
        public string City { get; set; }

        [Required]
        [Display(Name = "详细地址", Order = 3)]
        [MaxLength(1024)]
        public string Address { get; set; }

        [Display(Name = "备注", Order = 4)]
        [DataType(DataType.MultilineText)]
        public string Mark { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<SelectListItem> CityNodes
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){ Text="上海",Value="上海",Selected=true},
                    new SelectListItem(){ Text="北京",Value="北京"},
                    new SelectListItem(){ Text="苏州",Value="苏州"},
                };
            }
        }
        public virtual ICollection<HotelModel> Hotels { set; get; }
    }
}
