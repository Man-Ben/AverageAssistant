using CommunityToolkit.Mvvm.ComponentModel;

namespace AverageAssistant.Models;

public class Record
{
    public string Grade { get; set; } = string.Empty;
    public string? SubjectName { get; set; } = string.Empty;
    public int NumberOfLessons;
    public List<int> UsersGrades { get; set; } = new();
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
    public bool IsVisible { get; set; }
}
