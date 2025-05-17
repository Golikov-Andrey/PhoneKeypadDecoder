using PhoneKeypadDecoderApp.Core.Classes;

//Тестовые данные
string[] tests = {
            "44 4445*#",                   // HI
            "44 45*#",                     // HG
            "4433555 555666096667775553#", // HELLO WORLD
            "34447288555#",                // FVU
            "12A3#",                       // Ошибка
            "44##",                        // Ошибка
            "44  "                         // Ошибка
        };

//Димонстрация работы алгоритма на тестовых данных
foreach (var test in tests)
{
    try
    {
        //Вызов базового и альтернативного алгоритма
        Console.WriteLine($"Базовое решение: Ввод: {test} -> Результат: {PhoneKeypadDecoder.DecodeInput(test)}");
        Console.WriteLine($"Альтернативное:  Ввод: {test} -> Результат: {AlternativDecoder.DecodeInput(test)}");
    }
    catch (Exception ex)
    {
        //Перехват ошибки
        Console.WriteLine($"Ввод: {test} -> Ошибка: {ex.Message}");
    }
}