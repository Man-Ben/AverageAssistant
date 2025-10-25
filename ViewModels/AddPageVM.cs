/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This ViewModel handles the input fields and the data entered by the user.
----------------------- */

using AverageAssistant.Models;
using AverageAssistant.Services;
using JsonManagement;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AverageAssistant.ViewModels;

public partial class AddPageTools : ObservableObject
{
    
    //Notify the UI that the visibility of 'Number of lessons' entry may have changed
    partial void OnSelectedAverageSystemChanged(string? oldValue, string? newValue)
    {
        OnPropertyChanged(nameof(IsNrLessonsVisible));
    }

    private readonly IJsonManager _jsonManager;

    public AddPageTools()
    {
        _jsonManager = new JsonManager();
    }


    /*
        This command will be applied when the user want to leave the page without saving the data. 
        If the user press enter, the program will navigate back to the main page.
     */
    [RelayCommand]
    public static async Task ConfirmLeave(string? value)
    {
        bool answer = await Shell.Current.DisplayAlert("Warning!", 
        "Are you sure you want to leave the page without saving? Your data might be lost!", "Yes", "No");

        if (answer)
            await Shell.Current.GoToAsync("..");
    }


    /*
        This command will be executed when the user press the 'Add' button. 
        In the backround the data will be processed, and than send to the main page. After the data was send, 
        the method calls the 'CreateFileForInput' to save the data.
     */
    [RelayCommand]
     public async Task Add()
     {

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

        await _jsonManager.CreateFileForInput(newRecord);

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
    public string gradesInput = string.Empty;

    public ObservableCollection<Record> Records { get; } = new();

    public ObservableCollection<string> AverageSystemPicker { get; } = new ObservableCollection<string>
{
    "English",
    "Hungarian",
    "Romanian"
};

    public bool IsNrLessonsVisible => SelectedAverageSystem == "Romanian";


}