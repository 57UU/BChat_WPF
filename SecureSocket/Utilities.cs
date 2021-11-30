using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace SecureSocket
{
    class Utilities
    {
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string publickey, string content)
        {
           // publickey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            
            byte[] cipherbytes;
            rsa.FromXmlString(ToXmlPublicKey(publickey));
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// base64 public key string -> xml public key
        /// </summary>
        /// <param name="pubilcKey"></param>
        /// <returns></returns>
        private static string ToXmlPublicKey(string pubilcKey)
        {
            RsaKeyParameters p =
                PublicKeyFactory.CreateKey(Convert.FromBase64String(pubilcKey)) as RsaKeyParameters;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                RSAParameters rsaParams = new RSAParameters
                {
                    Modulus = p.Modulus.ToByteArrayUnsigned(),
                    Exponent = p.Exponent.ToByteArrayUnsigned()
                };
                rsa.ImportParameters(rsaParams);
                return rsa.ToXmlString(false);
            }
        }


        public static String getAESKey(int length)
        {
            SecureRandom random = new SecureRandom();

            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                bool isChar = (random.Next(2) % 2 == 0);// 输出字母还是数字
                if (isChar)
                { // 字符串
                    int choice = random.Next(2) % 2 == 0 ? 65 : 97; // 取得大写字母还是小写字母
                    ret.Append((char)(choice + random.Next(26)));
                }
                else
                { // 数字
                    ret.Append((random.Next(10)).ToString());
                }
            }
            return ret.ToString();
        }



        #region 新订购接口加解密
        /// <summary>
        /// AES加密 对应java中的 aes/cbc/pkcs5padding 模式的算法
        /// </summary>
        /// <param name="s">待加密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string str, string Key)
        {
            str = ConvertUtils.stringToHexString(str);
            string IV = Key;
            byte[] keyArray = Encoding.UTF8.GetBytes(Key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.IV = Encoding.UTF8.GetBytes(IV);
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密  对应java中的 aes/cbc/pkcs5padding 模式的算法
        /// </summary>
        /// <param name="s">待解密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>返回空为解析失败</returns>
        public static string AESDecrypt(string str, string key)
        {
            string IV = key;
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(str);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = Encoding.UTF8.GetBytes(IV);
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return ConvertUtils.hexStringToString(Encoding.UTF8.GetString(plainText));
        }
        #endregion

    }
     static class ConvertUtils
    {

        public static String toHex(byte[] input)
        {
            if (input == null)
                return null;
            StringBuilder output = new StringBuilder(input.Length * 2);
            for (int i = 0; i < input.Length; i++)
            {
                int current = input[i] & 0xff;
                if (current < 16)
                    output.Append("0");
                output.Append(Convert.ToString(current, 16));
            }

            return output.ToString();
        }

        public static byte[] fromHex(String input)
        {
            if (input == null)
                return null;
            byte[] output = new byte[input.Length/ 2];
            for (int i = 0; i < output.Length; i++)
            {
                string hexnum = input.Substring(i * 2, 2);
                //Console.WriteLine(hexnum);
                output[i] = (byte)Int32.Parse(hexnum, System.Globalization.NumberStyles.HexNumber);
            }
            return output;
        }

        public static String stringToHexString(String input)
        {
            return input != null ? toHex(Encoding.UTF8.GetBytes(input)) : null;
        }


        public static String hexStringToString(String input)
        {

                return input != null ? Encoding.UTF8.GetString(fromHex(input)) : null;
            }

    }
}
