using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emrys.Desktop
{

    public class Rootobject
    {
        public Image[] Images { get; set; }
    }


    public class Image
    {
        public string Url { get; set; }

        public string Copyright { get; set; }
    }
}
