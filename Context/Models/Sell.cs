using System;
using System.Collections.Generic;

namespace Context.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public int? ClientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ProductId { get; set; }
}
