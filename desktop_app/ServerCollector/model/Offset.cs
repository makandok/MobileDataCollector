using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCollector.model
{
    public class Offset : SimpleValue<int>
    {
        public Offset(int v)
        {
            Value = v;
        }
    }
}
