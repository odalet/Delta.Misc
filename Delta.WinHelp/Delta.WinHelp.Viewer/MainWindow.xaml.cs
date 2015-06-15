using System.IO;
using System.Reflection;
using System.Windows;

namespace Delta.WinHelp.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var here = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //var test = Path.Combine(here, "data", "ENHMET.HLP");
            //var test = Path.Combine(here, "data", "Css.hlp");               // Has LZ7 Compression and Hall Phrase compression
            //var test = Path.Combine(here, "data", "html40.HLP");            // Has LZ7 Compression and Hall Phrase compression
            //var test = Path.Combine(here, "data", "html40.GID");
            //var test = Path.Combine(here, "data", "OLEMSG2.HLP");
            //var test = Path.Combine(here, "data", "OLEMSG2.GID");

            //var test = Path.Combine(here, "data", "Borland", "MCISTRWH.HLP");
            //var test = Path.Combine(here, "data", "Borland", "PENAPIWH.HLP");
            //var test = Path.Combine(here, "data", "Borland", "SCHED.HLP");
            //var test = Path.Combine(here, "data", "Borland", "TCWHELP.HLP");
            //var test = Path.Combine(here, "data", "Borland", "WIN31MWH.HLP");
            //var test = Path.Combine(here, "data", "Borland", "WORKHELP.HLP");

            //var test = Path.Combine(here, "data", "VB3", "DATAMGR.HLP");
            //var test = Path.Combine(here, "data", "VB3", "VB.HLP");

            //var test = Path.Combine(here, "data", "VB5CCE", "ccreadme.hlp");
            //var test = Path.Combine(here, "data", "VB5CCE", "ctlcrwzd.hlp");
            //var test = Path.Combine(here, "data", "VB5CCE", "proppgwz.hlp");
            //var test = Path.Combine(here, "data", "VB5CCE", "setupwiz.hlp");
            var test = Path.Combine(here, "data", "VB5CCE", "vb5.hlp");             // Has NLevels == 2!
            //var test = Path.Combine(here, "data", "VB5CCE", "vb5def.hlp");
            //var test = Path.Combine(here, "data", "VB5CCE", "vb5pss.hlp");
            //var test = Path.Combine(here, "data", "VB5CCE", "vbcmn96.hlp");
            //var test = Path.Combine(here, "data", "VB5CCE", "vbenlr3.hlp");
            //var test = Path.Combine(here, "data", "VB5CCE", "veENdf3.hlp");

            var doc = WinHelpDocument.Load(test);
        }
    }
}
