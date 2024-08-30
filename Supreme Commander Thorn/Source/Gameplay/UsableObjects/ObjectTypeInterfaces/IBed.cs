using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public interface IBed
    {
        public int RestPeriod { get; set; }
        public float HealingCapabilities { get; set; }
        public void DisplayBedPrompt();
    }
}
