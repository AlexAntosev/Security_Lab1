using NSec.Cryptography;

namespace Security_Lab5.Services
{
    public interface IKeyVault
    {
        byte[] Get();
    }
}