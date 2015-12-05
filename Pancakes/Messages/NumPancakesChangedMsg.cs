using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace Pancakes.Messages
{
    public class NumPancakesChangedMsg
    {
        public int NumPancakes { get; set; }
    }

    public class SendRecpieViaEmailMsg
    {
        
    }

    public class ViewStateChangedMsg
    {
        public ApplicationViewState ApplicationViewState { get; set; }
    }
}
