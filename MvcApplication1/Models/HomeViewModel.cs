using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowMuchCanISpend.Models
{
    public class HomeViewModel
    {
        //What the future will be instead of hardcoded named properties of each
        public IList<CategoryDisplay> CategoryDisplays { get; set; }
    }
}