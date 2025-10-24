using AverageAssistant;
using AverageAssistant.Models;
using AverageAssistant.Services;
using AverageAssistant.ViewModels;
using AverageAssistant.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JsonManagement;
using System.Collections.ObjectModel;
namespace AverageAssistant.RecordsVM;

public partial class RecordVM : ObservableObject
{
    public Record Model { get; set; }

    private readonly Action<RecordVM> _OnDeleted;


    public RecordVM(Record model, Action<RecordVM> ondeleted)
    {
        Model = model;
        _OnDeleted = ondeleted;

        CalculateDisplayProperties();

        WeakReferenceMessenger.Default.Register<RecordVM, Record>(this, (r, msg) =>
        {
            r.Model.Grade = msg.Grade;
            r.Model.NumberOfLessons = msg.NumberOfLessons;
            r.Model.SubjectName = msg.SubjectName;

            Model.UsersGrades.Clear();
            foreach (var grade in msg.UsersGrades)
                Model.UsersGrades.Add(grade);
        });


    }

    public void CalculateDisplayProperties()
    {
        var Calculator = new AverageCalculator();
        Model.Average = ((IAverageSystem)Calculator).UsedAverageSystem(Model, Model, Model);

        AverageDisplay = !string.IsNullOrWhiteSpace(Model.Average)
        ? Model.Average
        : string.Empty;

        SubjectNameDisplay = !string.IsNullOrWhiteSpace(Model.SubjectName)
        ? $"{Model.SubjectName}"
        : string.Empty;

        GradeDisplay = !string.IsNullOrWhiteSpace(Model.Grade)
        ? $"{Model.Grade}"
        : string.Empty;

        GradesDisplay = Model.UsersGrades.Count > 0
        ? $"Your grades: {string.Join(", ", Model.UsersGrades)}"
        : string.Empty;

        NumberOfLessonsDisplay = Model.NumberOfLessons > 0
        ? $"The number of sessions: {Model.NumberOfLessons}"
        : string.Empty;

        NumberOfGradesWarning = Model.UsersGrades.Count - (Model.NumberOfLessons+3) != 0
        ? $"Warning! You do not have enough grades for this subject! You need {Math.Abs(Model.UsersGrades.Count - (Model.NumberOfLessons + 3))} more grade(s)"
        : string.Empty;

        IsNrLessonsVisible = !string.IsNullOrEmpty(NumberOfLessonsDisplay);
        IsUsersGradesVisible = Model.UsersGrades.Count > 0;
        IsSubjectNameVisible = !string.IsNullOrWhiteSpace(Model.SubjectName);
        IsGradeVisible = !string.IsNullOrWhiteSpace(Model.Grade);
        IsAverageVisible = !string.IsNullOrWhiteSpace(AverageDisplay);
        IsNrGradesWarningVisible = !string.IsNullOrWhiteSpace(NumberOfGradesWarning) && IsNrLessonsVisible == true;


    }

    [RelayCommand]
    public async Task DeleteFile()
    {
        bool answer = await Shell.Current.DisplayAlert("Warning!", "Are you sure that you want to delete this object?", "Delete", "Cancel");

        if (answer)
        {
            var deleteFile = new JsonManager();

            await ((IJsonManager)deleteFile).DeleteFile(Model);

            _OnDeleted?.Invoke(this);
        }

        

    }

    [RelayCommand]
    public async Task GoToEdit(ObservableCollection<RecordVM> centralCollection)
    {

        var RecordForEdit = new Record()
        {
            Grade = Model.Grade,
            SubjectName = Model.SubjectName,
            UsersGrades = Model.UsersGrades.ToList(),
            NumberOfLessons = Model.NumberOfLessons,
            SelectedAverageSystem = Model.SelectedAverageSystem
        };

        WeakReferenceMessenger.Default.Send(RecordForEdit);

        _OnDeleted?.Invoke(this);

        var FileHandler = new JsonManager();
        await ((IJsonManager)FileHandler).DeleteFile(RecordForEdit);

        await Shell.Current.GoToAsync(nameof(EditPage));
    }


    public string? AverageDisplay { get; set; }
    public string? SubjectNameDisplay { get; set; }
    public string? GradeDisplay { get; set; }
    public string? GradesDisplay { get; set; }
    public string? NumberOfLessonsDisplay { get; set; }
    public string? NumberOfGradesWarning { get; set; }

    public bool IsNrLessonsVisible { get; set; }
    public bool IsUsersGradesVisible { get; set; }
    public bool IsSubjectNameVisible { get; set; }
    public bool IsGradeVisible { get; set; }
    public bool IsAverageVisible { get; set; }
    public bool IsNrGradesWarningVisible { get; set; }
}