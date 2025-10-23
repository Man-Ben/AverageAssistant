using AverageAssistant.Services;
using AverageAssistant.Models;

namespace JsonManagement;

public interface IJsonManager
{
    public Task CreateFileForInput(Record record);
    public Task CreateFileForSettings(string settingToSave);
    public Task<Record> ReadFromFile();

    public Task DeleteFile(Record record);


}