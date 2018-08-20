using System.Linq;
using NUnit.Framework;

namespace CircularBuffer
{
    [TestFixture]
    public class CircularBufferTest
    {
        private Queue _queue;
        private const int CAPACITY = 5;
        private const int OVER_CAPACITY = CAPACITY + 2;

        [SetUp]
        public void Init()
        {
            _queue = new Queue(CAPACITY);
        }

        [Test]
        public void AddTest()
        {
            foreach (var i in Enumerable.Range(0, CAPACITY))
            {
                var result = _queue.Add(i);
                Assert.AreEqual(result, true, "Enqueue returns bad value!");
            }
            foreach (var i in Enumerable.Range(0, OVER_CAPACITY))
            {
                var result = _queue.Add(i);
                Assert.AreEqual(result, false, "Enqueue returns bad value!");
            }
        }

        [Test]
        public void RemoveTest()
        {
            int resultValue;
            foreach (var i in Enumerable.Range(0, CAPACITY))
            {
                _queue.Add(i);
            }
            foreach (var _ in Enumerable.Range(0, CAPACITY))
            {
                var result = _queue.Remove(out resultValue);
                Assert.AreEqual(result, true, "Dequeue returns bad value!");
            }
            foreach (var _ in Enumerable.Range(0, OVER_CAPACITY))
            {
                var result = _queue.Remove(out resultValue);
                Assert.AreEqual(result, false, "Enqueue returns bad value!");
            }
        }
    }
}
