using System;

namespace Set.SetExceptions
{
    public class SetException : Exception // Ошибка множества, родительский класс
    {
        protected internal string ErrorText { get; protected set; }
    } 
}