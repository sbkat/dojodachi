using System;

namespace dojodachi.Models
{
    public class DojodachiModel
    {
        public int happiness { get; set; }
        public int fullness { get; set; }
        public int energy { get; set; }
        public int meals { get; set; }
        public DojodachiModel()
        {
            happiness = 20;
            fullness = 20;
            energy = 50;
            meals = 3;
        }
    }

}