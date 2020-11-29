using System;

namespace Security_Lab3.Models
{
    public class LcgParams
    {
        public long Multiplier { get; set; }
        
        public long Increment { get; set; }

        public long Module { get; set; } = Convert.ToInt64(Math.Pow(2, 32));
    }
}