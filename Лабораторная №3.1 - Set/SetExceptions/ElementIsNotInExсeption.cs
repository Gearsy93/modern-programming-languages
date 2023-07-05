namespace Set.SetExceptions
{
    public class ElementIsNotInExсeption : SetException // Удаление несуществующего элемента
    {
        public ElementIsNotInExсeption(int a)
        {
            ErrorText = "\nЭлемент " + a + " отстутствует во множестве\n";
        }
    } 
}