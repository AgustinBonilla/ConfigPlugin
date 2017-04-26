using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigPlugin.Interfaces
{
    public interface IConfiguration
    {
        string CurrentConfiguration { get; }
    }
}
