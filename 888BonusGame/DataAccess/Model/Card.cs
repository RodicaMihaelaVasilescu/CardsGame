﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
  public class Card
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Value { get; set; }
  }
}
