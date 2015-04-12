namespace GAB.Services.OrderConsumer
{
    using System;
    using System.Diagnostics;

    public static class TimedOperation
    {
        public static double Run(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            action.Invoke();

            stopwatch.Stop();

            return stopwatch.Elapsed.TotalSeconds;
        }
    }
}