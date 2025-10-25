/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This ViewModel handles the modified data entered by the user. It acts like the 'AddPage', and there are very much commun commands,
so I won't document again everything.
----------------------- */

/*----------------------
 Detected issues

 I wanted to display the received data in the entries, but it does not work. Cause: Unknown.

 I tried my best, the fixing was unsuccessful.
 -----------------------*/


using AverageAssistant.Models;
using AverageAssistant.RecordsVM;
using AverageAssistant.Services;
using AverageAssistant.ViewModels;
using JsonManagement;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AverageAssistant.ViewModels;

public partial class EditPageVM : ObservableObject
{
    partial void OnSelectedAverageSystemChanged(string? oldValue, string? newValue)
    {
        OnPropertyChanged(nameof(IsNrLessonsVisible));
    }

    /*
        In the constructor the 'EditPageVM' receives the existing data from the 'RecordsVM'.
        Later in this code the existing code that was modified by the user, the program will send back to the 'RecordsVM'.
     */

    public EditPageVM()
    {
        WeakReferenceMessenger.Default.Register<EditPageVM, Record>(this, (r, RecordForEdit) =>
        {

            r.Grade = RecordForEdit.Grade;
            r.SubjectName = RecordForEdit.SubjectName;
            r.SelectedAverageSystem = RecordForEdit.SelectedAverageSystem;
            r.NumberOfLessonsInput = RecordForEdit.NumberOfLessons.ToString();

            r.UsersGrades.Clear();

            foreach (var item in RecordForEdit.UsersGrades)
                r.UsersGrades.Add(item);

            r.GradesInput = string.Join(", ", RecordForEdit.UsersGrades);
        });
    }


    [RelayCommand]
    public static async Task ConfirmLeave(string? value)
    {
        bool answer = await Shell.Current.DisplayAlert("Warning!",
        "Are you sure you want to leave the page without saving? Your data might be lost!", "Yes", "No");

        if (answer)
            await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task Edit()
    {

        var FileHandler = new JsonManager();

        UsersGrades.Clear();
        if (!string.IsNullOrWhiteSpace(GradesInput))
        {
            var gradeInNumbers = GradesInput
                                .Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries)
                                .Select(s => int.TryParse(s, out var number) ? number : 0);
            foreach (var grade in gradeInNumbers)
                UsersGrades.Add(grade);
        }

        NumberOfLessons = int.TryParse(NumberOfLessonsInput, out var lessons) ? lessons : 0;

        var newRecord = new Record();
        {
            newRecord.Grade = Grade ?? string.Empty;
            newRecord.SubjectName = SubjectName ?? string.Empty;
            newRecord.UsersGrades = UsersGrades.ToList();
            newRecord.NumberOfLessons = NumberOfLessons;
            newRecord.SelectedAverageSystem = SelectedAverageSystem ?? string.Empty;
        }

        WeakReferenceMessenger.Default.Send(newRecord);

        await ((IJsonManager)FileHandler).CreateFileForInput(newRecord);

        await Shell.Current.GoToAsync("..");

    }


    [ObservableProperty]
    public string? grade;

    [ObservableProperty]
    public string? subjectName;

    [ObservableProperty]
    public ObservableCollection<int> usersGrades = new();

    [ObservableProperty]
    public int numberOfLessons;

    [ObservableProperty]
    public string? numberOfLessonsInput;

    [ObservableProperty]
    public string? selectedAverageSystem;

    [ObservableProperty]
    public string? gradesInput;

    public ObservableCollection<Record> Records { get; set; } = new();

    public ObservableCollection<string> AverageSystemPicker { get; } = new ObservableCollection<string>
{
    "English",
    "Hungarian",
    "Romanian"
};

    public bool IsNrLessonsVisible => SelectedAverageSystem == "Romanian";
}