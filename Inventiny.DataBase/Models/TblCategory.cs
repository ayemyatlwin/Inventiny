using System;
using System.Collections.Generic;

namespace Inventiny.DataBase.Models;

public partial class TblCategory
{
    public int CategoryId { get; set; }

    public string CategoryCode { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}
