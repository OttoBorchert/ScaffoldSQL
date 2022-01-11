using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ScaffoldingSQLProject.Controllers
{

    /// <summary>
    ///     This file defines 
    /// </summary>
	public partial class FileController
	{
		/// <summary>
		///     Increment every time the version number is changed, for a maximum of 99. I guess this could be written in hex, though, so... 255? Maybe? But that seems excessive.
		/// </summary>
		const byte p_encryptionVersionNumber = 1;

		private static string VersioNumberToBase64() => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{p_encryptionVersionNumber:00}"));

		private static string VersionNumberFromBase64(string base64) => Encoding.UTF8.GetString(Convert.FromBase64String(base64[0..4]));

        public static string[] Encrypt(string[] wordsarray)
        {
            if (p_encryptionVersionNumber < 0 || p_encryptionVersionNumber > 99)
            {
                throw new InvalidDataException("Version number in compiled code is invalid. It must be constrained to [0, 99]");
            }

            const ushort
                blockSize = 128,
                keySize = 128;

            string[] result = new string[wordsarray.Length];
            byte[] encrypted;
            for (int i = 0; i < wordsarray.Length; ++i)
            {
                string word = wordsarray[i];
                // Clean up my grabage, c++++!
                using RijndaelManaged r = new RijndaelManaged();
                r.BlockSize = blockSize;
                r.KeySize = keySize;

                // Deallocate IDisposable
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, r.CreateEncryptor(r.Key, r.IV), CryptoStreamMode.Write))
                    {
                        // Write to the stream
                        using StreamWriter streamWriter = new StreamWriter(cryptoStream);
                        streamWriter.Write(word);
                    }
                    // Get the encrypted data
                    encrypted = memoryStream.ToArray();
                }
                // Convert to string for storage
                string encryptedString = Convert.ToBase64String(encrypted);
                string iv = Convert.ToBase64String(r.IV);
                string key = Convert.ToBase64String(r.Key);
                // Write the result to the array.
                result[i] = VersioNumberToBase64() + iv + key + encryptedString;
            }

            return result;
        }

        /// <summary>
        ///     Decrypts a list of strings. If it could not be decrypted, the original will be returned.
        ///     Note: The original array is modified in place.
        /// </summary>
        /// <param name="wordsarray"></param>
        /// <returns></returns>
		public static string[] Decrypt(string[] wordsarray)
        {
            // Set the length to be 16 bytes for both the block and the key
            const ushort
                blockSize = 128,
                keySize = 128;

            string[] result = new string[wordsarray.Length];
            for (int i = 0; i < wordsarray.Length; ++i)
            {
                string word = wordsarray[i];

                // If you look at the Base64 standard, it can generate any string that matches the regex pattern ([a-z]|[A-Z]|[0-9]|/|+)+
                // With a length that must be % 24 bits (or 4 bytes) (https://en.wikipedia.org/wiki/Base64). In the encryption stage,
                // We prepend the string with the version number, with 3 characters devoted to the version number, and 1 for padding so that we can test 
                // that the string is encrypted after the addition of the C# encrypttion layer. Note, the version number cannot be > 99 or less than 0.
                if (word.Length < 4 || word[3] != '=')
                {
                    result[i] = wordsarray[i];
                    continue;
                }

                // Check version number. 
                if (VersionNumberFromBase64(word) == "01")
                {
                    // Decrypt version 1
                    // Split the string into each components
                    string iv = word[4..28],
                        key = word[28..52],
                        decrypt = word[52..];

                    // Clean up my grabage, c++++!
                    using var r = new RijndaelManaged();
                    // Initialize data.
                    r.BlockSize = blockSize;
                    r.KeySize = keySize;
                    r.IV = Convert.FromBase64String(iv);
                    r.Key = Convert.FromBase64String(key);

                    // Clean up the objects that imp[lement IDisposable
                    using var memoryStream = new MemoryStream(Convert.FromBase64String(decrypt));
                    using var cryptoStream = new CryptoStream(memoryStream, r.CreateDecryptor(r.Key, r.IV), CryptoStreamMode.Read);
                    using var streamReader = new StreamReader(cryptoStream);
                    // Read the decrypted string
                    result[i] = streamReader.ReadToEnd();
                }
                else
                {
                    Console.Error.WriteLine($"WARNING: Invalid version number detected in string. Must be between 1 and {p_encryptionVersionNumber}. Skipping.");
                    result[i] = wordsarray[i];
                }
            }

            return result;
        }

    }
}
