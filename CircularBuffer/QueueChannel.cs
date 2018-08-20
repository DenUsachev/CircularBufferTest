using System;
using System.Threading;
using System.Threading.Tasks;

namespace CircularBuffer
{
    class QueueChannel
    {
        private const int POLL_INTERVAL_MS = 100;
        private readonly Queue _queue;
        private static readonly object Locker = new object();

        public QueueChannel(int capacity)
        {
            _queue = new Queue(capacity);
        }

        public Task<bool> Add(int value)
        {
            TaskCompletionSource<bool> taskCompletion = null;
            Timer timer = null;
            timer = new Timer(delegate
            {
                if (timer != null)
                {
                    timer.Dispose();
                }
                lock (Locker)
                {
                    var result = _queue.Add(value);
                    if (result)
                    {
                        taskCompletion.SetResult(true);
                    }
                }
            });
            taskCompletion = new TaskCompletionSource<bool>(timer);
            timer.Change(POLL_INTERVAL_MS, Timeout.Infinite);
            return taskCompletion.Task;
        }
        public Task<Tuple<bool, int>> Remove()
        {
            TaskCompletionSource<Tuple<bool, int>> taskCompletion = null;
            Timer timer = null;
            timer = new Timer(delegate
            {
                if (timer != null)
                {
                    timer.Dispose();
                }
                lock (Locker)
                {
                    int resultlValue;
                    var result = _queue.Remove(out resultlValue);
                    if (result)
                    {
                        taskCompletion.SetResult(Tuple.Create(result, resultlValue));
                    }
                }
            });
            taskCompletion = new TaskCompletionSource<Tuple<bool, int>>(timer);
            timer.Change(POLL_INTERVAL_MS, Timeout.Infinite);
            return taskCompletion.Task;
        }
    }
}
