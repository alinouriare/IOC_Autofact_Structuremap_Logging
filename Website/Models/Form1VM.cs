using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class Form1VM
    {
       
        public string Name { get; set; }
       
        public string LastName { get; set; }

        public string CaptchaCode { get; set; }

    }
}
