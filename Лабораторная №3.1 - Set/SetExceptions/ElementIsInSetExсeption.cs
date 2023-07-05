namespace Set.SetExceptions
{
    public class ElementIsInSetExсeption : SetException // Нарушение уникальности элементов
    {
        public ElementIsInSetExсeption(int a)
        {
            ErrorText = "\nЭлемент " + a + " уже есть во множестве, он не будет добавлен\n";
        }
    } 
}