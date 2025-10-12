using AverageAssistant.Models;

namespace AverageAssistant.Services;

public interface IAverageSystem
{
    decimal Calculator(Record UsersGrades, Record UsersAverages);
    string HungarianAverageSystem(Record UsersGrades, Record UsersAverages);
    string RomanianAverageSystem(Record UsersGrades, Record UsersAverages);
    string EnglishAverageSystem(Record UsersGrades, Record UsersAverages);
    string UsedGradeSystem();

}