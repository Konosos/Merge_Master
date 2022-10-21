using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public static class GameUtility
    {
        public static string SimpleMoneyText(int _money)
        {
            int million = _money / 10000000;
            if (million > 0)
                return (_money / 1000000).ToString() + "M";
            int thousand = _money / 10000;
            if (thousand > 0)
                return (_money / 1000).ToString() + "K";
            return _money.ToString();
        }

        public static string RandomString(int lenght)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string generated_string = "";

            for (int i = 0; i < lenght; i++)
                generated_string += characters[Random.Range(0, characters.Length)];

            return generated_string;
        }
    }

    
}