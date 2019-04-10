using System;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Core.Data.Entity
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
