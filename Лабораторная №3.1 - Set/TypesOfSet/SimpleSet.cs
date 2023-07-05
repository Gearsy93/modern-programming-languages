using Set.SetExceptions;

namespace Set.TypesOfSet
{
    internal class SimpleSet : Set // Простое множество 
    {
        public SimpleSet(int n)
        {
            Content = new bool[n+1];
            MaxElement = n;
            for (var i = 0; i <= n; i++) { Content[i] = false; }
        }

        protected sealed override int MaxElement { get; set; }

        private bool[] Content { get; }

        public override void Insert(int a)
        {
            if (!Check(a)) { Content[a] = true; }
            else { throw new ElementIsInSetExсeption(a); }
        }

        public override void Delete(int a)
        {
            if (Check(a)) { Content[a] = false; }
            else { throw new ElementIsNotInExсeption(a); } 
        }

        public override bool Check(int a)
        {
            if (a > MaxElement) throw new IndexOutArrayExсeption(a);
            if (a < 0) { throw new NegativeElementException(a); }
            return Content[a];
        }

        public static SimpleSet operator +(SimpleSet a, SimpleSet b) // Объединение множеств
        {
            SimpleSet result, bigSet, smallSet;
            if (a.MaxElement >= b.MaxElement)
            {
                result = new SimpleSet(a.MaxElement);
                bigSet = a;
                smallSet = b;
            }
            else
            {
                result = new SimpleSet(b.MaxElement);
                bigSet = b;
                smallSet = a;
            }

            for (var i = 0; i <= result.MaxElement; i++)
                if (i <= smallSet.MaxElement)
                {
                    if (a.Check(i) || b.Check(i)) { result.Insert(i); }
                }
                else if (bigSet.Check(i))
                {
                    result.Insert(i);
                }
            return result;
        }

        public static SimpleSet operator *(SimpleSet a, SimpleSet b) // Пересечение множеств
        {
            var result = a.MaxElement >= b.MaxElement ? new SimpleSet(b.MaxElement) : new SimpleSet(a.MaxElement);
            for (var i = 0; i <= result.MaxElement; i++) 
                if (a.Check(i) && b.Check(i)) { result.Insert(i); }
            return result;
        }
    }
}