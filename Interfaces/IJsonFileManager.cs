/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This interface contains the most important methods for file handling (creating, writing, reading and deleting JSON files).
----------------------- */

using AverageAssistant.Services;
using AverageAssistant.Models;

namespace JsonManagement;

public interface IJsonManager
{
    //This method is responsible for creating a JSON file and writing the user's data to it.
    public Task CreateFileForInput(Record record);

    //This method is responsible for creating a JSON file and writing the settings if the user made any changes.
    public Task CreateFileForSettings(string settingToSave);

    //The folowing two methods handle the data reading.
    public Task<List<Record>> ReadFromFile();
    public Task<string?> ReadFromFileSettings();

    //And this one is used to delete files.
    public Task DeleteFile(Record record);

}