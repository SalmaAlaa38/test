﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilties
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } 
        public object Data { get; set; }
    }
}
