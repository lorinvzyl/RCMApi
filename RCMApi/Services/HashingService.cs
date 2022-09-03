using Geralt;

namespace RCMAppApi.Services
{
    /*
     * This uses a nuget package called Geralt, for the use of argon2id from a library called libsodium.
     * This class will set the standards of what will be sent to functions of the nuget package.
     */
    public class HashingService
    {
        public HashingService()
        {
            //Readonly fields can only be assigned a value in the constructor of a class.
            //Assign values as needed here from the database or use a non-default constructor.
        }

        //will be used to pass hash into database.
        private readonly byte[]? hash;
        //needs to be span<byte>
        public Span<byte> Hash => hash;

        //database hash
        private readonly byte[]? _hash;
        //needs to be readonlyspan<byte>
        public ReadOnlySpan<byte> HashDb => _hash;

        //entered password
        private readonly byte[]? password;
        //needs to be readonlyspan<byte>
        public ReadOnlySpan<byte> Password => password;

        //these need to be tested to have the delay we want. Maximum of 1 second for online use.
        public readonly int iterations = 3;
        public readonly int memorySize = 67108864;

        //Will make necessary calls to the database to send data to the database
        //Needs to store in the database: hashed password, iterations and memorysize.
        public void HashPassword()
        {
            Argon2id.ComputeHash(Hash, Password, iterations, memorySize);
        }

        //Will make necessary calls to the database to verify password.
        //User's hashed password
        public void VerifyHash()
        {
            Argon2id.VerifyHash(HashDb, Password);
        }

        //Will make necessary calls to the database to check if the user's hashed password is outdated.
        //Needs the user's hashed password, iterations and memorysize.
        public void NeedsRehash()
        {
            Argon2id.NeedsRehash(HashDb, iterations, memorySize);
        }
    }
}
