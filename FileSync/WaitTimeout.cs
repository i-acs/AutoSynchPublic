using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tools
{
    public class WaitTimeout
    {
        private DateTime timeout;
        private bool release;
        private bool timedOut;
        private Thread waitingThread;
        private long reportedIn;
        private long waitingOn;
        private Object reportInLock;
        private Func<bool> releaseEarly;
        private int evaluationFrequency;
        private int waitMilliseconds;

        /// <summary>
        /// A constructor that accepts waitMilliseconds as a maximum wait time and eveluationFrequency. This constructor is designed to be used with the
        /// Release(bool) function, used in a while loop.
        /// </summary>
        /// <param name="waitMilliseconds">The ammount of milliseconds to wait before timming out.</param>
        /// <param name="evaluationFrequency">The ammount of milliseconds between every evaluation of the timeout criteria</param>
        public WaitTimeout(int waitMilliseconds, int evaluationFrequency = 1)
        {
            this.waitMilliseconds       = waitMilliseconds;
            this.evaluationFrequency    = evaluationFrequency;
            this.reportInLock           = new Object();
            this.releaseEarly           = null;
        }

        /// <summary>
        /// A constructor that accepts waitMilliseconds as a maximum wait time before timing out and a function. A return value of true from exitEarlyCriteria() will cause isTimmedOut to 
        /// return true, and Wait() to exit. This constructor is designed to be used with Wait(), depending on the code in exitEarlyCriteria() for early timeout.
        /// </summary>
        /// <param name="waitMilliseconds">The ammount of milliseconds to wait before timming out.</param>
        /// <param name="timeOutEarly">A function that is called every time the timeout status is evaluated. If it returns true, the timeout status is set to true.</param>
        /// <param name="evaluationFrequency">The ammount of milliseconds between every evaluation of the timeout criteria</param>
        public WaitTimeout(int waitMilliseconds, Func<bool> timeOutEarly, int evaluationFrequency = 1)
        {
            this.waitMilliseconds       = waitMilliseconds;
            this.evaluationFrequency    = evaluationFrequency;
            this.reportInLock           = new Object();
            this.releaseEarly           = timeOutEarly;
        }

        private void BeginEvaluating(long waitingOn = 0, bool resetToZero = true)
        {
            this.release    = false;
            this.timedOut   = false;
            if (resetToZero) { this.reportedIn = 0; }
            this.waitingOn  = waitingOn;
            timeout         = DateTime.Now.AddMilliseconds(waitMilliseconds);

            new Thread(delegate()
            {

                while (!this.release && DateTime.Now < timeout)
                {
                    if (releaseEarly != null && releaseEarly()) { break; }
                    Thread.Sleep(this.evaluationFrequency);
                }

                timedOut = true;
                StopWaiting();

            }).Start();
        }

        public bool IsTimedOut()
        {
            return timedOut;
        }

        public void Close()
        {
            this.release    = true;
            this.timedOut   = true;
            StopWaiting();
        }

        public bool Release(bool releaseNow = true)
        {
            this.release = releaseNow;

            // If we are being instructed to force a release right now, then
            // timmedOut must be true. But we should ONLY set this if releaseNow = true
            if (this.release) { this.timedOut = true; }

            StopWaiting();
            return timedOut;
        }

        private void StopWaiting()
        {
            if (waitingThread != null)
            {
                waitingThread.Interrupt();
                waitingThread = null;
            }
        }

        public int GetRemainingMilliseconds()
        {
            if (timeout == null) { return 0; }

            TimeSpan remainingTime = timeout - DateTime.Now;

            return (int)remainingTime.TotalMilliseconds;
        }

        public int GetRemainingSeconds()
        {
            if (timeout == null) { return 0; }

            TimeSpan remainingTime = timeout - DateTime.Now;

            return (int)remainingTime.TotalSeconds;
        }

        public void ReportThreadComplete()
        {
            bool operationComplete = false;

            if (waitingOn == reportedIn) 
            { 
                throw new Exception("More threads reported completion then were expected to report in."); 
            }

            Interlocked.Add(ref reportedIn, 1);
            if (waitingOn == Interlocked.Read(ref reportedIn)) { operationComplete = true; }

            if (operationComplete) { Release(); }
        }

        public long GetNumReportedComplete()
        {
            return Interlocked.Read(ref reportedIn);
        }

        public void Wait()
        {
            waitingThread = Thread.CurrentThread;

            BeginEvaluating();

            try
            {
                Thread.Sleep(System.Threading.Timeout.Infinite);
            }
            catch (Exception) { }
        }

        public bool WaitOn(long numberOfThreads, bool resetToZero = true)
        {
            waitingThread   = Thread.CurrentThread;

            BeginEvaluating(numberOfThreads, resetToZero);

            try
            {
                Thread.Sleep(System.Threading.Timeout.Infinite);
            }
            catch (Exception) { }

            return (waitingOn == reportedIn);
        }
    }
}
