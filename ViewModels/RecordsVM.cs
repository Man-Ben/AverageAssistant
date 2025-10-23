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
    private readonly Action<RecordVM> _OnDeleted;

    public RecordVM(Record model, Action<RecordVM> ondeleted)
    {
        Model = model;
        _OnDeleted = ondeleted;

        var Calculator = new AverageCalculator();
        Model.Average = ((IAverageSystem)Calculator).UsedAverageSystem(Model, Model, Model);

        WeakReferenceMessenger.Default.Register<RecordVM, Record>(this, (r, msg) =>
        {
            r.Model.Grade = msg.Grade;
            r.Model.NumberOfLessons = msg.NumberOfLessons;
            r.Model.SubjectName = msg.SubjectName;

            Model.UsersGrades.Clear();
            foreach (var grade in r.Model.UsersGrades)
                r.Model.UsersGrades = msg.UsersGrades;
        });

    }

    public async Task FileHandling()
    {
        var FileHandler = new JsonManager();

        var inputRecord = await ((IJsonManager)FileHandler).ReadFromFile();

        if (inputRecord != null)
        {
            Model.Grade = inputRecord.Grade;
            Model.SubjectName = inputRecord.SubjectName;
            Model.NumberOfLessons = inputRecord.NumberOfLessons;

            Model.UsersGrades.Clear();
            foreach (var grade in inputRecord.UsersGrades)
                Model.UsersGrades.Add(grade);
        }

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

        _OnDeleted?.Invoke(this);

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

