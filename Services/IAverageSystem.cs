using AverageAssistant.Models;
using AverageAssistant.Services;

namespace AverageAssistant.Services;

public interface IAverageSystem
{
    decimal Calculator(Record UsersGrades);
    decimal CorrectionCalculator(Record UsersGrades, decimal starterValue, decimal endValue, decimal bestGradeInTheSystem, string functionOfMethod);
    string HungarianAverageSystem(Record UsersGrades);
    string RomanianAverageSystem(Record UsersGrades);
    string EnglishAverageSystem(Record UsersGrades);
    string UsedAverageSystem(Record UsersGrades, Record Average, Record SelectedAverageSystem);

}