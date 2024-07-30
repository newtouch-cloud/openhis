using NewtouchHIS.Lib.Base.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Newtouch.Core.Common.Security
{
    /// <summary>
    /// Rijndael加密 解密 实例类
    /// </summary>
    public class RijndaelEnhanced
    {
        #region Private members
        // If hashing algorithm is not specified, use SHA-1.
        private static string DEFAULT_HASH_ALGORITHM = "SHA1";

        // If key size is not specified, use the longest 256-bit key.
        private static int DEFAULT_KEY_SIZE = 128;

        // Do not allow salt to be longer than 255 bytes, because we have only
        // 1 byte to store its length. 
        private static int MAX_ALLOWED_SALT_LEN = 255;

        // Do not allow salt to be smaller than 4 bytes, because we use the first
        // 4 bytes of salt to store its length. 
        private static int MIN_ALLOWED_SALT_LEN = 4;

        // Random salt value will be between 4 and 8 bytes long.
        private static int DEFAULT_MIN_SALT_LEN = MIN_ALLOWED_SALT_LEN;
        private static int DEFAULT_MAX_SALT_LEN = 8;

        // Use these members to save min and max salt lengths.
        private int minSaltLen = -1;
        private int maxSaltLen = -1;

        // These members will be used to perform encryption and decryption.
        private ICryptoTransform? encryptor = null;
        private ICryptoTransform? decryptor = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with 256-bit key, derived using 1 password iteration,
        /// hashing without salt, no initialization vector, electronic codebook
        /// (ECB) mode, SHA-1 hashing algorithm, and 4-to-8 byte long salt.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <remarks>
        /// This constructor is not recommended because it does not use
        /// initialization vector and uses the ECB cipher mode, which is less
        /// secure than the CBC mode.
        /// </remarks>
        public RijndaelEnhanced(string passPhrase) :
            this(passPhrase, null)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with 256-bit key, derived using 1 password iteration,
        /// hashing without salt, cipher block chaining (CBC) mode, SHA-1
        /// hashing algorithm, and 4-to-8 byte long salt.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        public RijndaelEnhanced(string passPhrase,
                                string initVector) :
            this(passPhrase, initVector, -1)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with 256-bit key, derived using 1 password iteration,
        /// hashing without salt, cipher block chaining (CBC) mode, SHA-1 
        /// hashing algorithm, and 0-to-8 byte long salt.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        public RijndaelEnhanced(string passPhrase,
                                string initVector,
                                int minSaltLen) :
            this(passPhrase, initVector, minSaltLen, -1)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with 256-bit key, derived using 1 password iteration,
        /// hashing without salt, cipher block chaining (CBC) mode, SHA-1
        /// hashing algorithm. Use the minSaltLen and maxSaltLen parameters to
        /// specify the size of randomly generated salt.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        public RijndaelEnhanced(string passPhrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen) :
            this(passPhrase, initVector, minSaltLen, maxSaltLen, -1)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption using the key derived from 1 password iteration,
        /// hashing without salt, cipher block chaining (CBC) mode, and
        /// SHA-1 hashing algorithm.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be 
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        /// <param name="keySize">
        /// Size of symmetric key (in bits): 128, 192, or 256.
        /// </param>
        public RijndaelEnhanced(string passPhrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen,
                                int keySize) :
            this(passPhrase, initVector, minSaltLen, maxSaltLen, keySize, null)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption using the key derived from 1 password iteration, hashing 
        /// without salt, and cipher block chaining (CBC) mode.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        /// <param name="keySize">
        /// Size of symmetric key (in bits): 128, 192, or 256.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hashing algorithm: "MD5" or "SHA1". SHA1 is recommended.
        /// </param>
        public RijndaelEnhanced(string passPhrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen,
                                int keySize,
                                string hashAlgorithm) :
            this(passPhrase, initVector, minSaltLen, maxSaltLen, keySize,
                hashAlgorithm, null)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption using the key derived from 1 password iteration, and
        /// cipher block chaining (CBC) mode.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        /// <param name="keySize">
        /// Size of symmetric key (in bits): 128, 192, or 256.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hashing algorithm: "MD5" or "SHA1". SHA1 is recommended.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used for password hashing during key generation. This is
        /// not the same as the salt we will use during encryption. This parameter
        /// can be any string.
        /// </param>
        public RijndaelEnhanced(string passPhrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen,
                                int keySize,
                                string hashAlgorithm,
                                string saltValue) :
            this(passPhrase, initVector, minSaltLen, maxSaltLen, keySize,
                hashAlgorithm, saltValue, 1)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with the key derived from the explicitly specified
        /// parameters.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        /// <param name="keySize">
        /// Size of symmetric key (in bits): 128, 192, or 256.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hashing algorithm: "MD5" or "SHA1". SHA1 is recommended.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used for password hashing during key generation. This is
        /// not the same as the salt we will use during encryption. This parameter
        /// can be any string.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to hash password. More iterations are
        /// considered more secure but may take longer.
        /// </param>
        public RijndaelEnhanced(string passPhrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen,
                                int keySize,
                                string hashAlgorithm,
                                string saltValue,
                                int passwordIterations)
        {
            // Save min salt length; set it to default if invalid value is passed.
            if (minSaltLen < MIN_ALLOWED_SALT_LEN)
                this.minSaltLen = DEFAULT_MIN_SALT_LEN;
            else
                this.minSaltLen = minSaltLen;

            // Save max salt length; set it to default if invalid value is passed.
            if (maxSaltLen < 0 || maxSaltLen > MAX_ALLOWED_SALT_LEN)
                this.maxSaltLen = DEFAULT_MAX_SALT_LEN;
            else
                this.maxSaltLen = maxSaltLen;

            // Set the size of cryptographic key.
            if (keySize <= 0)
                keySize = DEFAULT_KEY_SIZE;

            // Set the name of algorithm. Make sure it is in UPPER CASE and does
            // not use dashes, e.g. change "sha-1" to "SHA1".
            if (hashAlgorithm == null)
                hashAlgorithm = DEFAULT_HASH_ALGORITHM;
            else
                hashAlgorithm = hashAlgorithm.ToUpper().Replace("-", "");

            // Initialization vector converted to a byte array.
            byte[] initVectorBytes = null;

            // Salt used for password hashing (to generate the key, not during
            // encryption) converted to a byte array.
            byte[] saltValueBytes = null;

            // Get bytes of initialization vector.
            if (initVector == null)
                initVectorBytes = new byte[0];
            else
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            // Get bytes of salt (used in hashing).
            if (saltValue == null)
                saltValueBytes = new byte[0];
            else
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Generate password, which will be used to derive the key.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                       passPhrase,
                                                       saltValueBytes,
                                                       hashAlgorithm,
                                                       passwordIterations);

            // Convert key to a byte array adjusting the size from bits to bytes.
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Initialize Rijndael key object.
            var symmetricKey = Rijndael.Create();

            // If we do not have initialization vector, we cannot use the CBC mode.
            // The only alternative is the ECB mode (which is not as good).
            if (initVectorBytes.Length == 0)
                symmetricKey.Mode = CipherMode.ECB;
            else
                symmetricKey.Mode = CipherMode.CBC;

            // Create encryptor and decryptor, which we will use for cryptographic
            // operations.
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        }
        #endregion

        #region Encryption routines
        /// <summary>
        /// Encrypts a string value generating a base64-encoded string.
        /// </summary>
        /// <param name="plainText">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(string plainText)
        {
            return Encrypt(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts a byte array generating a base64-encoded string.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(byte[] plainTextBytes)
        {
            return Convert.ToBase64String(EncryptToBytes(plainTextBytes));
        }

        /// <summary>
        /// Encrypts a string value generating a byte array of cipher text.
        /// </summary>
        /// <param name="plainText">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(string plainText)
        {
            return EncryptToBytes(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts a byte array generating a byte array of cipher text.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(byte[] plainTextBytes)
        {
            // Add salt at the beginning of the plain text bytes (if needed).
            byte[] plainTextBytesWithSalt = AddSalt(plainTextBytes);

            // Encryption will be performed using memory stream.
            MemoryStream memoryStream = new MemoryStream();

            // Let's make cryptographic operations thread-safe.
            lock (this)
            {
                // To perform encryption, we must use the Write mode.
                CryptoStream cryptoStream = new CryptoStream(
                                                   memoryStream,
                                                   encryptor,
                                                    CryptoStreamMode.Write);

                // Start encrypting data.
                cryptoStream.Write(plainTextBytesWithSalt,
                                    0,
                                   plainTextBytesWithSalt.Length);

                // Finish the encryption operation.
                cryptoStream.FlushFinalBlock();

                // Move encrypted data from memory into a byte array.
                byte[] cipherTextBytes = memoryStream.ToArray();

                // Close memory streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Return encrypted data.
                return cipherTextBytes;
            }
        }
        #endregion

        #region Decryption routines
        /// <summary>
        /// Decrypts a base64-encoded cipher text value generating a string result.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-encoded cipher text string to be decrypted.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(string cipherText)
        {
            return Decrypt(Convert.FromBase64String(cipherText));
        }

        /// <summary>
        /// Decrypts a byte array containing cipher text value and generates a
        /// string result.
        /// </summary>
        /// <param name="cipherTextBytes">
        /// Byte array containing encrypted data.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(byte[] cipherTextBytes)
        {
            return Encoding.UTF8.GetString(DecryptToBytes(cipherTextBytes));
        }

        /// <summary>
        /// Decrypts a base64-encoded cipher text value and generates a byte array
        /// of plain text data.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-encoded cipher text string to be decrypted.
        /// </param>
        /// <returns>
        /// Byte array containing decrypted value.
        /// </returns>
        public byte[] DecryptToBytes(string cipherText)
        {
            return DecryptToBytes(Convert.FromBase64String(cipherText));
        }

        /// <summary>
        /// Decrypts a base64-encoded cipher text value and generates a byte array
        /// of plain text data.
        /// </summary>
        /// <param name="cipherTextBytes">
        /// Byte array containing encrypted data.
        /// </param>
        /// <returns>
        /// Byte array containing decrypted value.
        /// </returns>
        public byte[] DecryptToBytes(byte[] cipherTextBytes)
        {
            byte[] decryptedBytes = null;
            byte[] plainTextBytes = null;
            int decryptedByteCount = 0;
            int saltLen = 0;

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Since we do not know how big decrypted value will be, use the same
            // size as cipher text. Cipher text is always longer than plain text
            // (in block cipher encryption), so we will just use the number of
            // decrypted data byte after we know how big it is.
            decryptedBytes = new byte[cipherTextBytes.Length];

            // Let's make cryptographic operations thread-safe.
            lock (this)
            {
                // To perform decryption, we must use the Read mode.
                CryptoStream cryptoStream = new CryptoStream(
                                                   memoryStream,
                                                   decryptor,
                                                   CryptoStreamMode.Read);

                // Decrypting data and get the count of plain text bytes.
                decryptedByteCount = cryptoStream.Read(decryptedBytes,
                                                        0,
                                                        decryptedBytes.Length);
                // Release memory.
                memoryStream.Close();
                cryptoStream.Close();
            }

            // If we are using salt, get its length from the first 4 bytes of plain
            // text data.
            if (maxSaltLen > 0 && maxSaltLen >= minSaltLen)
            {
                saltLen = (decryptedBytes[0] & 0x03) |
                            (decryptedBytes[1] & 0x0c) |
                            (decryptedBytes[2] & 0x30) |
                            (decryptedBytes[3] & 0xc0);
            }

            // Allocate the byte array to hold the original plain text (without salt).
            plainTextBytes = new byte[decryptedByteCount - saltLen];

            // Copy original plain text discarding the salt value if needed.
            Array.Copy(decryptedBytes, saltLen, plainTextBytes,
                        0, decryptedByteCount - saltLen);

            // Return original plain text value.
            return plainTextBytes;
        }
        #endregion

        #region Helper functions
        /// <summary>
        /// Adds an array of randomly generated bytes at the beginning of the
        /// array holding original plain text value.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Byte array containing original plain text value.
        /// </param>
        /// <returns>
        /// Either original array of plain text bytes (if salt is not used) or a
        /// modified array containing a randomly generated salt added at the 
        /// beginning of the plain text bytes. 
        /// </returns>
        private byte[] AddSalt(byte[] plainTextBytes)
        {
            // The max salt value of 0 (zero) indicates that we should not use 
            // salt. Also do not use salt if the max salt value is smaller than
            // the min value.
            if (maxSaltLen == 0 || maxSaltLen < minSaltLen)
                return plainTextBytes;

            // Generate the salt.
            byte[] saltBytes = GenerateSalt();

            // Allocate array which will hold salt and plain text bytes.
            byte[] plainTextBytesWithSalt = new byte[plainTextBytes.Length +
                                                     saltBytes.Length];
            // First, copy salt bytes.
            Array.Copy(saltBytes, plainTextBytesWithSalt, saltBytes.Length);

            // Append plain text bytes to the salt value.
            Array.Copy(plainTextBytes, 0,
                        plainTextBytesWithSalt, saltBytes.Length,
                        plainTextBytes.Length);

            return plainTextBytesWithSalt;
        }

        /// <summary>
        /// Generates an array holding cryptographically strong bytes.
        /// </summary>
        /// <returns>
        /// Array of randomly generated bytes.
        /// </returns>
        /// <remarks>
        /// Salt size will be defined at random or exactly as specified by the
        /// minSlatLen and maxSaltLen parameters passed to the object constructor.
        /// The first four bytes of the salt array will contain the salt length
        /// split into four two-bit pieces.
        /// </remarks>
        private byte[] GenerateSalt()
        {
            // We don't have the length, yet.
            int saltLen = 0;

            // If min and max salt values are the same, it should not be random.
            if (minSaltLen == maxSaltLen)
                saltLen = minSaltLen;
            // Use random number generator to calculate salt length.
            else
                saltLen = GenerateRandomNumber(minSaltLen, maxSaltLen);

            // Allocate byte array to hold our salt.
            byte[] salt = new byte[saltLen];

            // Populate salt with cryptographically strong bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(salt);

            // Split salt length (always one byte) into four two-bit pieces and
            // store these pieces in the first four bytes of the salt array.
            salt[0] = (byte)((salt[0] & 0xfc) | (saltLen & 0x03));
            salt[1] = (byte)((salt[1] & 0xf3) | (saltLen & 0x0c));
            salt[2] = (byte)((salt[2] & 0xcf) | (saltLen & 0x30));
            salt[3] = (byte)((salt[3] & 0x3f) | (saltLen & 0xc0));

            return salt;
        }

        /// <summary>
        /// Generates random integer.
        /// </summary>
        /// <param name="minValue">
        /// Min value (inclusive).
        /// </param>
        /// <param name="maxValue">
        /// Max value (inclusive).
        /// </param>
        /// <returns>
        /// Random integer value between the min and max values (inclusive).
        /// </returns>
        /// <remarks>
        /// This methods overcomes the limitations of .NET Framework's Random
        /// class, which - when initialized multiple times within a very short
        /// period of time - can generate the same "random" number.
        /// </remarks>
        private int GenerateRandomNumber(int minValue, int maxValue)
        {
            // We will make up an integer seed from 4 bytes of this array.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert four random bytes into a positive integer value.
            int seed = ((randomBytes[0] & 0x7f) << 24) |
                        (randomBytes[1] << 16) |
                        (randomBytes[2] << 8) |
                        (randomBytes[3]);

            // Now, this looks more like real randomization.
            Random random = new Random(seed);

            // Calculate a random number.
            return random.Next(minValue, maxValue + 1);
        }
        #endregion
    }

    /// <summary>
    /// 静态扩展
    /// </summary>
    public static class RijndaelEnhancedExt
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText">密文</param>
        /// <param name="rigndae">对象实例</param>
        /// <returns></returns>
        public static string Decrypt(this string cipherText, RijndaelEnhanced rigndae)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                {
                    return string.Empty;
                }
                return rigndae.Decrypt(cipherText);
            }
            catch
            {
                throw new RijndaelDecrptyException();
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="rigndae">对象实例</param>
        /// <returns></returns>
        public static string Encrypt(this string plainText, RijndaelEnhanced rigndae)
        {
            try
            {
                //if (string.IsNullOrEmpty(plainText))
                //{
                //    return string.Empty;
                //}
                if (plainText == null)
                {
                    return null;
                }
                return rigndae.Encrypt(plainText);
            }
            catch
            {
                throw new RijndaelEncryptException();
            }
        }

    }

}