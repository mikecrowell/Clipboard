using FieldTool.BLL;
using FieldTool.Constants.Config;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using FieldTool.UI;
using System;
using System.Configuration;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FieldTool
{
    internal static class Program
    {
        //private static readonly System.Windows.Forms.Timer batteryCheckTimer = new System.Windows.Forms.Timer();

        public static Mutex IsOnlyApplicationRunning { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                bool result;
                IsOnlyApplicationRunning = new System.Threading.Mutex(true, "ClipBoardMutexAppId4125", out result);

                if (!result)
                {
                    MessageBox.Show("Another instance is already running.");
                    return;
                }

                Lg.StartingHeader("ClipBoard");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                PowerStatus ps = SystemInformation.PowerStatus;
                if (ps.BatteryLifePercent <= 0.07 && ps.PowerLineStatus == PowerLineStatus.Offline)
                {
                    MessageBox.Show("Battery must be above 7%. Clipboard will now close");
                    Application.Exit();
                }

                GC.KeepAlive(IsOnlyApplicationRunning);
                Application.Run(new frmMain());
                IsOnlyApplicationRunning.Dispose();
            }
            catch (Exception e)
            {
                var cause = ExceptionHelper.Innermost(e);
                Lg.FatalError(e, "Main");
                Lg.FatalError(cause, "Cause");
                Lg.FailedFinish("ClipBoard");

                MessageBox.Show(cause.Message, "Fatal Error: Saving your datafile.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DataStore.SaveData(true);
            }
        }





        public static void CheckBattery(Object sender, EventArgs myEventArgs)
        {
            PowerStatus ps = SystemInformation.PowerStatus;

            try
            {
                if (ConfigurationManager.AppSettings[AppConstants.AppKeys.ENV] != AppConstants.Enviorment.RELEASE)
                {
                    Lg.Info($"BatteryListPercent: {ps.BatteryLifePercent}");
                }

                if (ps.BatteryLifePercent <= 0.07 && ps.PowerLineStatus == PowerLineStatus.Offline)
                {
                    Lg.Info("CheckBattery: Shutting Down...");
                    DataStore.SaveData(true);
                    Thread.Sleep(1000);
                    Application.Exit();
                }
            }
            catch (Exception e)
            {
                Lg.FatalError(e, "CheckBattery");
            }
        }


    }
}