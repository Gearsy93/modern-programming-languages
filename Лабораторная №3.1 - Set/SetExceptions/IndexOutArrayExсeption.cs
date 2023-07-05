namespace Set.SetExceptions
{
    public class IndexOutArrayExсeption : SetException // Выход за пределы массива
    {
        public IndexOutArrayExсeption(int a)
        {
            ErrorText = "\nЭлемент " + a + " больше максимального допустимого элемента во множестве\n";
        }
    } 
}