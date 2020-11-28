using System;

namespace Security_Lab1.Encoders
{
    public static class XorEncoder
    {
        public static string Encrypt(string text, char key) 
        {
            var encryptedText = ""; 
            var textLength = text.Length; 
  
            for (var i = 0; i < textLength; i++)  
            { 
                encryptedText += char.ToString((char) (text[i] ^ key)); 
            } 
  
            return encryptedText; 
        } 
        
        public static string RepeatingKeyEncrypt(string text, string key) 
        {
            var encryptedText = ""; 
            var textLength = text.Length; 
  
            var k = 0;
            for (var i = 0; i < textLength; i++)
            {
                encryptedText += Convert.ToByte((char) (text[i] ^ key[k]));
                
                k++;
                if (k == key.Length)
                {
                    k = 0;
                }
            } 
  
            return encryptedText; 
        } 
    }
}