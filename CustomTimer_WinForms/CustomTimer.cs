using System;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace CustomTimer_WinForms
{
    public class CustomTimer
    {
        private System.Threading.Timer timer;
        private bool isRunning;
        private int interval;
        private Action timeElapsedHandler;
        private Action<Exception> errorHandler;


        public event Action TimeElapsed
        {
            add { timeElapsedHandler += value; }
            remove { timeElapsedHandler -= value; }
        }

        public event Action<Exception> OnError
        {
            add { errorHandler += value; }
            remove { errorHandler -= value; }
        }

        public CustomTimer(int interval)
        {
            this.interval = interval;
            isRunning = false;
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                timer = new System.Threading.Timer(TimerCallback, null, 0, interval);
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                timer.Dispose();
                isRunning = false;
            }
        }

        private void TimerCallback(object state)
        {
            try
            {
                // Simulating some task execution in the elapsed time event
                ThreadPool.QueueUserWorkItem(callbackState =>
                {
                    timeElapsedHandler?.Invoke();
                });
            }
            catch (Exception ex)
            {
                if (errorHandler != null)
                {
                    errorHandler(ex); // Invoke the errorHandler delegate
                }
                
            }
        }
    }



}
