using System;
using System.Text;

namespace Game
{
    public class UTF8Encryption : Encryption
    {
        public override string Decrypt(string encryptedJSONSave)
        {
            byte[] bytes = Convert.FromBase64String(encryptedJSONSave);
            string jsonSave = Encoding.UTF8.GetString(bytes);
            return jsonSave;
        }

        public override string Encrypt(string JSONSave)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JSONSave);
            string byteSave = Convert.ToBase64String(bytes);
            return byteSave;
        }
    }
}