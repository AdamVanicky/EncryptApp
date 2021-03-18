using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(@"Zašifrujte text {Pokud hledáte pomocnou ruku, najdete ji na konci své paže}
Blok ~ Bloková šifra XOR s klíčem PRAVDA
Caesar ~ Caesarova šifra s klíčem 7
Čím chcete šifrovat? ");
                string vyber = Console.ReadLine();

                string inputS = "Pokud hledáte pomocnou ruku, najdete ji na konci své paže";
                string XORKey = "PRAVDA";
                int CeaserMoving = 7;

                inputS = FixString(inputS);

                switch (vyber)
                {
                    case "Blok":
                        XORencrypt(inputS, XORKey);
                        break;
                    case "Caesar":
                        CaesarEncrypt(inputS, CeaserMoving);
                        break;
                }
                Console.WriteLine("Nashledanou!");
                Console.ReadLine();
            }
        }

        static void XORencrypt(string inputS, string encryptKey)
        {
            string result = "";

            for (int i = 0; i < inputS.Length; i++)
            {
                int keyPos = i % encryptKey.Length;

                int keyI = Convert.ToInt32(encryptKey[keyPos]);

                int inputI = Convert.ToInt32(inputS[i]);

                int combine = inputI ^ keyI;

                char combineChar = (char)combine;

                result += combineChar;
            }
            Console.WriteLine("Výsledkem šifrování je: " + result);

            Console.WriteLine("Chcete dešifrovat? ");
            string decrypt = Console.ReadLine();
            switch (decrypt)
            {
                case "Ano":
                    XORdecrypt(result, encryptKey);
                    break;
                case "Ne":
                    Console.WriteLine("Tak to je konec.");
                    break;
            }
        }
        static void CaesarEncrypt(string inputS, int positionsMoved)
        {
            string output = "";
            int zInt = Convert.ToInt32('z');
            int aInt = Convert.ToInt32('a');
            foreach(char z in inputS)
            {
                if(z == ' ' || z == ',' || z == '.' || z == '?' || z == '!')
                {
                    continue;
                }
                else
                {
                    int zi = Convert.ToInt32(z);
                    zi += positionsMoved;
                    if(zi - zInt <= 0)
                    { output += (char)zi; }
                    else
                    {
                        int mezivypocet = zi - zInt;
                        output += (char)(aInt - 1 + mezivypocet);
                    }
                    
                }
            }
            Console.WriteLine($@"String {inputS} byl zašifrován Ceasarovou šifrou z klíčem {positionsMoved}.
Výsledkem šifry je {output}");

            Console.WriteLine("Chcete dešifrovat? ");
            string decrypt = Console.ReadLine();
            switch (decrypt)
            {
                case "Ano":
                    CaesarDecrypt(output, positionsMoved);
                    break;
                case "Ne":
                    Console.WriteLine("Tak to je konec.");
                    break;
            }
        }

        static string FixString(string inputS)
        {
            if (inputS.Contains("á"))
                inputS = inputS.Replace('á', 'a');
            if (inputS.Contains("ě"))
                inputS = inputS.Replace('ě', 'e');
            if (inputS.Contains("š"))
                inputS = inputS.Replace('š', 's');
            if (inputS.Contains("č"))
                inputS = inputS.Replace('č', 'c');
            if (inputS.Contains("ř"))
                inputS = inputS.Replace('ř', 'r');
            if (inputS.Contains("ž"))
                inputS = inputS.Replace('ž', 'z');
            if (inputS.Contains("ý"))
                inputS = inputS.Replace('ý', 'y');
            if (inputS.Contains("í"))
                inputS = inputS.Replace('í', 'i');
            if (inputS.Contains("é"))
                inputS = inputS.Replace('é', 'e');

            return new string(inputS.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        static void XORdecrypt (string encryptedText, string key)
        {
            string decryptoresult = "";

            for (int i = 0; i < encryptedText.Length; i++)
            {
                int keyPos = i % key.Length;

                int keyI = Convert.ToInt32(key[keyPos]);

                int inputI = Convert.ToInt32(encryptedText[i]);

                int combine = inputI ^ keyI;

                char combineChar = (char)combine;

                decryptoresult += combineChar;
            }

            Console.WriteLine("Výsledkem dešifrování je: " + decryptoresult);
        }

        static void CaesarDecrypt(string encryptedText, int movePositions)
        {
            int positon = 0;
            string output = "";
            int zInt = Convert.ToInt32('z');
            int aInt = Convert.ToInt32('a');
            foreach (char z in encryptedText)
            {
                if (z == ' ' || z == ',' || z == '.' || z == '?' || z == '!')
                {
                    continue;
                }
                else
                {
                    int zi = Convert.ToInt32(z);
                    zi -= movePositions;
                    if (zi >= aInt && zi <= zInt)
                    { output += (char)zi; }
                    else if (positon == 0) { output += (char)zi; }
                    else
                    {
                        int mezivypocet = aInt - zi;
                        output += (char)(zInt + 1 - mezivypocet);
                    }
                }
                positon++;
            }
            Console.WriteLine("Výsledkem dešifrování je: " + output);
        }
    }
}
