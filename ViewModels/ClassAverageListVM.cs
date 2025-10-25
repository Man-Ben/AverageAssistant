/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This ViewModel handles the main page.
----------------------- */

using AverageAssistant.Models;
using AverageAssistant.RecordsVM;
using AverageAssistant.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging;
using JsonManagement;
using AverageAssistant.SettingsPageVM;

namespace AverageAssistant.ViewModels;

public partial class CaViewModel : ObservableObject
{

    /*
        In the constructor the ViewModel receives the data send by the AddPage.
        When a new record is received, it wraps it in the 'RecordVM' and adds it to the 'Records' collection
     */
    public CaViewModel()
    {
        WeakReferenceMessenger.Default.Register<CaViewModel, Record>(this, (r, newRecord) =>
        {

            var vm = new RecordVM(newRecord, recordVM =>
            {
                r.Records.Remove(recordVM);

            });
            r.Records.Add(vm);

        });
    }

    /*
        This task will load the data from the JSON files, converts them to 'RecordVM' instances and adds them to the 'Records' collection.
     */
    public async Task LoadRecordsFormFiles()
    {
        var FileHandler = new JsonManager();
        var FileRecord = await ((IJsonManager)FileHandler).ReadFromFile();


        foreach (var record in FileRecord)
        {
            var vm = new RecordVM(record, r => Records.Remove(r));
            vm.CalculateDisplayProperties();
            Records.Add(vm);

        }

    }

    //This command navigates the user to the AddPage for entering the data.

    [RelayCommand]
    public async Task GoToAddPage()
    {
        await Shell.Current.GoToAsync(nameof(AddPage));
    }


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
}