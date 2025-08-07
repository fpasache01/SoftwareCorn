using System;
using System.Collections.Generic;

namespace Context.Models;

public partial class Sell
{
    public int SellId { get; set; }

    public int? ClientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ProductId { get; set; }
}
