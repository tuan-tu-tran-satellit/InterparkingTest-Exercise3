using InterParkingTestFileReadinLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ctrl + C to quit");
            while(true)
            {
                try
                {
                    ReadingOptions options = AskReadingOptions();
                    IFileReader reader = BuildReader(options);
                    bool keepGoing = true;
                    while (keepGoing)
                    {
                        keepGoing = PickFileToRead(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something when terribly wrong: " + ex);
                }
                Console.WriteLine();
                Console.WriteLine("Select new reading options. (press ctrl+C to quit)");
            }
        }
        private static ReadingOptions AskReadingOptions()
        {
            Console.WriteLine("Choose your file type:");
            Console.WriteLine("1: Text");
            Console.WriteLine("2: Xml");
            Console.WriteLine("3: Json");
            Console.Write("Enter type number: ");
            int fileType = Convert.ToInt32(Console.ReadLine().Trim());

            Console.WriteLine();
            Console.Write("Do you want to use the decryption system? (yY/nN) ");
            bool decrypt = ReadBoolean();

            Console.WriteLine();
            Console.Write("Do you want to enforce access security ? (yY/nN) ");
            bool accessControl = ReadBoolean();
            bool? isAdmin = null;
            if (accessControl)
            {
                Console.WriteLine();
                Console.Write("Are you an admin? Admins can read all files, non-admins can only read .txt and .txt.encrypted files (yY/nN) ");
                isAdmin = ReadBoolean();
            }

            var options = new ReadingOptions()
            {
                FileType = (FileType)fileType,
                Decrypt = decrypt,
                IsAdmin = isAdmin,
            };
            return options;
        }
        private static bool ReadBoolean()
        {
            bool yesNo = Console.ReadLine().Trim().ToLower() switch
            {
                "y" => true,
                "n" => false,
                _ => throw new ArgumentException("please play along and type y or Y or n or N"),
            };
            return yesNo;
        }
        private static IFileReader BuildReader(ReadingOptions options)
        {
            IFileReader reader = new FileReader();
            if (options.Decrypt)
            {
                reader = new EncryptedFileReader();
            }

            reader = options.FileType switch
            {
                FileType.Json => new JsonFileReader(reader),
                FileType.Xml => new XmlContentReader(reader),
                _ => reader,
            };

            if (options.IsAdmin.HasValue)
            {
                reader = new RestrictedFileReader(new RoleBasedAccessControl(options.IsAdmin.Value), reader);
            }

            return reader;
        }

        private static bool PickFileToRead(IFileReader reader)
        {
            Console.WriteLine();
            Console.WriteLine("Choose the file to read:");
            var files = new List<string>
            {
                "file.txt",
                "file.xml",
                "file.json",
                "file.txt.encrypted",
                "file.xml.encrypted",
                "file.json.encrypted",
            };
            for (int i = 0; i < files.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i, files[i]);
            }
            Console.Write("Enter the file number or empty to stop and choose other reading options: ");
            string input = Console.ReadLine().Trim();
            
            if (String.IsNullOrWhiteSpace(input))
                return false;

            var index = Convert.ToInt32(input);
            var fname = Path.Combine("samples", files[index]);
            Console.WriteLine("Reading content of " + fname);
            try
            {
                string content = reader.ReadFileContent(fname);
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex);
            }

            return true;
        }
    }
    public enum FileType { Text = 1, Xml = 2, Json = 3 }
}
