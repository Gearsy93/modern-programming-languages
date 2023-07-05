namespace Set.SetExceptions
{
    public class NegativeElementException : SetException // Отрицательный элемент
    {
        public NegativeElementException(int a)
        {
            ErrorText = "\nЭлемент " + a + " не может входить во множество, так как он отрицателен\n";
        }
    } 
}