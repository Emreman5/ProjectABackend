﻿using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class AuthResponseDto
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string? UserId { get; set; }
        public Token? Token { get; set; }
    }
}
