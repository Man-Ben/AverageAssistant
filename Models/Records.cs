using AverageAssistant.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using JsonManagement;
using System.Diagnostics;


namespace AverageAssistant.Models;

public record Record
{
    public List<int> UsersGrades { get; set; } = new();

    public int NumberOfLessons;

    public string Grade { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string SelectedAverageSystem { get; set; } = string.Empty;
    public string? Average {get; set;}
}

