using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCA_FYP_API.Dto
{
    public class UpdatePunishment
    { 
        public int id { get; set; }
        public DateTime EndDate { get; set; }
        public string fine { get; set; }
    }
}