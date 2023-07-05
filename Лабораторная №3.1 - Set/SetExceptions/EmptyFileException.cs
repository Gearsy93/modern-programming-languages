namespace Set.SetExceptions
{
    public class EmptyFileException : SetException // Пустой файл
    {
        public EmptyFileException(string str)
        {
            ErrorText = "\nФайл расположенный в " + str + " пуст\n";
        }
    }
}