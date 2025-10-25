/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This ViewModel handles the data that will be displayed on the main page's CollectionView.
----------------------- */

/*----------------------
 Detected issues

 1. When the program loads data from files, an unexpected issue occurs: when the average system is 'Romanian' two important display 
 strings ('NumberOfLessonsDisplay' and 'NumberGradesWarning') remains empty. Cause: unknown.
 
 2. After modifying the user's data, the old data is still visible on the UI, despite of the 'delete screen'. Cause: unknown.

 I tried my best, the fixing was unsuccessful.
 -----------------------*/

using AverageAssistant;
using AverageAssistant.Models;
using AverageAssistant.Services;
using AverageAssistant.ViewModels;
using AverageAssistant.Views;
using JsonManagement;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AverageAssistant.RecordsVM;

public partial class RecordVM : ObservableObject
{
    /*
        In the constructor the VM receives the data send by the 'EditPageVM'.
        The '_OnDeleted' action is stored to allow the VM to notify the central collection
        when this record is deleted, so the UI can be updated automatically.
    */
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

    /*
        Here the program calculates what will the user see.
     */
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

    /*
        If the user press the 'Delete' button this method will be called.
        This deletes the selected file, and updates the UI - clears the selected item.
     */

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

    /*
        This command sends the user to the 'EditPage'. 
        Here I made a little trick. Instead of just renaming the file - if user changes it - and write in it, I just delete it
        after sending the data to the 'EditPageVM', and when the user navigates back, a new file will be created.
     */

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
    public Record Model { get; set; }

    private readonly Action<RecordVM> _OnDeleted;
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