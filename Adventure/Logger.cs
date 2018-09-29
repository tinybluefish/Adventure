using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    static class Logger
    {
        internal static ListBox LOG = new ListBox();

        public static void Log(string msg)
        {
            LOG.Items.Add(msg);
            LOG.SetSelected(LOG.Items.Count - 1, true);         
        }
    }
}
