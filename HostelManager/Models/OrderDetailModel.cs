﻿using HostelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class OrderDetailModel
    {
        public int TotalApply { get; set; }

        public IEnumerable<PersonOrderDetail> Persons { get; set; }
    }

    public class PersonOrderDetail
    {
        public int Status { get; set; }

        public string GUID { get; set; }
        public string StatusStr { get { return Status == 1 ? "待处理" : Status == 2 ? "预录用" : Status == 3 ? "录用" : "拒绝"; } set { } }


        public string ApplyTime { get; set; }



        public ServicePersonModel Person { get; set; }
    }
}