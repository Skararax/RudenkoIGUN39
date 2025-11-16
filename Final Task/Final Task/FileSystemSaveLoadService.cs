using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FileSystemSaveLoadService : ISaveLoadService<string>
{
    private string _saveDirectory;

    public FileSystemSaveLoadService(string savePath)
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
            Console.WriteLine($"Создана папка: {savePath}");
        }

        _saveDirectory = savePath;

        Console.WriteLine($"Сервис готов. Папка: {_saveDirectory}");
    }

    public void SaveData(string data, string fileName)
    {
        string filePath = Path.Combine(_saveDirectory, fileName + ".txt");

        try
        {
            File.WriteAllText(filePath, data);
            Console.WriteLine($"Данные сохранены в: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
    }

    public string LoadData(string fileName)
    {
        string filePath = Path.Combine(_saveDirectory, fileName + ".txt");

        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл не найден: {filePath}");
                return null;
            }

            string data = File.ReadAllText(filePath);
            Console.WriteLine($"Данные загружены из: {filePath}");
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            return null;
        }
    }
} 


