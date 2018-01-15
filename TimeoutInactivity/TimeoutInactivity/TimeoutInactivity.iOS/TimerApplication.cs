using System.Diagnostics;
using System.Timers;
using Foundation;
using UIKit;

namespace TimeoutInactivity.iOS
{
    [Register("TimerApplication")]
    public class TimerApplication : UIApplication
    {
        static int timeInterval = 15 * 1000; // for 30 seconds.

        private static readonly Timer _timer = new Timer(timeInterval);
        public static Timer IdelTimer => _timer;

        public TimerApplication()
        {
            Debug.WriteLine($"TimerApplication --> {IdelTimer.Interval}");

            IdelTimer.Elapsed += IdelTimer_Elapsed;
            ResetIdelTimer();
        }

        void ResetIdelTimer()
        {
            if (IdelTimer != null)
            {
                IdelTimer.Stop();
                IdelTimer.Start();
            }
        }

        private void IdelTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine($"IdelTimer_Elapsed --> {sender}");

            NSNotificationCenter.DefaultCenter.PostNotificationName("AppTimeout", null);
        }

        public override void SendEvent(UIEvent uievent)
        {
            base.SendEvent(uievent);

            if (IdelTimer != null)
                ResetIdelTimer();

            foreach (var touch in uievent.AllTouches)
            {
                var uitouch = (UITouch)touch;
                if (uitouch.Phase == UITouchPhase.Began)
                {
                    Debug.WriteLine($"UITouch Phase --> {uitouch.Phase}");
                    break;
                }
            }
        }
    }
}