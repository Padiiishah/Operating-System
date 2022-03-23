using System;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Text.Json;
using System.Xml.Serialization;

namespace os
{
    [Serializable]
    public class Car
    {
        public string brand;
        public string color;
        public string model;
        public string productionYear;
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            bool humus = true;
            while (humus)
            {
                Console.WriteLine("Выберите функцию программы: ");
                Console.WriteLine("Выйти из программы - 0");
                Console.WriteLine("Вывести информацию в консоль о логических дисках, именах, метке тома, размере и типе файловой системы - 1");
                Console.WriteLine("Работа с файлами - 2");
                Console.WriteLine("Работа с форматом JSON - 3");
                Console.WriteLine("Работа с форматом XML - 4");
                Console.WriteLine("Работа с zip архивами - 5");
                int vbr = Convert.ToInt32(Console.ReadLine());

                switch (vbr)
                {
                    case 0:
                        {
                            humus = false;
                            break;
                        }
                    case 1:
                        {
                            DriveInfo[] drives = DriveInfo.GetDrives();

                            foreach (DriveInfo drive in drives)
                            {
                                Console.WriteLine($"Название: {drive.Name}");
                                Console.WriteLine($"Тип: {drive.DriveType}");
                                if (drive.IsReady)
                                {
                                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                                    Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                    case 2:
                        {
                            string path = "A://test/";
                            Console.WriteLine("Выберите функцию: ");
                            Console.WriteLine("Выйти в главное меню - 0");
                            Console.WriteLine("Создать файл - 1");
                            Console.WriteLine("Записать в файл строку, введённую пользователем - 2");
                            Console.WriteLine("Прочитать файл в консоль - 3");
                            Console.WriteLine("Удалить файл - 4");
                            int vbr1 = Convert.ToInt32(Console.ReadLine());
                            switch (vbr1)
                            {
                                case 0:
                                    {
                                        break;
                                    }
                                case 1:
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                                        if (!dirInfo.Exists)
                                        {
                                            dirInfo.Create();
                                        }
                                        using (FileStream fstream = new FileStream($"{path}\\note.txt", FileMode.Create))
                                        {
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                                        if (!dirInfo.Exists)
                                        {
                                            dirInfo.Create();
                                        }
                                        Console.WriteLine("Введите строку для записи в файл: ");
                                        string text = Console.ReadLine();


                                        using (FileStream fstream = new FileStream($"{path}\\note.txt", FileMode.OpenOrCreate))
                                        {
                                            byte[] array = System.Text.Encoding.Default.GetBytes(text);

                                            await fstream.WriteAsync(array, 0, array.Length);
                                            Console.WriteLine("Текст записан в файл");
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        FileInfo fInfo = new FileInfo($"{path}\\note.txt");
                                        if (fInfo.Exists)
                                        {
                                            using (FileStream fstream = File.OpenRead($"{path}\\note.txt"))
                                            {
                                                byte[] array = new byte[fstream.Length];

                                                await fstream.ReadAsync(array, 0, array.Length);

                                                string textFromFile = System.Text.Encoding.Default.GetString(array);
                                                Console.WriteLine($"Текст из файла : {textFromFile}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        FileInfo file1Info = new FileInfo($"{path}\\note.txt");
                                        if (file1Info.Exists)
                                        {
                                            FileInfo fileInf = new FileInfo($"{path}\\note.txt");
                                            if (fileInf.Exists)
                                            {
                                                fileInf.Delete();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }

                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Выберите функцию: ");
                            Console.WriteLine("Выйти в главное меню - 0");
                            Console.WriteLine("Создать новый объект. Выполнить сериализацию объекта в формате JSON и записать в файл. - 1");
                            Console.WriteLine("Прочитать файл в консоль - 2");
                            Console.WriteLine("Удалить файл - 3");
                            int vbr2 = Convert.ToInt32(Console.ReadLine());
                            switch (vbr2)
                            {
                                case 0:
                                    {
                                        break;
                                    }
                                case 1:
                                    {
                                        using (FileStream fs = new FileStream("A://test/user.json", FileMode.OpenOrCreate))
                                        {
                                            Console.WriteLine("Введите имя: ");
                                            string name = Console.ReadLine();
                                            Console.WriteLine("Введите возраст: ");
                                            int age = Convert.ToInt32(Console.ReadLine());
                                            Person tom = new Person() { Name = name, Age = age };
                                            await JsonSerializer.SerializeAsync<Person>(fs, tom);
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        FileInfo file1Info = new FileInfo("A://test/user.json");
                                        if (file1Info.Exists)
                                        {
                                            using (FileStream fs = new FileStream("A://test/user.json", FileMode.OpenOrCreate))
                                            {
                                                Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                                                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        FileInfo file1Info = new FileInfo("A://test/user.json");
                                        if (file1Info.Exists)
                                        {
                                            FileInfo fileInf = new FileInfo("A://test/user.json");
                                            if (fileInf.Exists)
                                            {
                                                fileInf.Delete();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Выберите функцию: ");
                            Console.WriteLine("Выйти в главное меню - 0");
                            Console.WriteLine("Создать файл формате XML из редактора - 1");
                            Console.WriteLine("Записать в файл новые данные из консоли - 2");
                            Console.WriteLine("Прочитать файл в консоль - 3");
                            Console.WriteLine("Удалить файл - 4");
                            int vbr3 = Convert.ToInt32(Console.ReadLine());
                            switch (vbr3)
                            {
                                case 0:
                                    {
                                        break;
                                    }
                                case 1:
                                    {
                                        using (FileStream fstream = new FileStream("A://test/car.xml", FileMode.Create))
                                        {
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        Car auto = new Car();
                                        Console.WriteLine("Введите данные автомобиля");
                                        Console.Write("Марка: ");
                                        auto.brand = Console.ReadLine();
                                        Console.Write("Модель: ");
                                        auto.model = Console.ReadLine();
                                        Console.Write("Цвет: ");
                                        auto.color = Console.ReadLine();
                                        Console.Write("Год выпуска: ");
                                        auto.productionYear = Console.ReadLine();
                                        StreamWriter writer = new StreamWriter("A://test/car.xml");
                                        XmlSerializer serializer = new XmlSerializer(typeof(Car));
                                        serializer.Serialize(writer, auto);
                                        writer.Close();
                                        break;
                                    }
                                case 3:
                                    {
                                        Car auto = new Car(); 
                                        Stream streamout = new FileStream("A://test/car.xml", FileMode.Open, FileAccess.Read);
                                        XmlSerializer xml = new XmlSerializer(typeof(Car));
                                        auto = (Car)xml.Deserialize(streamout);
                                        streamout.Close();
                                        Console.WriteLine("Марка  " + "Модель  " + "Цвет  " + "Год выпуска  ");
                                        Console.WriteLine(auto.brand + "    " + auto.model + "    " + auto.color + "    " + auto.productionYear);
                                        break;
                                    }
                                case 4:
                                    {
                                        FileInfo file1Info = new FileInfo("A://test/car.xml");
                                        if (file1Info.Exists)
                                        {
                                            FileInfo fileInf = new FileInfo("A://test/car.xml");
                                            if (fileInf.Exists)
                                            {
                                                fileInf.Delete();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case 5:
                        {
                            string zipfolder = "A://test/0fold/";
                            string sourceFolder = "A://test/";
                            string archive = "A://test/test.zip";
                            string targetFolder = "A://test/unziped";
                            Console.WriteLine("Выберите функцию: ");
                            Console.WriteLine("Выйти в главное меню - 0");
                            Console.WriteLine("Создать архив в формате zip - 1");
                            Console.WriteLine("Добавить файл, выбранный пользователем, в архив - 2");
                            Console.WriteLine("Разархивировать файл и вывести данные о нем - 3");
                            Console.WriteLine("Удалить файл и архив - 4");
                            int vbr1 = Convert.ToInt32(Console.ReadLine());
                            switch (vbr1)
                            {
                                case 0:
                                    {
                                        break;
                                    }
                                case 1:
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(sourceFolder);
                                        if (!dirInfo.Exists)
                                        {
                                            dirInfo.Create();
                                        }
                                        using (FileStream fstream = new FileStream($"{sourceFolder}\\test.zip", FileMode.Create))
                                        {
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(zipfolder);
                                        if (!dirInfo.Exists)
                                        {
                                            dirInfo.Create();
                                        }
                                        Console.WriteLine("Введите название файла, который хотите заархивировать: ");
                                        string zipfile = Console.ReadLine();
                                        string zip1file = "A://test/" + zipfile;
                                        FileInfo fileInf2 = new FileInfo(zip1file);
                                        if (fileInf2.Exists)
                                        {
                                            fileInf2.MoveTo(zipfolder + zipfile);
                                        }
                                        ZipFile.CreateFromDirectory(zipfolder, "A://test/test1.zip");


                                        break;
                                    }
                                case 3:
                                    {
                                        FileInfo fileInf2 = new FileInfo(archive);
                                        if (fileInf2.Exists)
                                        {
                                            DirectoryInfo dirInfo = new DirectoryInfo(targetFolder);
                                            if (!dirInfo.Exists)
                                            {
                                                dirInfo.Create();
                                            }
                                            ZipFile.ExtractToDirectory("A://test/test1.zip", targetFolder);

                                            FileInfo fInfo = new FileInfo(targetFolder + "/note.txt");
                                            if (fInfo.Exists)
                                            {
                                                using (FileStream fstream = File.OpenRead(targetFolder + "/note.txt"))
                                                {
                                                    byte[] array = new byte[fstream.Length];

                                                    await fstream.ReadAsync(array, 0, array.Length);

                                                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                                                    Console.WriteLine($"Текст из файла : {textFromFile}");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Файл не существует");
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("Файл не существует");
                                        }

                                        break;
                                    }
                                case 4:
                                    {
                                        Console.WriteLine("Введите название архива, который хотите удалить: ");
                                        string delzip = Console.ReadLine();
                                        delzip = "A://test/" + delzip;
                                        FileInfo fileInf = new FileInfo(delzip);
                                        if (fileInf.Exists)
                                        {
                                            fileInf.Delete();
                                        }

                                        else
                                        {
                                            Console.WriteLine("Архив не существует");
                                        }
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }
        }
    }
}