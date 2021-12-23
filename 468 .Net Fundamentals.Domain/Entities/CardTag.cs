﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("CardTag")]
    public class CardTag
    {
        public int CardId { get; set; }


        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }

        public virtual Card Card { get; set; }

    }
}
