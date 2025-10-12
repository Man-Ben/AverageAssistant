namespace AverageAssistant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using AverageAssistant.Models;

public partial class AddPageTools : ObservableObject
{
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
    "Romanian",
    "Hungarian"
};

    public bool IsNrLessonsVisible => selectedAverageSystem == "Romanian";

    partial void OnSelectedAverageSystemChanged(string? oldValue, string? newValue)
    {
        OnPropertyChanged(nameof(IsNrLessonsVisible));
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
            newRecord.IsNrLessonsVisible = IsNrLessonsVisible;
            newRecord.SelectedAverageSystem = selectedAverageSystem ?? string.Empty;
        }

        WeakReferenceMessenger.Default.Send(newRecord);

        await Shell.Current.GoToAsync("..");
     }
}