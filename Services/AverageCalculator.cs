/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This file contains the logic for average calculation, average correction,
the output string formation, and decision making.
----------------------- */

using AverageAssistant.Models;
using AverageAssistant.Services;

namespace AverageAssistant.Services;

public class AverageCalculator : IAverageSystem
{

    /*
        This method calculates the average. The 'UsersGrades' parameter
        contains the data to be processed. When the task ends, the method returns the 'average' decimal.
    */

    decimal IAverageSystem.Calculator(Record UsersGrades)
    {
        var gradesList = UsersGrades.UsersGrades;

        decimal sumOfGrades = gradesList.Sum();
        decimal average = 0;
        
        if(gradesList.Count > 1)
            average = sumOfGrades / gradesList.Count;

        return average;
           
    }

    /*
        This method has two functions: counts how many 'best grades' (the best grade in the used average system, 
        for example: in the Romanian system the best grade is 10) are needed, and calculates a better average (for example from 4.30 to 5, 
        or 6.20 to 7).
        The 'UsersGrades' parameter contains the data to be processed again. The starterValue is the first and low average. 
        The endValue is the second and better average. The 'bestGradeInTheSystem' contains the best grade in the current average system.
        This will be added to the starterValue until it reaches the endValue.
        The 'functionOfMethod' parameter has an important rule. If this has the 'Corrected Average' value, the method returns the modified starterValue.
    */

    decimal IAverageSystem.CorrectionCalculator(Record UsersGrades, decimal starterValue, decimal endValue, decimal bestGradeInTheSystem, string functionOfMethod)
    {
        var gradesList = UsersGrades.UsersGrades;

        decimal sumOfGrades = gradesList.Sum();
        
        int numberOfBestGrades = 0;

        while(starterValue <= endValue)
        {
            sumOfGrades += bestGradeInTheSystem;
            numberOfBestGrades++;
            starterValue = sumOfGrades / (gradesList.Count + numberOfBestGrades);
        }

        
        if (functionOfMethod == "CorrectedAverage") 
                return starterValue; 
        else
            return numberOfBestGrades;
        
    }

    /*
      The 'EnglishAverageSystem', the 'HungarianAverageSystem' and the 'RomanianAverageSystem' methods format a string
      that will be displayed as a message to the user. This string contains the following information: the average, a little message for
      the user (ex. 'Congrats'), and - if necessary - the corrected average and the number of best grades needed for correction.
    */

    string IAverageSystem.EnglishAverageSystem(Record UsersGrades)
    {
        IAverageSystem calc = this;

        decimal average = calc.Calculator(UsersGrades);

        if (average == 0)
            return string.Empty;

        string AverageLetter = average switch
        { 
            >= 90 and <= 100 => "A*",
            >= 80 and < 90 => "A",
            >= 70 and < 80 => "B",
            >= 60 and < 70 => "C",
            >= 50 and < 60 => "D",
            >= 40 and < 50 => "E",
            < 40 => "F",
            _=> string.Empty
        };

        string DisplayAverage = AverageLetter switch
        {

            "A*" => $"{average:N2}% {AverageLetter} - Congrats! ;)",
            "A" => $"{average:N2}% {AverageLetter} - Well Done! If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 90, 100, string.Empty)} A* to reach {calc.CorrectionCalculator(UsersGrades, average, 90, 100, "CorrectedAverage"):N2}%",
            "B" => $"{average:N2}% {AverageLetter} - Good! If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 80, 100, string.Empty)} more grades of A* to reach {calc.CorrectionCalculator(UsersGrades, average, 80, 100, "CorrectedAverage"):N2}%",
            "C" => $"{average:N2}% {AverageLetter} - Average. If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 70, 100, string.Empty)} more grades of A* to reach {calc.CorrectionCalculator(UsersGrades, average, 70, 100, "CorrectedAverage"):N2}%",
            "D" => $"{average:N2}% {AverageLetter} - Pass. If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 60, 100, string.Empty)} more grades of A* to reach  {calc.CorrectionCalculator(UsersGrades, average, 60, 100, "CorrectedAverage"):N2}%",
            "E" => $"{average:N2}% {AverageLetter} - Low Pass! Next time study harder! If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 50, 100, string.Empty)} more grades of A* to reach  {calc.CorrectionCalculator(UsersGrades, average, 50, 100, "CorrectedAverage"):N2}%",
            "F" => $"{average:N2}% {AverageLetter} - Uh-oh! You are in trouble, but don't give up! You can still fix this." +
            $"You only need {calc.CorrectionCalculator(UsersGrades, average, 40, 100, string.Empty)} more grades of A* and you'll be safe :)",
            _ => string.Empty
        };

        return DisplayAverage;
    }

    string IAverageSystem.HungarianAverageSystem(Record UsersGrades)
    {
        IAverageSystem calc = this;

        decimal average = calc.Calculator(UsersGrades);

        if (average == 0)
            return string.Empty;

        string DisplayAverage = average switch
        {
            >= 4.5m and <= 5 => $"{Math.Ceiling(average)} - Congrats! ;)",
            >= 4 and < 4.5m => $"{Math.Ceiling(average)} - Well done! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 4.5m, 5, string.Empty)} 5(s) to bring your average up to {calc.CorrectionCalculator(UsersGrades, average, 4.5m, 100, "CorrectedAverage"):N2}",
            >= 3 and < 3.5m => $"{Math.Ceiling(average)} - Good! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 3.5m, 5, string.Empty)} 5(s) to bring your average up to  {calc.CorrectionCalculator(UsersGrades, average, 3.5m, 100, "CorrectedAverage"):N2}",
            >= 2 and < 2.5m => $"{Math.Ceiling(average)} - Next time study harder! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 2.5m, 5, string.Empty)} 5(s) to bring your average up to {calc.CorrectionCalculator(UsersGrades, average, 2.5m, 100, "CorrectedAverage"):N2}",
            >= 1 and < 1.5m => $"{Math.Ceiling(average)} - Uh-oh! You are in trouble, but don't give up! You can still fix this. If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 1.5m, 5, string.Empty)} 5(s), and you'll be safe :)",
            _ => string.Empty
        };

        return DisplayAverage;
        
    }

    string IAverageSystem.RomanianAverageSystem(Record UsersGrades)
    {
        IAverageSystem calc = this;

        decimal average = calc.Calculator(UsersGrades);

        if (average == 0)
            return string.Empty;

        string DisplayAverage = average switch
        {
            >= 9.5m and <= 10 => $"{Math.Ceiling(average)} - Congrats! ;)",
            >= 8.5m and < 9.5m => $"{Math.Ceiling(average)} - Well done! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 9.5m, 10, string.Empty)} 10(s) to bring your average up to {calc.CorrectionCalculator(UsersGrades, average, 9.5m, 10, "CorrectedAverage"):N2}",
            >= 7.5m and < 8.5m => $"{Math.Ceiling(average)} - If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 8.5m, 10, string.Empty)} 10(s) to bring your average up to {calc.CorrectionCalculator(UsersGrades, average, 8.5m, 10, "CorrectedAverage"):N2}",
            >= 6.5m and < 7.5m => $"{Math.Ceiling(average)} - If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 7.5m, 10, string.Empty)} 10(s) to bring your average up to  {calc.CorrectionCalculator(UsersGrades, average, 7.5m, 10, "CorrectedAverage"):N2}",
            >= 5.5m and < 6.5m => $"{Math.Ceiling(average)} - If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 6.5m, 10, string.Empty)} 10(s) to bring your average up to  {calc.CorrectionCalculator(UsersGrades, average, 6.5m, 10, "CorrectedAverage"):N2}",
            >= 4.5m and < 5.5m => $"{Math.Ceiling(average)} - If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 5.5m, 10, string.Empty)} 10(s) to bring your average up to  {calc.CorrectionCalculator(UsersGrades, average, 5.5m, 10, "CorrectedAverage"):N2}",
            < 4.5m => $"{Math.Ceiling(average)} - Uh-oh, you are in trouble, but don't give up! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 4.5m, 10, string.Empty)} 10(s) to bring your average up to  {calc.CorrectionCalculator(UsersGrades, average, 4.5m, 10, "CorrectedAverage"):N2}",
            _ => string.Empty
        };

        return DisplayAverage;
        
    }

    /*
        This method decides which of the three average systems will be used.
     */

    string IAverageSystem.UsedAverageSystem(Record UsersGrades, Record Average, Record SelectedAverageSystem)
    {
        IAverageSystem calc = this;

        var SelectedSystem = SelectedAverageSystem.SelectedAverageSystem;

        Average.Average = SelectedSystem switch
        {
            "English" => calc.EnglishAverageSystem(UsersGrades),
            "Hungarian" => calc.HungarianAverageSystem(UsersGrades),
            "Romanian" => calc.RomanianAverageSystem(UsersGrades),
            _ => string.Empty
        };

        return Average.Average;
    }

}