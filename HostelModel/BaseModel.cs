using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebDirectiveExpand.CustomAttribute;

namespace HostelModel
{
    
    public class BaseModel
    {
        public BaseModel()
        {
            GUID = Guid.NewGuid().ToString("N");
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        [ModelKey]
     
        public string GUID { get; set; }
        [Required]
        [JsonIgnore]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Display(Name = "创建时间", Order = 99, AutoGenerateField = false)]
        [JsonProperty("CreateTime")]
        [NotMapped]
       
        public string TimeStr { get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
