using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vault
{
    class Program
    {
        static void Main(string[] args)
        {
            string s_passPhrase = StringCipher.GeneratePassPhrase("PlayerData");

            string saveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "My Games", "Fallout Shelter");
            string filePath = Path.Combine(saveFolder, "Vault" + (1).ToString() + ".sav");
            string vault = File.ReadAllText(filePath);

            string decrypted = StringCipher.Decrypt(vault, s_passPhrase);
        }
    }
}
