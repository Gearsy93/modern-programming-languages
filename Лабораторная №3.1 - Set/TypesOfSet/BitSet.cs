using System;
using Set.SetExceptions;

namespace Set.TypesOfSet
{
    internal class BitSet : Set // Битовое множество
    {
        private readonly uint[] _code;

        public BitSet(int n)
        {
            MaxElement = n;
            MaxBlock = n / 32 + 1;
            _code = new uint[MaxBlock]; 
        }

        private int MaxBlock { get; }

        protected sealed override int MaxElement { get; set; }

        private static int SearchNumerOfBlock(int a)
        {
            var number = a / 32;
            return number;
        }

        public override void Insert(int a)
        {
            var c = a%32;
            uint tmp = 1;
            tmp <<= a;
            if (!Check(a))
            {
                _code[SearchNumerOfBlock(a)] |= tmp;
            }
            else
            {
                throw new ElementIsInSetExсeption(a);
            }
        }

        public override void Delete(int a)
        {
            var c = a % 32;
            uint tmp = 1;
            tmp <<= a;
            if (Check(a))
            {
                _code[SearchNumerOfBlock(a)] &= ~tmp;
            }
            else
            {
                throw new ElementIsNotInExсeption(a);
            }
        }

        public override bool Check(int a)
        {
            if (a > MaxElement) throw new IndexOutArrayExсeption(a);
            if (a < 0) { throw new NegativeElementException(a); }
            var tmp = _code[SearchNumerOfBlock(a)];
            var e = a % 32;
            tmp >>= e;
            return tmp % 2 == 1;
        }

        public static BitSet operator +(BitSet a, BitSet b) // Объединение множеств
        {
            var flag = a.MaxElement >= b.MaxElement;
            var maxElement = flag ? a.MaxElement : b.MaxElement;
            var maxBlock = flag ? a.MaxBlock-1 : b.MaxBlock-1; 
            var result = new BitSet(maxElement);
            var minBlock = flag ? b.MaxBlock-1 : a.MaxBlock-1;
            for (var i = 0; i <= maxBlock; i++)
            {
                if (i <= minBlock) { result._code[i] = a._code[i] | b._code[i]; }
                else { result._code[i] = flag ? a._code[i] : b._code[i]; }
            }
            return result;
        }

        public static BitSet operator *(BitSet a, BitSet b) // Пересечение множеств
        {
            var n = a.MaxElement <= b.MaxElement ? a.MaxElement : b.MaxElement;
            var result = new BitSet(n);
            for (var i = 0; i < result.MaxBlock; i++)
                result._code[i] = a._code[i] & b._code[i];
            return result;
        }
    }
}