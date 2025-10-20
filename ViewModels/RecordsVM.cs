using AverageAssistant;
using AverageAssistant.Models;
using AverageAssistant.Services;
using AverageAssistant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JsonManagement;
namespace AverageAssistant.RecordsVM;

public partial class RecordVM:ObservableObject
{
    public Record Model { get; set; }

    public RecordVM(Record model)
    {
        Model = model;
        var Calculator = new AverageCalculator();
        Model.Average = ((IAverageSystem)Calculator).UsedAverageSystem(Model, Model, Model);
    }

    public async Task FileHandling()
    {
        var FileHandler = new JsonManager();
        await ((IJsonManager)FileHandler).CreateFileForFlags(Model);
        Model = await ((IJsonManager)FileHandler).ReadFromFileInput();
    }

    [RelayCommand]
    public async Task DeleteFile()
    {
        var deleteFile = new JsonManager();

        await ((IJsonManager)deleteFile).DeleteFile(Model);
    }

    public string? AverageDisplay => Model.AverageDisplay;
    public string GradeDisplay => Model.GradeDisplay;
    public string SubjectNameDisplay => Model.SubjectName;
    public string GradesDisplay => Model.GradesDisplay;
    public string NumberOfLessonsDisplay => Model.NumberOfLessonsDisplay;
    public string NumberOfLessonsWarning => Model.NumberOfGradesWarning;

    public bool IsNrLessonsVisible => Model.IsNrLessonsVisible;
    public bool IsUsersGradesVisible => Model.IsUsersGradesVisible;
    public bool IsSubjectNameVisible => Model.IsSubjectNameVisible;
    public bool IsGradeVisible => Model.IsGradeVisible;
    public bool IsAverageVisible => Model.IsAverageVisible;
    public bool IsNrGradesWarningVisible => Model.IsNrGradesWarningVisible;

    

}

