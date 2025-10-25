/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This interface contains the most important methods for calculating averages.
----------------------- */

using AverageAssistant.Models;
using AverageAssistant.Services;

namespace AverageAssistant.Services;


public interface IAverageSystem
{
    //This method handles the average calculation. 
    decimal Calculator(Record UsersGrades);

    //This method handles the correction of the average.
    decimal CorrectionCalculator(Record UsersGrades, decimal starterValue, decimal endValue, decimal bestGradeInTheSystem, string functionOfMethod);

    //The following three methods are responsible for formatting an output string.
    string HungarianAverageSystem(Record UsersGrades);
    string RomanianAverageSystem(Record UsersGrades);
    string EnglishAverageSystem(Record UsersGrades);

    //This method is responsible for deciding which one of the Systems will be used.
    string UsedAverageSystem(Record UsersGrades, Record Average, Record SelectedAverageSystem);

}