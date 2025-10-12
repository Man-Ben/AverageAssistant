namespace AverageAssistant.ViewModels;

using AverageAssistant.Models;
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
    public ObservableCollection<int> usersGrades = new();

    [ObservableProperty]
    public int numberOfLessons;

    public ObservableCollection<Record> Records { get; } = new();

    public CaViewModel()
    {
        WeakReferenceMessenger.Default.Register<CaViewModel,Record>(this, (r, newRecord) =>
        {
                r.Records.Add(newRecord);
        });
    }

    [RelayCommand]
    public async Task GoToAddPage()
    {
        await Shell.Current.GoToAsync(nameof(AddPage));
    }
}