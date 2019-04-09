using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using JenningsRE.ResultTransfer;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace JenningsRE
{
    internal class UserAccess
    {
        private static jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection();
        internal static ObjectResult VerifyUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return new InvalidResult("Error: Username/Password are required.");

            try
            {
                var result = from user in context.users
                             where user.username == username
                             select user;

                if(result.Count() == 0)
                {
                    return new NotFoundResult("Error: User does not exist.");
                }

                string passwordHash = result.First().passwordHash;
                bool? isAdmin = result.First().isAdmin;
            //START CODE BLOCK 
            //-- Code within used from 
            //https://github.com/mheyman/Isopoh.Cryptography.Argon2/blob/master/README.md 
                Argon2Config config = new Argon2Config
                {
                    Type = Argon2Type.DataIndependentAddressing,
                    Version = Argon2Version.Nineteen,
                    TimeCost = 3,
                    MemoryCost = 32768,
                    Lanes = 4,
                    Threads = Environment.ProcessorCount,
                    Password = Encoding.ASCII.GetBytes(password),
                    Salt = Convert.FromBase64String(Properties.Settings.Default.Salt),
                    HashLength = 20
                };
                SecureArray<byte> hashB = null;
                try
                {
                    if (config.DecodeString(passwordHash, out hashB) && hashB != null)
                    {
                        var argon2ToVerify = new Argon2(config);
                        using (var hashToVerify = argon2ToVerify.Hash())
                        {
                            if (!hashB.Buffer.Where((b, i) => b != hashToVerify[i]).Any())
                            {
                                return new OkResult(isAdmin.ToString());
                            }
                            else
                                return new WrongResult("Error: Username/Password is incorrect.");
                        }
                    }
                }
                catch(Exception ex)
                {
                    return new InvalidResult("Error: Failed to verify password.");
                }
                finally
                {
                    hashB?.Dispose();
                }
                //END CODE BLOCK
            }
            catch (Exception ex)
            {
                return new InvalidResult("Error: Failed retrieving data from the database.");
            }
            return new InvalidResult("Error: Failed to process verification.");
        }

        internal static void AddUser(string username, string password)
        {

            Argon2Config config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 3,
                MemoryCost = 32768,
                Lanes = 4,
                Threads = Environment.ProcessorCount,
                Password = Encoding.ASCII.GetBytes(password),
                Salt = Convert.FromBase64String(Properties.Settings.Default.Salt), // >= 8 bytes if not null
                HashLength = 20 // >= 4
            };
            Argon2 argon2 = new Argon2(config);
            string passwordHash;
            using (SecureArray<byte> hashA = argon2.Hash())
            {
                passwordHash = config.EncodeString(hashA.Buffer);
            }
            var userCreated = new user()
            {
                username = username,
                passwordHash = passwordHash,
                isAdmin = false,
                isEnabled = false
            };
            context.users.Add(userCreated);
            context.SaveChanges();
        }

        internal static void DeleteUser(user deleteUser)
        {
            var userToRemove = context.users.Find(deleteUser.id);
            context.users.Remove(userToRemove);
            context.SaveChanges();
        }

        internal static void UpdateUser(user updateUser)
        {
            var userToUpdate = context.users.Find(updateUser.id);
            updateUser.passwordHash = userToUpdate.passwordHash;
            context.Entry(userToUpdate).CurrentValues.SetValues(updateUser);
            context.SaveChanges();
        }

        internal static DbSet<user> GetUsers()
        {
            return context.Set<user>();
        }
    }
}