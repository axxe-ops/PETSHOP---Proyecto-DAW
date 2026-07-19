using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SEGURIDAD
{
    public static class ENCRIPTADO
    {
        public static string Hashear(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] entradaBytes = Encoding.UTF8.GetBytes(contraseña);
                byte[] hashBytes = sha256.ComputeHash(entradaBytes);


                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}
