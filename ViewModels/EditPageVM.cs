using AverageAssistant.Models;
using AverageAssistant.RecordsVM;
using AverageAssistant.Services;
using AverageAssistant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JsonManagement;
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
    public async Task Edit(Record recordToEdit)
    {
        

        if (recordToEdit == null)
            return;

        recordToEdit.Grade = this.Grade ?? recordToEdit.Grade;
        recordToEdit.SubjectName = this.SubjectName ?? recordToEdit.SubjectName;
        recordToEdit.NumberOfLessons = this.NumberOfLessons;
        
        recordToEdit.UsersGrades.Clear();

        if (!string.IsNullOrWhiteSpace(GradesInput))
        {
            var gradeInNumbers = GradesInput
                                .Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries)
                                .Select(s => int.TryParse(s, out var number) ? number : 0);
            foreach (var grade in gradeInNumbers)
                recordToEdit.UsersGrades.Add(grade);
        }

        var FileHandler = new JsonManager();
        await ((IJsonManager)FileHandler).DeleteFile(recordToEdit);
        await ((IJsonManager)FileHandler).CreateFileForInput(recordToEdit);


        WeakReferenceMessenger.Default.Send(recordToEdit);

        await Shell.Current.GoToAsync("..");
    }

}