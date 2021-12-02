using System;
using System.Security.Cryptography;
using System.Text;

namespace LojaVirtual.Libraries
{
    public class KeyGenerator
    {
        internal static readonly char[] chars =
           "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetUniqueKey(int tamanho)
        {
            byte[] data = new byte[4 * tamanho];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(tamanho);
            for (int i = 0; i < tamanho; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }
}
