﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class LoginHotelModel
    {
        public string username { get; set; }

        public string password { get; set; }
    }

    public class UpdateHotelPwdModel
    {
        public string oldPassword;
        public string newPassword;
             
    }
}
