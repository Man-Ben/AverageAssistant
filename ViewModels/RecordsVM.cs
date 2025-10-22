using AverageAssistant;
using AverageAssistant.Models;
using AverageAssistant.Services;
using AverageAssistant.ViewModels;
using AverageAssistant.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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

        var inputRecord = await ((IJsonManager)FileHandler).ReadFromFile();

        if (inputRecord != null)
        {
            Model.Grade = inputRecord.Grade;
            Model.SubjectName = inputRecord.SubjectName;
            Model.UsersGrades = inputRecord.UsersGrades;
            Model.NumberOfLessons = inputRecord.NumberOfLessons;
        }

        OnPropertyChanged(nameof(Model));

    }



    [RelayCommand]
    public async Task DeleteFile()
    {
        bool answer = await Shell.Current.DisplayAlert("Warning!", "Are you sure that you want to delete this object?", "Delete", "Cancel");

        if (answer)
        {
            var deleteFile = new JsonManager();
            await ((IJsonManager)deleteFile).DeleteFile(Model);
        }
    }

    [RelayCommand]
    public async Task GoToEdit()
    {
        var RecordForEdit = new Record()
        {
            Grade = Model.Grade,
            SubjectName = Model.SubjectName,
            UsersGrades = Model.UsersGrades,
            NumberOfLessons = Model.NumberOfLessons,
            SelectedAverageSystem = Model.SelectedAverageSystem
        };

        WeakReferenceMessenger.Default.Send(RecordForEdit);

        await Shell.Current.GoToAsync(nameof(EditPage));
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

