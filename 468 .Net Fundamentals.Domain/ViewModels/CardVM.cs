﻿using _468_.Net_Fundamentals.Domain.EnumType;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class CardVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Duedate { get; set; }
        public string Description { get; set; }
        public TaskPriority? Priority { get; set; }
        public int BusinessId { get; set; }


    }
}
