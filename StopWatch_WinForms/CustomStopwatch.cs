using System;
using System.Threading;
using System.Threading.Tasks;

namespace StopWatch_WinForms
{
    public class CustomStopwatch
    {
        private Thread timerThread;
        private bool isRunning;
        private DateTime lastStartTime;
        private TimeSpan elapsedTime;
        private readonly object lockObject = new object();

        public event Action<TimeSpan> TimeElapsed;
        public event Action<Exception> OnError;

        public CustomStopwatch()
        {
            elapsedTime = TimeSpan.Zero;
            isRunning = false;
        }

        public void Start()
        {
            lock (lockObject)
            {
                if (isRunning) return;

                isRunning = true;
                lastStartTime = DateTime.Now;
                if (timerThread == null || !timerThread.IsAlive)
                {
                    timerThread = new Thread(RunTimer) { IsBackground = true };
                    timerThread.Start();
                }
            }
        }

        public void Stop()
        {
            lock (lockObject)
            {
                if (!isRunning) return;

                elapsedTime += DateTime.Now - lastStartTime;
                isRunning = false;
                TimeElapsed?.Invoke(elapsedTime);
            }
        }

        public void Reset()
        {
            lock (lockObject)
            {
                isRunning = false;
                elapsedTime = TimeSpan.Zero;
                TimeElapsed?.Invoke(TimeSpan.Zero);
            }
        }

        private void RunTimer()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    lock (lockObject)
                    {
                        if (isRunning)
                        {
                            var currentElapsedTime = elapsedTime + (DateTime.Now - lastStartTime);
                            TimeElapsed?.Invoke(currentElapsedTime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }


    }

}
