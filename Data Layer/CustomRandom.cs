using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Data_Layer
{
    public static class CustomRandom
    {
        static public int GetNumber(int minValue, int maxValue)
        {
            RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();
            int result = 0;
            do
            {
                byte[] randombyte = new byte[1];
                rnd.GetBytes(randombyte); //получаем случайный байт 
                double random_multiplyer = ( randombyte[0] / 255d );
                int difference = maxValue - minValue + 1; //находим разницу между максимальным и минимальным значением 
                result = (int) ( minValue + Math.Floor(random_multiplyer * difference) );  //прибавляем к минимальному значение число от 0 до difference
            } while ( result > maxValue || result < minValue );
            return result;
        }
    }
}
