using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Data.Entity
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime DateCreated { get; set; }
    }
}
