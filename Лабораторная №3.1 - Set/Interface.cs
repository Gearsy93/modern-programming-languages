using System;
using System.IO;
using Set.SetExceptions;
using Set.TypesOfSet;

namespace Set
{
    internal static class Interface
    {
        public static void SetActions(ref TypesOfSet.Set set)
        {
            CreateSet(ref set);
            InputDataSet(ref set);
            ActivitiesSet(ref set);
        }

        private static void CreateSet(ref TypesOfSet.Set set)
        {
            bool errorFlag;
            do
            {
                try
                {
                    Console.WriteLine("Выберите вид представления множества:\n" +
                                      "1. Перечисление элементов\n" +
                                      "2. Логический массив\n" +
                                      "3. Битовый массив");
                    var typeOfSet = int.Parse(Console.ReadLine() ?? throw new FormatException());
                    Console.WriteLine("Введите максимальное значение элемента во множестве");
                    var maxElement = int.Parse(Console.ReadLine() ?? throw new FormatException());
                    if (maxElement < 0) { throw new NegativeElementException(maxElement); }

                    switch (typeOfSet)
                    {
                        case 1:
                            {
                                set = new MultiSet(maxElement);
                                break;
                            }
                        case 2:
                            {
                                set = new SimpleSet(maxElement);
                                break;
                            }
                        case 3:
                            {
                                set = new BitSet(maxElement);
                                break;
                            }
                        default:
                             {
                                Console.WriteLine("Вы ввели неверный тип множества данные");
                                errorFlag = true;
                                continue;
                            }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Вы ввели неверные данные, повторите ввод\n");
                    errorFlag = true;
                    continue;
                }
                catch (SetException e)
                {
                    Console.WriteLine(e.ErrorText);
                    errorFlag = true;
                    continue;
                }

                errorFlag = false;

            } while (errorFlag);
        }

        private static void InputDataSet(ref TypesOfSet.Set set)
        {
            var errorFlag = false;
            do
            {
                try
                {
                    Console.WriteLine("Выберите вариант ввода данных:\n" +
                                      "1. С клавиатуры\n" +
                                      "2. Из файла");
                    var typeOfEnter = int.Parse(Console.ReadLine() ?? throw new FormatException());
                    switch (typeOfEnter)
                    {
                        case 1:
                        {
                            Console.WriteLine("Введите строку");
                            set.Create(Console.ReadLine() ?? throw new FormatException());
                            break;
                        }
                        case 2:
                        {
                            Console.WriteLine("Введите путь к файлу");
                            string container = null;
                            var path = Console.ReadLine();
                            var file =
                                new StreamReader(path ?? throw new ArgumentException());
                            do
                            {
                                if (file.EndOfStream) break;
                                var str = file.ReadLine();
                                container += str + " ";
                            } while (true);

                            file.Close();

                            if (container != null)
                            {
                                var split = container.Split(' ');
                                var mas = new int[split.Length - 1];
                                for (var i = 0; i < split.Length - 1; i++)
                                {
                                    mas[i] = Convert.ToInt32(split[i]);
                                }

                                set.Create(mas);
                            }
                            else
                            {
                                throw new EmptyFileException(path);
                            }

                            break;
                        }
                        default:
                        {
                            Console.WriteLine("\nКоманда не была распознана, введите еще раз\n");
                            errorFlag = true;
                            continue;
                        }
                    }

                    errorFlag = false;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("\nВы ввели неверный путь, повторите ввод\n");
                    errorFlag = true;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\nВы ввели неверный путь, повторите ввод\n");
                    errorFlag = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nВы ввели пустую строку, повторите ввод\n");
                    errorFlag = true;
                }
                catch (SetException e)
                {
                    if (e is ElementIsInSetExсeption) continue;
                    Console.WriteLine(e.ErrorText);
                    errorFlag = true;
                }
            } while (errorFlag);

        }

        private static void ActivitiesSet(ref TypesOfSet.Set set)
        {
            var exitFlag = false;
            do
            {
                try
                {
                    Console.WriteLine("Выберите действие:\n" +
                                      "1. Добавить элемент\n" +
                                      "2. Удалить элемент\n" +
                                      "3. Проверить наличие элемента во множестве\n" +
                                      "4. Выполнить объединение с другим множеством\n" +
                                      "5. Выполнить пересечение с другим множеством\n" +
                                      "6. Распечатать содержимое множества\n" +
                                      "0. Выход");
                    var typeOfCommad = int.Parse(Console.ReadLine() ?? throw new FormatException());
                    switch (typeOfCommad)
                    {
                        case 1: // Добавление элемента
                        {
                            Console.WriteLine("Введите добавляемый элемент");
                            var a = int.Parse(Console.ReadLine() ?? throw new FormatException());
                            set.Insert(a);
                            break;
                        }
                        case 2: // Удаление элемента
                        {
                            Console.WriteLine("Введите удаляемый элемент");
                            var a = int.Parse(Console.ReadLine() ?? throw new FormatException());
                            set.Delete(a);
                            break;
                        }
                        case 3: // Проверка наличия элемента во множестве
                        {
                            Console.WriteLine("Введите проверяемый элемент");
                            var a = int.Parse(Console.ReadLine() ?? throw new FormatException());
                            Console.WriteLine(set.Check(a) ? "\nПринадлежит\n" : "\nНе принадлежит\n");
                            break;
                        }
                        case 4: // Объединение
                        {
                            Console.WriteLine("Введите максимальное значение элемнта во множестве");
                            var maxElement = int.Parse(Console.ReadLine());
                            TypesOfSet.Set anotherSet;
                            switch (set)
                            {
                                case SimpleSet _:
                                {
                                    anotherSet = new SimpleSet(maxElement);
                                    InputDataSet(ref anotherSet);
                                    var tmp = (SimpleSet)set + (SimpleSet)anotherSet;
                                    set = tmp;
                                    break;
                                }
                                case BitSet _:
                                {
                                    anotherSet = new BitSet(maxElement);
                                    InputDataSet(ref anotherSet);
                                    var tmp = (BitSet)set + (BitSet)anotherSet;
                                    set = tmp;
                                    break;
                                }
                                default:
                                {
                                    Console.WriteLine("Операция объединения не определена для данного класса");
                                    break;
                                }
                            }
                            break;
                        }
                        case 5: // Пересечение
                        {
                            Console.WriteLine("Введите максимальное значение элемента во множестве");
                            var maxElement = int.Parse(Console.ReadLine());
                            TypesOfSet.Set anotherSet;
                            switch (set)
                            {
                                case SimpleSet _:
                                {
                                    anotherSet = new SimpleSet(maxElement);
                                    InputDataSet(ref anotherSet);
                                    var tmp = (SimpleSet)set * (SimpleSet)anotherSet;
                                    set = tmp;
                                    break;
                                }
                                case BitSet _:
                                {
                                    anotherSet = new BitSet(maxElement);
                                    InputDataSet(ref anotherSet);
                                    var tmp = (BitSet)set * (BitSet)anotherSet;
                                    set = tmp;
                                    break;
                                }
                                default:
                                {
                                    Console.WriteLine("Операция объединения не определена для данного класса");
                                    break;
                                }
                            }
                            break;
                            }
                        case 6: // Вывод содержимого множества
                        {
                            set.Print();
                            break;
                        }
                        case 0:
                        {
                            exitFlag = true;
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Команда не была распознана, введите еще раз");
                            break;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Вы ввели неверные данные, повторите ввод\n");
                }
                catch (SetException e)
                {
                    Console.WriteLine(e.ErrorText);
                }

            } while (!exitFlag);
        }
    }
}