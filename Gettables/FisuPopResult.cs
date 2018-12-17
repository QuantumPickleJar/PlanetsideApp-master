using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Gettables
{

    public class FisuPopResult
    {
        public Config config { get; set; }
        public FResult[] result { get; set; }
        public Timing timing { get; set; }
    }
    
    public class Config
    {
        public int[] world { get; set; }
    }

    public class Timing
    {
        public int startms { get; set; }
        public int queryms { get; set; }
        public int processms { get; set; }
        public int totalms { get; set; }
    }
}

    

