using AverageAssistant.Services;
using AverageAssistant.Models;

namespace JsonManagement;

public interface IJsonManager
{
    public Task CreateFileForInput(Record record);
    public Task CreateFileForFlags(Record record);
    public Task<Record> ReadFromFileInput();

    public Task DeleteFile(Record record);

    public Task RenameFile();

}