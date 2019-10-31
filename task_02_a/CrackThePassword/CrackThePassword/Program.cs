using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrackThePassword
{
    class Program
    {
        private static SHA256Managed Sha256;

        private static int _MaxDayNumber = 31;
        private static int _MaxMonthNumber = 12;
        private static int _MaxYearNumber = 9999;

        private static bool _isPasswordCracked = false;

        private static string _stringDate;

        private static string _PasswordHash =
            "d887b8314484a54a1aafca4da2fa542ac3d8ff8cdf51625db97b15bd5c098cf0";

        static void Main(string[] args)
        {
            Sha256 = new SHA256Managed();

            for (int _yearNumber = 1; _yearNumber <= _MaxYearNumber; 
                _yearNumber++)
            {
                for (int _monthNumber = 1; _monthNumber <= _MaxMonthNumber; 
                    _monthNumber++)
                {
                    for (int _dayNumber = 1; _dayNumber <= _MaxDayNumber; 
                        _dayNumber++)
                    {
                        if (((_yearNumber % 4 == 0) &&
                            (_monthNumber == 2) && (_dayNumber > 29)) ||
                            ((_monthNumber == 2) && (_dayNumber > 28)) ||
                            ((_monthNumber == 4) && (_dayNumber > 30)) ||
                            ((_monthNumber == 6) && (_dayNumber > 30)) ||
                            ((_monthNumber == 9) && (_dayNumber > 30)) ||
                            ((_monthNumber == 11) && (_dayNumber > 30)))
                        {
                            continue;
                        }
                        _stringDate = _dayNumber.ToString() + _monthNumber.ToString() +
                            _yearNumber.ToString();
                        Console.WriteLine(_stringDate);

                        var hash = GetHash(_stringDate);

                        if (string.Compare(_PasswordHash, hash) == 0)
                        {
                            Console.WriteLine("Дата: " + _stringDate);
                            Console.WriteLine("Хэш-функция: " + _PasswordHash);
                            Console.WriteLine("Пароль взломан!");
                            Console.ReadLine();
                            _isPasswordCracked = true;
                            break;
                        }
                    }
                }
            }

            if (!_isPasswordCracked)
            {
                Console.WriteLine("Не удалось взломать пароль.");
                Console.ReadLine();
            }

            //var hash = GetHash("752019"); // Пример вызова
        }

        //Считает хэш любой строки
        public static string GetHash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = Sha256.ComputeHash(bytes);
            StringBuilder hashStringBuilder = new StringBuilder(64);

            foreach (byte x in hash)
            {
                hashStringBuilder.Append(string.Format("{0:x2}", x));
            }

            return hashStringBuilder.ToString();
        }
    }
}
