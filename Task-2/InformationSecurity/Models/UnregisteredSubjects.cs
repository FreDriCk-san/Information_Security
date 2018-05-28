﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationSecurity.Models
{
    public class UnregisteredSubjects
    {
        public long Id { get; set; }

        [Required]
        public string Login { get; set; }
    }
}
