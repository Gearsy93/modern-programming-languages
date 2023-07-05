using System;
using System.IO;
using System.Collections.Generic;
using static wishes_generator.model;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace wishes_generator
{
    class Interface
    {
        public static Data LoadFromExcel(string excel_name)
        {
            Data values;
            string text_name,
                text_sex,
                text,
                text_font,
                text_path;
            values.persons.Persons = new List<Person>();
            values.wish_lists.values = new List<Wish_List>();

            //Открываем документ с входными данными, книга одна
            Excel.ApplicationClass excel = null;
            Excel.Workbook workbook = null;

            try
            {
                excel = new Excel.ApplicationClass();
                excel.Visible = true;
                workbook = excel.Workbooks.Open(excel_name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //А листов уже 3
            if (excel != null && workbook != null)
            {
                Excel.Worksheet names = (Excel.Worksheet)workbook.Worksheets["Имена"];
                Excel.Worksheet wishes = (Excel.Worksheet)workbook.Worksheets["Списки_Пожелания"];
                Excel.Worksheet settings = (Excel.Worksheet)workbook.Worksheets["Настроечные данные"];

                //Далее считываем все данные в соответствующие пользовательские данные
                for (int i = 1; i < names.Rows.Count; i++)
                {
                    var cell_name = (Excel.Range)names.Cells[i + 1, 1];
                    var cell_sex = (Excel.Range)names.Cells[i + 1, 2];
                    var value_name = cell_name.Value;
                    var value_sex = cell_sex.Value;

                    if (value_name == null || value_sex == null) break;

                    text_name = value_name.ToString();
                    text_sex = value_sex.ToString();

                    Person temp;
                    temp.Name = text_name;
                    temp.Sex = text_sex;
                    values.persons.Persons.Add(temp);
                }

                for (int i = 0; i < wishes.Columns.Count; i++)
                {
                    var temp = (Excel.Range)wishes.Cells[1, i + 1];
                    var temp_value = temp.Value;
                    if (temp_value == null) break;

                    Wish_List temp_list;
                    //j = 0
                    {
                        var cell = (Excel.Range)wishes.Cells[1, i + 1];
                        var value = cell.Value;

                        if (value == null) break;

                        text = value.ToString();
                        temp_list.Name = text;
                        temp_list.values = new List<string>();
                    }
                    for (int j = 1; j < wishes.Rows.Count; j++)
                    {
                        var cell = (Excel.Range)wishes.Cells[j + 1, i + 1];
                        var value = cell.Value;

                        if (value == null) break;

                        text = value.ToString();
                        temp_list.values.Add(text);
                    }
                    values.wish_lists.values.Add(temp_list);
                }

                var cell_font = (Excel.Range)settings.Cells[2, 1];
                var cell_path = (Excel.Range)settings.Cells[2, 2];
                var value_font = cell_font.Value;
                var value_path = cell_path.Value;

                if (!(value_font == null || value_path == null))
                {
                    Settings temp;
                    text_font = value_font.ToString();
                    text_path = value_path.ToString();

                    temp.font = text_font;
                    temp.path = text_path;
                    values.settings = temp;
                }
                else
                {
                    throw new Exception("Font or Path wasn't found");
                }
                if (excel != null) excel.Quit();

                return values;
            }
            else
            {
                throw new Exception("File is not available");
            }
        }

        public static void CreateWord(Fields fields)
        {
            int number = 1;
            Word.Range bookmark_range;

            //Проверяем, создана ли локальная папка с файлами выходных данных
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Выходные данные"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Выходные данные");
            }
            //Уникальные имена выходных документов - просто итерация от 1...
            while (true)
            {
                if (File.Exists(Directory.GetCurrentDirectory() + @"\Выходные данные\" + number.ToString() + ".docx"))
                {
                    number++;
                }
                else
                {
                    var file = File.Create(Directory.GetCurrentDirectory() + @"\Выходные данные\" + number.ToString() + ".docx");
                    file.Close();
                    break;
                }
            }

            //Открываем 2 документа - новый созданны и шаблон
            Word.ApplicationClass word = null;
            Word.Document document = null;
            Word.ApplicationClass word_template = null;
            Word.Document document_template = null;

            try
            {
                word = new Word.ApplicationClass();
                word.Visible = true;
                document = word.Documents.Open(Directory.GetCurrentDirectory() + @"\Выходные данные\" + number.ToString() + ".docx");
                document.Activate();

                word_template = new Word.ApplicationClass();
                word_template.Visible = true;
                document_template = word_template.Documents.Open(fields.path);
                document.Activate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Начинаем обработку нового документа
            if (word != null && document != null && word_template != null && document_template != null)
            {
                //В моем случае шаблон в альбомной ориентации, так что меняем ориентацию нового документа
                document.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

                //Теперь для каждого человека добавляем шаблон в конец нового документа
                foreach (Field field in fields.values)
                {
                    object docstart = document.Content.End - 1;
                    object docend = document.Content.End;

                    //Просто копируем содержимое
                    document_template.Content.Copy();
                    Word.Range range = document.Range(ref docstart, ref docend);
                    range.Paste();

                    int num = 0;
                    while(true)
                    {
                        //Если ошибка документ занят
                        try
                        {
                            //Считываем закладки документа (перенеслись с шаблон), меняем текст и шрифт
                            foreach (Word.Bookmark bookmark in document.Bookmarks)
                            {
                                bookmark_range = bookmark.Range;

                                if (num == 0)
                                {
                                    bookmark_range.Text = field.appeal;
                                    bookmark_range.Font.Name = fields.font;
                                }
                                else
                                {
                                    bookmark_range.Text = field.wish;
                                    bookmark_range.Font.Name = fields.font;
                                }
                                num++;
                            }
                            break;
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    //При добавлении нового шаблона нужно удалить прошлые закладки, ведь имя всегда одинаковое
                    foreach (Word.Bookmark bookmark in document.Bookmarks)
                    {
                        bookmark.Delete();
                    }

                }

                //Где-то добавляется пустой символ, нужно его удалить, ведь получаетя лишняя страница
                object start = document.Content.End - 1;
                object end = document.Content.End;
                Word.Range range_empty = document.Range(ref start, ref end);
                range_empty = document.Range(ref start, ref end);
                range_empty.Delete();

                document.Save();
            }

            if (word != null)
                word.Quit();
            if (word_template != null)
                word_template.Quit(0);

            return;
        }
    }
}
