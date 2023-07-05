kusing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Set.TypesOfSet
{
    internal abstract class Set // Множество
    {
        protected abstract int MaxElement { get; set; } // Максимальный элемент множества

        public abstract void Insert(int a); // Добавление элемента
        public abstract void Delete(int a); // Удаление элемента
        public abstract bool Check(int a); // Проверка наличия элемента во множестве

        public void Create(string str) // Заполнение множества из строки
        {
            Create(str.Split(' ').Select(int.Parse).ToArray());
        }

        public void Create(IEnumerable<int> mas) // Заполнение множества из массива
        {
            foreach (var elem in mas) Insert(elem);
        }

        public void Print() // Вывод содержимого
        {
            var result = new StringBuilder();
            for (var i = 0; i <= MaxElement; i++)
                if (Check(i)) { result.Append($"{i} "); }
            Console.WriteLine(result.ToString() != "" ? result.ToString() : "Пустое множество");
        }
    }
}