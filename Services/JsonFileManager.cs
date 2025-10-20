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

    async Task IJsonManager.CreateFileForFlags(Record record)
    {
        string gradePart = string.IsNullOrWhiteSpace(record.Grade) 
            ? "UnknownGrade" 
            : new string(record.Grade.ToLower().Where(char.IsLetterOrDigit).ToArray());

        string subjectPart = string.IsNullOrWhiteSpace(record.SubjectName) 
            ? "UnknownSubject" 
            : new string(record.SubjectName.ToLower().Where(char.IsLetterOrDigit).ToArray());

        string flagsFileName = $"{gradePart}_{subjectPart}_flags.json";

        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "FlagData");

        Directory.CreateDirectory(jsonDir);

        string filePath = Path.Combine (jsonDir, flagsFileName);

        var flagsData = new
        {
            record.IsGradeVisible,
            record.IsSubjectNameVisible,
            record.IsUsersGradesVisible,
            record.IsAverageVisible,
            record.IsNrLessonsVisible,
            record.IsNrGradesWarningVisible
        };

        string jsonFlags = JsonSerializer.Serialize(flagsData, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, jsonFlags);
    }


    //It doesn't working, I don't know why. Come back here later!
    async Task<Record> IJsonManager.ReadFromFileInput()
    {
        string baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string jsonDir = Path.Combine(baseDir, "UserData");

        Directory.CreateDirectory(jsonDir);

        string[] files = Directory.GetFiles(jsonDir, "*.json");

        var ReadRecords = new List<Record>();

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
        string DeleteFlagFile = $"{gradePart}_{subjectPart}_flags.json";

        if (File.Exists(DeleteFile) && File.Exists(DeleteFlagFile))
        {
            await Task.Run(() => File.Delete(DeleteFile));
            await Task.Run(() => File.Delete(DeleteFlagFile));
        }
    }

    async Task IJsonManager.RenameFile()
    {
        throw new NotImplementedException();
    }


}