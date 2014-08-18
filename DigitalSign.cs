using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/////////////////////////////////////////
//////////// By Nickitee ////////////////
/////////////////////////////////////////


    class DigitalSign
    {
        public static RSACryptoServiceProvider rsa;


        public static void AssignNewKey(ref string privateKey, ref string publicKey) 
        {
            const int PROVIDER_RSA_FULL = 1;
            const string CONTAINER_NAME = "SpiderContainer";
            CspParameters cspParams;
            cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = CONTAINER_NAME;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
            rsa = new RSACryptoServiceProvider(2048, cspParams);
            string publicPrivateKeyXML = rsa.ToXmlString(true);
            privateKey = publicPrivateKeyXML;
            string publicOnlyKeyXML = rsa.ToXmlString(false);
            publicKey = publicOnlyKeyXML;
        }
        public static bool CompareRSAMethod(string textToSign, string gettedSign, string publicRsaKey)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(textToSign);
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.KeySize = 2048;
                RSA.FromXmlString(publicRsaKey);
                byte[] Signature = Convert.FromBase64String(gettedSign);
                return (RSA.VerifyData(buffer, "SHA1", Signature));
            }
            catch
            {
                return false;
            }
        }
        public static string Sign(string ptext, string decryptedXmlPkey)
        {
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            byte[] buffer = Encoding.ASCII.GetBytes(ptext);
            buffer = cryptoTransformSHA1.ComputeHash(buffer);
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(decryptedXmlPkey);
            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
            RSAFormatter.SetHashAlgorithm("SHA1");
            byte[] SignedHash = RSAFormatter.CreateSignature(buffer);
            return Convert.ToBase64String(SignedHash);
        }

        public static string XOR(string text, int key)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                int charValue = Convert.ToInt32(text[i]);
                charValue ^= key;
                SB.Append((char)(charValue));
            }
            return SB.ToString();
        }

        private static byte[] EncryptString(byte[] clearText, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearText, 0, clearText.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }
        public static string EncryptString(string clearText, string Password)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] encryptedData = EncryptString(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }
        private static byte[] DecryptString(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }
        public static string DecryptString(string cipherText, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] decryptedData = DecryptString(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }
    }

