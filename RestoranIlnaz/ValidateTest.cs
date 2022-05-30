using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranIlnaz
{
    public static class ValidateTest
    {
        public static string ValidateRegistration(string Login, string Password, string FIO)
        {
            if (Login.Length < 3)
            {
                return "Длина логина должна быть больше 3х символов";
            }

            if (Password.Length < 4)
            {
                return "Длина пароля должна быть больше 4х символов";
            }

            if (FIO.Split(' ').Length != 3)
            {
                return "ФИО должно состоять из 3х слов";
            }

            return "Успешно";
        }
    }
}
