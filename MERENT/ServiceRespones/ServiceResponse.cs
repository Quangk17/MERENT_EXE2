﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceRespones
{
    public class ServiceResponse<T>
    {
        public T ?Data { get; set; }
        public bool Success { get; set; } = false;
        public string? Message { get; set; } = null;
        public string? Error { get; set; } = null;
        public List<string>? ErrorMessages { get; set; } = null;
    }
}
