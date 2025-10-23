using AverageAssistant.Services;
using AverageAssistant.Models;
using System.Text.Json;

namespace JsonManagement;

public class JsonManager:IJsonManager
{
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
            record.NumberOfLessons,
        };

        string jsonInput = JsonSerializer.Serialize(inputData, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, jsonInput);


    }

    async Task IJsonManager.CreateFileForSettings(string settingToSave)
    {
        string fileName = $"Settings.json";
        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "Settings");
        string filePath = Path.Combine(jsonDir, fileName);

        Directory.CreateDirectory(jsonDir);


        string jsonInput = JsonSerializer.Serialize(settingToSave, new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(filePath, jsonInput);
    }


    async Task<Record> IJsonManager.ReadFromFile()
    {
        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "UserData");

        Directory.CreateDirectory(jsonDir);

        string[] files = Directory.GetFiles(jsonDir, "*.json");


        foreach(var file in files)
        {
            string json = await File.ReadAllTextAsync(file);
            var recordReading = JsonSerializer.Deserialize<Record>(json);

            if(recordReading != null)
                return recordReading;
        }
        return null!;

    }
   
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