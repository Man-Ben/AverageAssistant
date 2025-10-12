using CommunityToolkit.Mvvm.ComponentModel;
using AverageAssistant.Services;

namespace AverageAssistant.Models;

public class Record
{
    public List<int> UsersGrades { get; set; } = new();
    public List<decimal> UsersAverages { get; set; } = new();
    public int NumberOfLessons;


    public string Grade { get; set; } = string.Empty;
    public string? SubjectName { get; set; } = string.Empty;


    public string SubjectNameDisplay => !string.IsNullOrWhiteSpace(SubjectName)
        ? $"{SubjectName}"
        : string.Empty;
    public string GradeDisplay => !string.IsNullOrWhiteSpace(Grade)
        ? $"{Grade}"
        : string.Empty;
    public string GradesDisplay => UsersGrades.Count > 0
        ? $"Your grades: {string.Join(", ", UsersGrades)}"
        : string.Empty;
    public string NumberOfLessonsDisplay => NumberOfLessons > 0
        ? $"The number of sessons: {NumberOfLessons}"
        : string.Empty;
    public string? SelectedAverageSystem;
    public string? AverageDisplay;


    public bool IsNrLessonsVisible { get; set; }
    public bool IsUsersGradesVisible => UsersGrades.Count > 0;
    public bool IsSubjectNameVisible => !string.IsNullOrWhiteSpace(SubjectName);
    public bool IsGradeVisible => !string.IsNullOrWhiteSpace(Grade);
}
