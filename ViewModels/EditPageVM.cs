using AverageAssistant.Models;
using AverageAssistant.RecordsVM;
using AverageAssistant.Services;
using AverageAssistant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AverageAssistant.ViewModels;

public partial class EditPageVM:ObservableObject
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
    public string? gradesInput;

    public ObservableCollection<Record> Records { get; set; } = new();

    public ObservableCollection<string> AverageSystemPicker { get; } = new ObservableCollection<string>
{
    "English",
    "Hungarian",
    "Romanian"
};

    public bool IsNrLessonsVisible => SelectedAverageSystem == "Romanian";
    partial void OnSelectedAverageSystemChanged(string? oldValue, string? newValue)
    {
        OnPropertyChanged(nameof(IsNrLessonsVisible));
    }

    public EditPageVM()
    {
        WeakReferenceMessenger.Default.Register<EditPageVM, Record>(this, (r, RecordForEdit) =>
        {
            r.Records ??= new ObservableCollection<Record>();
            r.Records.Add(RecordForEdit);

            r.GradesInput = string.Join(", ", RecordForEdit.UsersGrades);
            r.NumberOfLessonsInput = numberOfLessons.ToString();
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
    public static async Task Edit()
    {

        await Shell.Current.GoToAsync("..");
    }

}