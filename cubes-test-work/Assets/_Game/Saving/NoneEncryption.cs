namespace Game
{
    public class NoneEncryption : Encryption
    {
        public override string Encrypt(string value)
        {
            return value;
        }

        public override string Decrypt(string value)
        {
            return value;
        }
    }
}
