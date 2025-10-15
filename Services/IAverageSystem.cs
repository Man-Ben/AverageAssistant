using AverageAssistant.Models;

namespace AverageAssistant.Services;

public interface IAverageSystem
{
    decimal Calculator(Record UsersGrades, Record UsersAverages);
    decimal CorrectionCalculator(Record UsersGrades, decimal starterValue, decimal endValue, decimal bestGradeInTheSystem);
    string HungarianAverageSystem(Record UsersGrades, Record UsersAverages);
    string RomanianAverageSystem(Record UsersGrades, Record UsersAverages);
    string EnglishAverageSystem(Record UsersGrades, Record UsersAverages);
    string UsedGradeSystem(Record AverageDisplay, Record SelectedAverageSystem);

}