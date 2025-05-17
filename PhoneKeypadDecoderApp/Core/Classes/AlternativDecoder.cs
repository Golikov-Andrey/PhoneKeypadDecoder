using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneKeypadDecoderApp.Core.Classes
{
    /// <summary>
    /// Класс статический для простоты использования. не надо создавать экземпляр. 
    /// 
    /// Альтернативное решение задачи(PhoneKeypadDecoder). Решение медленное, но более понятное.  
    /// В основе лежит поэтапная замена серий цифр.
    /// Для валидации использован метод из класса PhoneKeypadDecoder
    /// 
    /// Класс предназначен декодировать нажатия с кнопочного телефона формата: "4433555 555666096667775553#" -> HELLO WORLD
    /// 
    /// Разработал: Голиков Андрей Сергеевич newsc2@yandex.ru
    /// Дата: 16052025
    /// Версия: 1.0
    /// </summary>
    public class AlternativDecoder
    {
        //Массив автозамен серий цифр, от длинных к коротким
        private static readonly string[,] ReplaseArr = new string[27, 2]
            {
                {"222","C"},{"22","B"},{"2","A"},
                {"333","F"},{"33","E"},{"3","D"},
                {"444","I"},{"44","H"},{"4","G"},
                {"555","L"},{"55","K"},{"5","J"},
                {"666","O"},{"66","N"},{"6","M"},
                {"7777","S"},{"777","R"},{"77","Q"},{"7","P"},
                {"888","V"},{"88","U"},{"8","T"},
                {"9999","Z"},{"999","Y"},{"99","X"},{"9","W"},
                {"0"," "}
            };

        //Массив удалений символов при нажатии '*'
        private static readonly string[] RemoveArr = new string[27]
            {
                "A*","B*","C*","D*","E*","F*","G*","H*","I*","J*",
                "K*","L*","M*","N*","O*","P*","Q*","R*","S*","T*","U*",
                "V*","W*","X*","Y*","Z*","#"
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
            if (PhoneKeypadDecoder.ValidateInput(input))
            {
                //Результат декодирования и буфер
                string result = string.Empty;
                string buffer = string.Empty;

                //Разделяем строку на группы цифр 
                string[] currentGroup = input.Split(' ');

                //Обработка групп цифр последовательно   
                foreach (string group in currentGroup)
                {
                    buffer=group;

                    //Заменяем серии цифр на буквы
                    for (int i = 0; i < 27; i++)
                    {
                        buffer = buffer.Replace(ReplaseArr[i, 0], ReplaseArr[i, 1]);
                    }

                    //Обрабатываем нажатие '*'
                    for (int i = 0; i < 27; i++)
                    {
                        buffer = buffer.Replace(RemoveArr[i], "");
                    }

                    result += buffer;
                }

                return result;
            }
            else
            {
                //Последовательность имеет не корректный формат
                throw new ArgumentException("Invalid format: wrong letter/digit or end of group");
            }
        }
       
    }
}
