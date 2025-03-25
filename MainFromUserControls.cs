using Clipboard.UI.DirectInstall;
using Clipboard.UI.DirectInstall.Settings;
using Clipboard.UI.Error;
using Clipboard.UI.Home;
using Clipboard.UI.Home.Download;
using Clipboard.UI.Home.Help;
using Clipboard.UI.LogIn;
using Clipboard.UI.PleaseWait;
using Clipboard.UI.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard.UI
{
    public static class MainFromUserControls
    {
        public static MainForm MainForm { get; set; }
        public static ucHome UcHome { get; set; }
        public static ucDirectInstall UcDirectInstall { get; set; }
        public static ucLogIn UcLogIn { get; set; }
        public static ucDownload UcDownload { get; set; }
        public static ucHelp UcHelp { get; set; }
        public static ucError UcError { get; set; }
        public static Clipboard.UI.Home.Settings.ucSettings UcSettings { get; set; }
        public static ucPleaseWait UcPleaseWait { get; set; }
    }
}
