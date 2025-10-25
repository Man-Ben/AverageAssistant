/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This file contains the logic for creating JSON files, data writing, data reading and deleting files
----------------------- */


using AverageAssistant.Services;
using AverageAssistant.Models;
using System.Text.Json;

namespace JsonManagement;

public class JsonManager:IJsonManager
{

    /*
        This method is used to create files, that will contain the data entered by the user. The files will be stored in a
        directory named 'Userdata'. If the directory does not exist, the method will create it. 
     */

    async Task IJsonManager.CreateFileForInput(Record record)
    {
        string gradePart = string.IsNullOrWhiteSpace(record.Grade) ? "UnknownGrade" : new string(record.Grade.ToLower().Where(char.IsLetterOrDigit).ToArray());
        string subjectPart = string.IsNullOrWhiteSpace(record.SubjectName) ? "UnknownSubject" : new string(record.SubjectName.ToLower().Where(char.IsLetterOrDigit).ToArray());

        string fileName = $"{gradePart}_{subjectPart}.json";

        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "UserData");

        Directory.CreateDirectory(jsonDir);

        string filePath = Path.Combine(jsonDir, fileName);

        var inputData = new
        {
            record.Grade,
            record.SubjectName,
            record.UsersGrades,
            record.SelectedAverageSystem,
            record.NumberOfLessons
            
        };

        string jsonInput = JsonSerializer.Serialize(inputData, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, jsonInput);
    }

    /*
        This method's purpose is to create a single file named 'Settings.json', that will contain the settings modified by the user. The file will be stored in a
        directory named 'Settings'. If the directory does not exist, the method will create it. 
    */

    async Task IJsonManager.CreateFileForSettings(string settingToSave)
    {
        string fileName = $"Settings.json";
        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "Settings");
        string filePath = Path.Combine(jsonDir, fileName);

        Directory.CreateDirectory(jsonDir);

        var dict = new Dictionary<string, string>
        {
            {"Theme", settingToSave }
        };

        string jsonInput = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(filePath, jsonInput);
    }


    /*
        The task of this method is to read all the files found in the 'UserData' directory. If the directory does not exist, the method 
        will create it.
     */

    async Task<List<Record>> IJsonManager.ReadFromFile()
    {
        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "UserData");

        Directory.CreateDirectory(jsonDir);

        string[] files = Directory.GetFiles(jsonDir, "*.json");

        var records = new List<Record>();

        foreach (var file in files)
        {
            string json = await File.ReadAllTextAsync(file);
            var recordReading = JsonSerializer.Deserialize<Record>(json);

            if(recordReading != null)
                records.Add(recordReading);
        }

        return records;
    }

    /*
        This method will read the data from the 'Settings.json' found in the 'Settings' directory. If the directory does not exist,
        the method will create it.
     */

    async Task<string?> IJsonManager.ReadFromFileSettings()
    {
        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "Settings");
        string fileName = "Settings.json";
        string filePath = Path.Combine(jsonDir, fileName);

        Directory.CreateDirectory(jsonDir);

        if (!File.Exists(filePath))
            return null;

        string json = await File.ReadAllTextAsync(filePath);
        var dataReading = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

        return dataReading != null && dataReading.ContainsKey("Theme") ? dataReading["Theme"] : null;
    }

    /*
        This method will be used more often than the others. It's purpose of this is the deleting the files selected by the user.
     */

    async Task IJsonManager.DeleteFile(Record record)
    {
        string gradePart = string.IsNullOrWhiteSpace(record.Grade)
            ? "UnknownGrade"
            : new string(record.Grade.ToLower().Where(char.IsLetterOrDigit).ToArray());

        string subjectPart = string.IsNullOrWhiteSpace(record.SubjectName)
            ? "UnknownSubject"
            : new string(record.SubjectName.ToLower().Where(char.IsLetterOrDigit).ToArray());


        string DeleteFile = $"{gradePart}_{subjectPart}.json";
        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "UserData");

        string Delete = Path.Combine(jsonDir, DeleteFile);


        if (File.Exists(Delete))
            await Task.Run(() => File.Delete(Delete));
    }
}