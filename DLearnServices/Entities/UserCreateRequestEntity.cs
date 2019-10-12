﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLearnServices.Entities
{
    public class UserCreateRequestEntity : UserEntity
    {
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
