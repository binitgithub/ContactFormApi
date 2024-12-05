using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactFormApi.Models
{
    public class ContactFormModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}