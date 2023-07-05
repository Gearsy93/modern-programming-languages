using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static wishes_generator.model;

namespace wishes_generator
{
    class Logic
    {
        public static Fields CreateWishes(Data values)
        {
            //Инициализация
            int uniques = 0,
                overall_unique_triads = 0,
                n = values.wish_lists.values.Count;
            Fields fields;
            Field field;
            string temp_main;
            Wish_Lists used_list;
            used_list.values = new List<Wish_List>();
            List<string> appeals = new List<string>();
            List<string> triads = new List<string>();
            List<string> groups = new List<string>();
            fields.path = values.settings.path;
            fields.font = values.settings.font;
            fields.values = new List<Field>();
            Random rnd = new Random();

            //Определеляем возможность создания уникальных триад:
            //Т.к. нам не важен порядок, нужно посчитать сочетания без повторений, причем в каждом группе может быть несколько пожеланий
            //Пусть в каждой группе пожеланий всего одна фраза. Если мы добавим хотя бы 1 группе еще одно, общее число возможных уникальных триад увеличится вдвое. Если добавить 2 - втрое.
            //Тогда общее число уникальных триад можно посчитать по формуле: C(k,n) * сумма всех уникальных пожеланий
            //В моем случае (готовой выборке групп и фраз) получается около 400 уникальных вариаций
            foreach (Wish_List temp_list in values.wish_lists.values)
            {
                uniques += temp_list.values.Count;
            }

            overall_unique_triads = Fact(n) / (Fact(3) * Fact(n - 3)) * uniques;

            if (overall_unique_triads < values.persons.Persons.Count)
            {
                MessageBox.Show("Пополните фразы-пожелания или добавьте новые группы пожеланий",
                    "Сообщение");
            }

            //Создаем обращения, в зависимости от пола, окончание обращения должно меняться
            foreach (Person person in values.persons.Persons)
            {
                string temp = "Дорог";
                if (person.Sex == "М")
                {
                    temp += "ой, ";
                }
                else
                {
                    temp += "ая, ";
                }

                temp += person.Name + '!';
                appeals.Add(temp);
            }

            //Создаем список групп пожеланий для отслеживания использованных фраз, по заданию предпочтение отдается неиспользованным 
            foreach (Wish_List wish in values.wish_lists.values)
            {
                Wish_List empty_wish;
                empty_wish.Name = wish.Name;
                groups.Add(wish.Name);
                empty_wish.values = new List<string>();
                used_list.values.Add(empty_wish);
            }

            temp_main = "Поздравляю вас с масленицей! Желаю ";

            //Всего количество поздравлений = количеству имен в документе
            foreach (Person person in values.persons.Persons)
            {
                string other_temp_main = "" + temp_main,
                    current_wish;
                //Всего 3 группы на одну триаду, повторяться не должны
                List<string> used_groups = new List<string>();

                while (used_groups.Count != 3)
                {
                    //По заданию группы выбираются случайно
                    string current_group = groups[rnd.Next(0, groups.Count)];

                    if (!(used_groups.Contains(current_group)))
                    {
                        used_groups.Add(current_group);

                        foreach (Wish_List group in values.wish_lists.values)
                        {
                            if (group.Name == current_group)
                            {
                                bool check = true;
                                while (check)
                                {
                                    //Фраза выбирается также случайно, но если в списке использованных она присутствует, выбираем случайно следующую фразу
                                    int random_number = rnd.Next(0, group.values.Count);
                                    current_wish = group.values[random_number];

                                    int k = 0;
                                    foreach (Wish_List again_list in used_list.values)
                                    {
                                        if (again_list.Name == current_group)
                                        {
                                            if (!(again_list.values.Contains(current_wish)))
                                            {
                                                check = false;
                                                used_list.values[k].values.Add(current_wish);

                                                other_temp_main += current_wish + ", ";

                                                //Если все фразы из группы использованы, мы можем использовать заново их, поэтому очищаем список использованных фраз группы
                                                if (used_list.values[k].values.Count == values.wish_lists.values[k].values.Count)
                                                {
                                                    used_list.values[k].values.Clear();
                                                }
                                                break;
                                            }
                                        }
                                        k++;
                                    }
                                }
                            }
                        }
                    }
                    else continue;
                }
                triads.Add(other_temp_main);
            }
            for (int i = 0; i < values.persons.Persons.Count; i++)
            {
                field.appeal = appeals[i];
                field.wish = triads[i];
                field.wish = field.wish.Substring(0, field.wish.Length - 2);
                field.wish += '!';
                fields.values.Add(field);
            }
            return fields;
        }
        //Для рассчета уникальных триад
        public static int Fact(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * Fact(n - 1);
        }
    }
}
