using System;
using System.Collections.Generic;
using System.Text;

namespace ShortLink.Application.Services
{
    public class UniqueIdGenerator : IUniqueIdGenerator
    {
        private static UniqueIdGenerator _instance;
        private static readonly object Lock = new object();
        private readonly string _dictionary = "0123456789abcdefghijklmnopqrstuvwxyz";
        private readonly List<int> _sequence;

        public static void Configure(string firstId)
        {
            lock (Lock)
            {
                if (_instance == null)
                {
                    _instance = new UniqueIdGenerator(firstId);
                }
                else
                {
                    throw new InvalidOperationException("Object already exist");
                }
            }
        }

        public static UniqueIdGenerator GetInstance()
        {
            lock (Lock)
            {
                if (_instance == null)
                {
                    _instance = new UniqueIdGenerator();
                }
            }
            return _instance;
        }

        public UniqueIdGenerator()
        {
            _sequence = new List<int> {0};
        }

        public UniqueIdGenerator(string firstId)
        {
            if (string.IsNullOrEmpty(firstId))
            {
                _sequence = new List<int> {0};
            }
            else
            {
                _sequence = new List<int>();
                for (int i = firstId.Length - 1; i >= 0; i--)
                {
                    var index = _dictionary.IndexOf(firstId[i]);
                    if (index < 0)
                    {
                        throw new ArgumentException("Key does not match dictionary");
                    }
                    _sequence.Add(_dictionary.IndexOf(firstId[i]));
                }
            }
        }

        public string GetId()
        {
            var builder = new StringBuilder();
            lock (_sequence)
            {
                Increment(_sequence);
                for (int i = _sequence.Count - 1; i >= 0; i--)
                {
                    builder.Append(_dictionary[_sequence[i]]);
                }
            }
            return builder.ToString();
        }

        private void Increment(List<int> list, int currentIndex = 0)
        {
            if (list[currentIndex] < _dictionary.Length - 1)
            {
                list[currentIndex]++;
            }
            else
            {
                list[currentIndex] = 0;
                currentIndex++;
                if (currentIndex > list.Count - 1)
                {
                    list.Add(0);
                }
                Increment(list, currentIndex);
            }
        }
    }
}
