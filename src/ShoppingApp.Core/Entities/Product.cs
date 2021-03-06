using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.Common;

namespace ShoppingApp.Core.Entities;

public class Product : IEntity
{
    public int Id { get; set; }
    public byte[] Image { get; set; }
    public string Name { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
