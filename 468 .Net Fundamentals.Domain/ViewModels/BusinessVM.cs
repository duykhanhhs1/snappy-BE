using _468_.Net_Fundamentals.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class BusinessVM
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string ColorCode { get; set; }
        public int  ProjectId { get; set; }

    }
}
