using CleanArchitecture.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Test.Data.Entity
{
    public class SimpleEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
