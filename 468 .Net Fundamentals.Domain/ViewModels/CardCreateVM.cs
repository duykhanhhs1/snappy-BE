using _468_.Net_Fundamentals.Domain.EnumType;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class CardCreateVM
    {
      /*  public int Id { get; set; }*/
        public int BusId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Duedate { get; set; }

        public TaskPriority? Priority { get; set; }

        /*  public string? Description { get; set; }
          public DateTime? Duedate { get; set; }
          public TaskPriority? Priority { get; set; }
          public IList<int>? CardAssigns { get; set; }
          public IList<TodoVM>? Todos { get; set; }*/

    }
}
