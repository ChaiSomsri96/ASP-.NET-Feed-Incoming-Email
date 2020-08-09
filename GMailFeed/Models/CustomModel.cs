using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMailFeed.Models
{
    public class CustomerModel
    {
        public string MessageContent { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
    }
}
