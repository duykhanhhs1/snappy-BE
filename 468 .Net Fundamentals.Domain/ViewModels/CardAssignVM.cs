using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class CardAssignVM
    {
        public int CardId { get; set; }
        public string AssignTo { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
    }
}
