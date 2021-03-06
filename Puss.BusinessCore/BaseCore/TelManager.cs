﻿using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ITelManager : IDbContext<Tel>
    {
    }

    public class TelManager : DbContext<Tel>, ITelManager
    {
    }
}