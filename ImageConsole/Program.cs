#define DEBUG
#undef DEBUG
using System;
using System.Diagnostics;
using System.IO;
using DomainLayer;

namespace ImageConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageUtility imageUtility = new ImageUtility();
            ConsoleKey choice;
            do
            {
                Console.WriteLine(StringLiterals._imageMenu);
                choice = Console.ReadKey().Key;
                if(choice == ConsoleKey.NumPad1)
                {
                    string url = @"http://localhost:60561/api/upload";
                    #if (DEBUG)
                    string path = @"C:\Users\arun.munaganti\Downloads\veneno.jpg";
                    #else
                    Console.Write(StringLiterals._enterPath);
                    string path = Console.ReadLine();
                    #endif
                    if(!File.Exists(path))
                    {
                        Console.WriteLine(StringLiterals._fileNotExist);
                        continue;
                    }
                    try
                    {
                        string id = imageUtility.PostImage(path, url);
                        Console.WriteLine(StringLiterals._imageID + id);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (choice == ConsoleKey.NumPad2)
                {
                    string url = @"http://localhost:60561/api/download";
                    Console.Write(StringLiterals._enterID);
                    string id = Console.ReadLine();
                    #if (DEBUG)
                    string saveFolderPath = @"C:\Users\arun.munaganti\Desktop\";
                    #else
                    Console.Write(StringLiterals._folderPath);
                    string saveFolderPath = Console.ReadLine();
                    #endif
                    if(!Directory.Exists(saveFolderPath))
                    {
                        Console.WriteLine(StringLiterals._dirNotExist);
                        continue;
                    }
                    string path = imageUtility.GetImage(id, url, saveFolderPath);
                    if(string.IsNullOrEmpty(path))
                    {
                        Console.WriteLine(StringLiterals._invalidID);
                    }
                    else
                    {
                        Console.WriteLine(StringLiterals._imagePath + path);
                        Process.Start("CMD.exe", "/C explorer " + path); //opens image
                    }
                }
            } while (choice != ConsoleKey.NumPad3);
        }
    }
}
