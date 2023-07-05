using System.Collections.Generic;

namespace wishes_generator
{
    class model
    {
        //Для интерфейса
        public struct Person
        {
            public string Name;
            public string Sex;
        }

        public struct Person_List
        {
            public List<Person> Persons;
        }

        public struct Wish_List
        {
            public string Name;
            public List<string> values;
        }

        public struct Wish_Lists
        {
            public List<Wish_List> values;
        }

        public struct Settings
        {
            public string font;
            public string path;
        }

        public struct Data
        {
            public Person_List persons;
            public Wish_Lists wish_lists;
            public Settings settings;
        }

        //Для логики
        public struct Field
        {
            public string appeal;
            public string wish;
        }

        public struct Fields
        {
            public string font;
            public string path;
            public List<Field> values;
        }
    }
}
