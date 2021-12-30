﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSoft.Asset.Model
{
    public class AsDistSet
    {
       [Key]
        public int AsDistId { get; set; }
        public string DistCode { get; set; }
        public string Description { get; set; }
        public string Dist1 { get; set; }
    }
}