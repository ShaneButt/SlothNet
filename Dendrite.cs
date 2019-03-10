using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothNet
{
    class Dendrite
    {
        public Pulse Input { get; set; }
        public double Weight { get; set; } = 0;
        public bool Learnable { get; set; } = true;
    }
}
