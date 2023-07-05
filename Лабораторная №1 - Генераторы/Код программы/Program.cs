using System.IO;
using System.Windows.Forms;
using static wishes_generator.model;
using static wishes_generator.Logic;
using static wishes_generator.Interface;

namespace Generators
{
    class Program
    {
        static void Main(string[] args)
        {
            //Собираем все входные данные переменную values пользовательского типа Data
            //Обработанные в логике данные находятся в fields
            Data values;
            Fields fields;

            //Интерфейс - считать
            values = LoadFromExcel(Directory.GetCurrentDirectory() + @"\input.xlsx");

            //Логика - создать обращения и пожелания
            fields = CreateWishes(values);

            //Интерфейс - создать документы, скопировать шаблон несколько раз, поменять поля
            CreateWord(fields);

            MessageBox.Show("Генерация поздравлений завершена",
                    "Сообщение");
        }

        
    }
}
