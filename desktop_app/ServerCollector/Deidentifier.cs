using JhpDataSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncManager
{
    public static class Deidentifier
    {
        public static PlainText Decrypt(this EncryptedText encryptedText, 
            string encryptionKey)
        {
            var decryptedText = Crypto.Decrypt(encryptedText.Value, encryptionKey);
            return new PlainText(decryptedText);
        }
    }
}
