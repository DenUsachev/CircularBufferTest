namespace CircularBuffer
{
    public class Queue
    {
        private readonly int[] _items;
        private int _count;
        private int _head;
        private int _tail;

        public Queue(int capacity)
        {
            _items = new int[capacity];
            _head = _tail = 0;
            _count = 0;
        }

        public bool Add(int value)
        {
            if (_count++ >= _items.Length)
            {
                return false;
            }
            _tail = _tail % _items.Length;
            _items[_tail] = value;
            _tail += 1;
            return true;
        }

        public bool Remove(out int value)
        {
            value = 0;
            if (_count-- > 0)
            {
                _head = (_head += 1) % _items.Length;
                value = _items[_head];
                return true;
            }
            return false;
        }
    }
}
