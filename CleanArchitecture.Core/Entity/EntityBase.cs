using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitecture.Core.Entity
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
