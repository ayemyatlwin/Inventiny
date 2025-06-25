using System;
using System.Collections.Generic;

namespace Inventiny.DataBase.Models;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string CategoryCode { get; set; } = null!;

    public int ProductQuantity { get; set; }

    public decimal ProductPrice { get; set; }
}
