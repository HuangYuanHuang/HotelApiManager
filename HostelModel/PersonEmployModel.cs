using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HostelModel
{
    [Table("T_PersonEmploy")]
    public class PersonEmployModel: BaseModel
    {
        public int PersonId { get; set; }     

        [ForeignKey("PersonId")]       
        public virtual ServicePersonModel Person { get; set; }

        public int HotelOrderId { get; set; }

        [ForeignKey("HotelOrderId")]
        public virtual HotelWorkOrderModel HoterlOrder { get; set; }

        /// <summary>
        /// 状态 录用,终止
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 酒店对用工的评价 1-5
        /// </summary>
        public int? Evaluate{ get; set; }

        /// <summary>
        /// 用工对酒店的评价等级 1-5
        /// </summary>
        public int? HotelEvaluate { get; set; }

        /// <summary>
        /// 酒店对用工的评论
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 用工对酒店的评论
        /// </summary>
        public string HotelComment { get; set; }
    }
}
