using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCollector.model
{
    public class ResultsLimit: SimpleValue<int>
    {
        public ResultsLimit(int v)
        {
            Value = v;
        }
    }
}
