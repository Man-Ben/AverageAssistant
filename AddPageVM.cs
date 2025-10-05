namespace AverageAssistant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;

public partial class AddPageTools : ObservableObject
{
    [ObservableProperty]
    public string? grade;

    [ObservableProperty]
    public string? subjectName;

    [ObservableProperty]
    public string? numberOfLessons;

    [ObservableProperty]
    public string? usersGrades;

    [ObservableProperty]
    public string? selectedGradeSystem;

    public ObservableCollection<string> GradeSystem { get; } = new ObservableCollection<string>
{
    "American",
    "Romanian",
    "Hungarian"
};

    public bool IsVisible => SelectedGradeSystem == "Romanian";

    partial void OnSelectedGradeSystemChanged(string? value)
    {
        OnPropertyChanged(nameof(IsVisible));
    }

    [RelayCommand]
    public static async Task ConfirmLeave(string? value)
    {
        bool answer = await Shell.Current.DisplayAlert("Warning!", "Are you sure you want to leave the page without saving? Your data might be lost!", "Yes", "No");
        if (answer)
            await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
     public async Task Add()
     {
        if (!string.IsNullOrWhiteSpace(UsersGrades))
        {
            var cleanedData = UsersGrades?
                .Split(' ', ',')
                .Select(x => double.TryParse(x, out double n) ? n:0)
                .ToList() ?? new List<double>();
            WeakReferenceMessenger.Default.Send(new Messengers.GradesListMessage(cleanedData: cleanedData));
            UsersGrades = string.Empty;
        }
        await Shell.Current.GoToAsync("..");
     }
}