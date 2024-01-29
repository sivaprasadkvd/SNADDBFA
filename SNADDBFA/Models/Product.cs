using System;
using System.Collections.Generic;

namespace SNADDBFA.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public double Price { get; set; }

    public int Qty { get; set; }
}
