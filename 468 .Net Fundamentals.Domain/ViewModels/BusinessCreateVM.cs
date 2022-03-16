using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class BusinessCreateVM
    {
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public Boolean IsDefault { get; set; }
        public Boolean IsDefaultFinish { get; set; }
    }
}
