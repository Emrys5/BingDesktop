using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emrys.BingDesktop
{

    public class BingImageJson
    {
        public Image[] Images { get; set; }
    }


    public class Image
    {
        public string Url { get; set; }

        public string Copyright { get; set; }
    }
}
