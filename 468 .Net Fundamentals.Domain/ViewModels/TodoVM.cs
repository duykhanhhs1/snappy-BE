using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class TodoVM
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int CardId { get; set; }
        public string CardName { get; set; }
        public Boolean IsCompleted { get; set; }
    }
}
