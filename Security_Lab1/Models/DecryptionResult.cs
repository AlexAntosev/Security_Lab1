namespace Security_Lab1.Models
{
    public class DecryptionResult
    {
        public int MatchesCount { get; set; }
        
        public string DecryptedText { get; set; }
        
        public char Key { get; set; }

        public override string ToString()
        {
            return $"Decrypted text: {DecryptedText}; Key: {Key}; Matches count: {MatchesCount}";
        }
    }
}