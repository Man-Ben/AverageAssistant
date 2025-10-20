using CommunityToolkit.Mvvm.ComponentModel;
using AverageAssistant.Services;
using System.Diagnostics;


namespace AverageAssistant.Models;

public class Record   
{

    public List<int> UsersGrades { get; set; } = new();
   
    public int NumberOfLessons;


    public string Grade { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;

    public string SelectedAverageSystem { get; set; } = string.Empty;

    public string? Average {get; set;}

    public string AverageDisplay => !string.IsNullOrWhiteSpace(Average)
    ? Average
    : string.Empty;

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
        ? $"The number of sessions: {NumberOfLessons}"
        : string.Empty;
    public string NumberOfGradesWarning => UsersGrades.Count - NumberOfLessons != 0
        ? $"Warning! You do not not enough grades for this subject! You need {Math.Abs(UsersGrades.Count - NumberOfLessons)} more grade(s)"
        : string.Empty;


    public bool IsNrLessonsVisible => !string.IsNullOrEmpty(NumberOfLessonsDisplay);
    public bool IsUsersGradesVisible => UsersGrades.Count > 0;
    public bool IsSubjectNameVisible => !string.IsNullOrWhiteSpace(SubjectName);
    public bool IsGradeVisible => !string.IsNullOrWhiteSpace(Grade);
    public bool IsAverageVisible => !string.IsNullOrWhiteSpace(AverageDisplay);
    public bool IsNrGradesWarningVisible => !string.IsNullOrWhiteSpace(NumberOfGradesWarning) && IsNrLessonsVisible == true;
}

