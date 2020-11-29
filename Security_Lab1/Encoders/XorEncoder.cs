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
        
        public static string Decrypt(string text, char key) => Encrypt(text, key);
    }
}