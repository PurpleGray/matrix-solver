using System;
using System.Security.Cryptography;

namespace MatrixSolver.Library.Utils
{
    public class RandomDataGenerator : Random
    {
        private readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        private readonly byte[] _uint32Buffer = new byte[4];

        public override int Next()
        {
            _rng.GetBytes(_uint32Buffer);
            return BitConverter.ToInt32(_uint32Buffer, 0) & 0x7FFFFFFF;
        }

        public override int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue");
            }

            return Next(0, maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue");
            }

            if (minValue == maxValue)
            {
                return minValue;
            }

            long diff = maxValue - minValue;
            while (true)
            {
                _rng.GetBytes(_uint32Buffer);
                var rand = BitConverter.ToUInt32(_uint32Buffer, 0);
                var max = 1 + (long)uint.MaxValue;
                var remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (int)(minValue + rand % diff);
                }
            }
        }

        public override double NextDouble()
        {
            _rng.GetBytes(_uint32Buffer);
            var rand = BitConverter.ToUInt32(_uint32Buffer, 0);
            return rand / (1.0 + uint.MaxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            _rng.GetBytes(buffer);
        }
    }
}
