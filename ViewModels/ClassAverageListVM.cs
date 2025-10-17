namespace AverageAssistant.ViewModels;

using AverageAssistant.Models;
using AverageAssistant.RecordsVM;
using AverageAssistant.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging;


public partial class CaViewModel : ObservableObject
{
    [ObservableProperty]
    public string? grade;

    [ObservableProperty]
    public string? subjectName;

    [ObservableProperty]
    public int numberOfLessons;

    [ObservableProperty]
    public string? averageDisplay;

    [ObservableProperty]
    public string? numberOfLessonsWarning;

    [ObservableProperty]
    public ObservableCollection<int> usersGrades = new();


    public ObservableCollection<RecordVM> Records { get; } = new();

    public CaViewModel()
    {
        WeakReferenceMessenger.Default.Register<CaViewModel,Record>(this, (r, newRecord) =>
        {
            var vm = new RecordVM(newRecord);   
            r.Records.Add(vm);
        });
    }

    [RelayCommand]
    public async Task GoToAddPage()
    {
        await Shell.Current.GoToAsync(nameof(AddPage));
    }
}