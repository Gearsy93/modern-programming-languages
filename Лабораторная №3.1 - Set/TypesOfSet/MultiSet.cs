using Set.SetExceptions;

namespace Set.TypesOfSet
{
    internal class MultiSet : Set // Мультимножество
    {
        private readonly int[] _m;

        public MultiSet(int n)
        {
            MaxElement = n;
            _m = new int[n+1];
        }

        protected sealed override int MaxElement { get; set; }

        public override void Insert(int a)
        {
            if (a > MaxElement) { throw new IndexOutArrayExсeption(a); }
            _m[a]++;
        }

        public override void Delete(int a)
        {
            if (!Check(a)) { throw new ElementIsNotInExсeption(a); }
            _m[a]--;
        }

        public override bool Check(int a)
        {
            if (a > MaxElement) { throw new IndexOutArrayExсeption(a); }
            if (a < 0) { throw new NegativeElementException(a); }
            return _m[a] != 0;
        }
    }
}