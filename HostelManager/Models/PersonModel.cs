﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class UpdatePersonModel
    {
        public string RealName { get; set; }

        public string Sex { get; set; }

        public string IdentityCard { get; set; }


        public string Phone { get; set; }

        public string Address { get; set; }
    }
    public class LoginPersonModel
    {
        public string phone { get; set; }

        public string password { get; set; }
    }

    public class UpdatePersonPwdModel
    {
        public string GUID { get; set; }
        public string oldPassword { get; set; }

        public string newPassword { get; set; }
    }

    public class UploadImageModel
    {
        /// <summary>
        /// ICardPositive ICardBack Health,Icon
        /// </summary>
        public string type { get; set; }

        public string data { get; set; }

        /// <summary>
        /// 人员GUID
        /// </summary>
        public string GUID { get; set; }
    }
}
