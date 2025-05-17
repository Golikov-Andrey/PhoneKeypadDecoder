using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneKeypadDecoderApp.Core.Classes
{
    /*
               Это старый кнопочный телефон с буквенными клавишами, кнопками отмена и отправить.
           Для идентификации кнопки имеют номера. Многократное нажатие кнопки
           выполняет последовательный перебор букв, позволяя каждой кнопке
           представлять более одной буквы. Например однократное нажатие клавиши 2 вернет ‘A’, 
           а двойное нажатие вренет ‘B’
               Необходимо делать паузу в секунду, чтобы напечатать два символа с
           одной клавиши друг за другом : “3 33 333” -> “DEF”

           Задание:
               Разработайте код преобразующий любую последовательность нажатых
           клавиш в корректный вывод
           Подразумевается, что кнопка отправить ‘#’ будет завершать каждую
           последовательность нажатых клавиш
           Примеры:
           “44 45*#” -> “HI”(ошибка в тестовых данных)
           “4433555 555666096667775553#” -> “HELLO WORLD”
           “34447288555#” -> ???
        */


    /// <summary>
    /// Класс статический для простоты использования. не надо создавать экземпляр. 
    /// Для валидации кода выбраны регулярные выражения. Получается быстрая валидация на конечных автоматах
    /// Алгоритм однопроходный. Максимально быстрый по производительности. 
    /// Для ускорения алгоритма перейти от строк и символов к байтам
    /// 
    /// Класс предназначен декодировать нажатия с кнопочного телефона формата: "4433555 555666096667775553#" -> HELLO WORLD
    /// 
    /// Разработал: Голиков Андрей Сергеевич newsc2@yandex.ru
    /// Дата: 16052025
    /// Версия: 1.0
    /// </summary>
    public static class PhoneKeypadDecoder
    {
        //Словарь позволяет осуществлять быстрый поиск
        private static readonly Dictionary<char, char[]> KeyMap = new Dictionary<char, char[]>
            {
                {'2', new char[] {'A', 'B', 'C'}},
                {'3', new char[] {'D', 'E', 'F'}},
                {'4', new char[] {'G', 'H', 'I'}},
                {'5', new char[] {'J', 'K', 'L'}},
                {'6', new char[] {'M', 'N', 'O'}},
                {'7', new char[] {'P', 'Q', 'R', 'S'}},
                {'8', new char[] {'T', 'U', 'V'}},
                {'9', new char[] {'W', 'X', 'Y', 'Z'}},
                {'0', new char[] {' '}}
            };

        /// <summary>
        /// Метод декодирует последовательность нажатий в текстовое сообщение
        /// </summary>
        /// <param name="input">Последовательность нажатий кнопок</param>
        /// <returns>Возвращает декодированную последовательность нажатий</returns>
        /// <exception cref="ArgumentException">Последовательность не прошла валидацию</exception>
        public static string DecodeInput(string input)
        {
            // Валидация перед обработкой
            if (ValidateInput(input))
            {
                //Результат декодирования
                string result = string.Empty;
                //Разделяем строку на группы цифр 
                string[] currentGroup = input.Split(' ');


                //Обработка групп цифр последовательно   
                foreach(string group in currentGroup)
                {
                    result += DecodeGroupDigits(group);  
                }

                return result;
            }
            else
            {
                //Последовательность имеет не корректный формат
                throw new ArgumentException("Invalid format: wrong letter/digit or end of group");
            }
        }

        /// <summary>
        /// Метод декодирует группу цифр, независимо от наличия '#' в конце
        /// </summary>
        /// <param name="group">Группа цифр для декодирования</param>
        /// <returns>Декодированную группу цифр</returns>
        private static string DecodeGroupDigits(string group)
        {
            //Число цифр подряд
            int count = 1;

            //Накапливаем результат
            List<char> decodeResult = new List<char>();
            //Цифра для декодирования
            char key = group[0];

            //Перебираем все цифры и не доходим один символ
            for (int i = 0; i < group.Length - 1; i++)
            {

                if (group[i] != '*')
                {
                    //Цифра не изменилась и их количество меньше 4-х
                    if (group[i + 1] == key && count < 4)
                    {
                        count++;
                    }
                    else
                    {
                        //Смена цифры, декодируем старую последовательность цифр
                        decodeResult.Add(KeyMap[key][count-1]);
                        //Сохраняем новую цифру и сбрасываем счетчик
                        key = group[i + 1];
                        count = 1;
                    }
                }
                else
                {
                    //Нажата звездочка, стираем последнюю декодированную букву
                    if (decodeResult.Count==0)
                        throw new InvalidOperationException("Maximum identifier reached");
                    decodeResult.RemoveAt(decodeResult.Count-1);
                }                
            }

            //Обрабатываем последнюю цифру в последовательности
            key = group[group.Length-1];
            //При решетке ничего не делаем
            if (key!='#')
            {
                if (key != '*')
                { 
                    //Декодируем последнюю цифру
                    decodeResult.Add(KeyMap[key][count-1]);
                }
                else
                {
                    //Нажата звездочка, стираем последнюю декодированную букву
                    if (decodeResult.Count == 0)
                        throw new InvalidOperationException("Maximum identifier reached");
                    decodeResult.RemoveAt(decodeResult.Count - 1);
                }
            }

            return new string(decodeResult.ToArray()); ;
        }


        /// <summary>
        /// Метод проверяет корректность последовательности цифр для декодирования
        /// </summary>
        /// <param name="input">входная последовательность</param>
        /// <returns>результат валидации</returns>
        /// <exception cref="ArgumentException">Ошибки при валидации</exception>
        public static bool ValidateInput(string input)
        {
            // Регулярное выражение для проверки:
            // 1. Допустимые символы: 2-9, 0, *, пробелы и # в конце
            // 2. Пробелы только между группами, не в начале/конце
            // 3. # может быть только один и в конце
            const string pattern = @"^[2-90*]+(?: [2-90*]+)*#?$";

            // Проверка на недопустимые символы и структуру
            if (!Regex.IsMatch(input, pattern))
                throw new ArgumentException("Invalid characters or format");

            // Дополнительные проверки:
            // 1. # должен быть только в конце 
            int hashIndex = input.IndexOf('#');
            if (hashIndex != -1 && hashIndex != input.Length - 1)
                throw new ArgumentException("The '#' character should only be at the end.");

            // 2. Не более одного #
            if (input.Count(c => c == '#') > 1)
                throw new ArgumentException("There should be only one character '#'.");

            return true;
        }
    }
}
