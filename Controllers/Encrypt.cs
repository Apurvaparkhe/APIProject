using System.Text;

namespace MyNamespace
{
    public class PasswordEncryptor
    {
        public string EncryptPassword(string password)
        {
             byte[] storePassword= ASCIIEncoding.ASCII.GetBytes(password);
            string encryptedpassword= Convert.ToBase64String(storePassword);
            return encryptedpassword;
        }

        public string DecryptPassword(string password)
        {
             byte[] encryptedPassword= Convert.FromBase64String(password);
            string decryptedpassword= ASCIIEncoding.ASCII.GetString(encryptedPassword);
            return decryptedpassword;
        }
       
    }
}

// To use the EncryptPassword method, create an instance of PasswordEncryptor and call the method:
//var encryptor = new MyNamespace.PasswordEncryptor();
//var encryptedPassword = encryptor.EncryptPassword("my_password");
