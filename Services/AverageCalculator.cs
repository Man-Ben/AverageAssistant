using AverageAssistant.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace AverageAssistant.Services;

public class AverageCalculator : IAverageSystem
{
    decimal IAverageSystem.Calculator(Record UsersGrades, Record UsersAverages)
    {
        var gradesList = UsersGrades.UsersGrades;

        decimal sumOfGrades = gradesList.Sum();
        decimal average = 0;

        if (gradesList.Count == 0)
            return average;
        
        average = sumOfGrades / gradesList.Count;
        return average;
           
    }

    decimal IAverageSystem.CorrectionCalculator(Record UsersGrades, decimal starterValue, decimal endValue, decimal bestGradeInTheSystem)
    {
        var gradesList = UsersGrades.UsersGrades;

        decimal sumOfGrades = gradesList.Sum();
        
        int numberOfBestGrades = 0;

        while(starterValue < endValue)
        {
            sumOfGrades += bestGradeInTheSystem;
            starterValue = sumOfGrades / (gradesList.Count + numberOfBestGrades);
            numberOfBestGrades++;
        }

        return numberOfBestGrades;
        
    }

    string IAverageSystem.EnglishAverageSystem(Record UsersGrades, Record UsersAverages)
    {
        IAverageSystem calc = this;

        decimal average = calc.Calculator(UsersGrades, UsersAverages)*100;

        string DisplayAverage = average switch
        {
            >= 90 and <= 100 => $"{average:P2} A* - Congrats! ;)",
            >= 80 and < 90 => $"{average:P2} A - Well Done! If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 90, 100)} A* to reach A*",
            >= 70 and < 80 => $"{average:P2} B - Good! If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 80, 100)} more grades of A* to reach A",
            >= 60 and < 70 => $"{average:P2} C - Average. If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 70, 100)} more grades of A* to reach B",
            >= 50 and < 60 => $"{average:P2} D - Pass. If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 60, 100)} more grades of A* to reach C",
            >= 40 and < 50 => $"{average:P2} E - Low Pass! Next time study harder! If you want to raise your average, you only need {calc.CorrectionCalculator(UsersGrades, average, 50, 100)} more grades of A* to reach D",
            < 40 => $"{average:P2} F - Uh-oh! You are in trouble, but don't give up! You can still fix this." +
            $"You only need {calc.CorrectionCalculator(UsersGrades, average, 40, 100)} more grades of A* and you'll be safe :)",
            _ => string.Empty
        };

        return DisplayAverage;
    }

    string IAverageSystem.HungarianAverageSystem(Record UsersGrades, Record UsersAverages)
    {
        IAverageSystem calc = this;

        decimal average = calc.Calculator(UsersGrades, UsersAverages);

        string DisplayAverage = average switch
        {
            >= 4.5m and <= 5 => $"{Math.Round(average)}. Congrats! ;)",
            >= 4 and < 4.5m => $"{Math.Round(average)}. Well done! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 4.5m, 5)} 5s to bring your average up to 4.5",
            >= 3 and < 3.5m => $"{Math.Round(average)}. Good! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 3.5m, 5)} 5s to bring your average up to 3.5",
            >= 2 and < 2.5m => $"{Math.Round(average)}. Next time study harder! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 2.5m, 5)} 5s to bring your average up to 2.5",
            >= 1 and < 1.5m => $"{Math.Round(average)}. Uh-oh! You are in trouble, but don't give up! You can still fix this. If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 1.5m, 5)} 5s, and you'll be safe :)",
            _ => string.Empty
        };

        return DisplayAverage;
        
    }

    string IAverageSystem.RomanianAverageSystem(Record UsersGrades, Record UsersAverages)
    {
        IAverageSystem calc = this;

        decimal average = calc.Calculator(UsersGrades, UsersAverages);

        string DisplayAverage = average switch
        {
            >= 9.5M and <= 10 => $"{Math.Round(average)}. Congrats! ;)",
            >= 9 and < 9.5m => $"{Math.Round(average)}. Well done! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 9.5m, 10)} 5s to bring your average up 9.5",
            >= 8 and < 8.5m => $"{Math.Round(average)}. If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 8.5m, 10)} 5s to bring your average up 8.5",
            >= 7 and <= 7.5m => $"{Math.Round(average)}. If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 7.5m, 10)} 5s to bring your average up 7.5",
            >= 6 and < 6.5m => $"{Math.Round(average)}. If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 6.5m, 10)} 5s to bring your average up 6.5",
            >= 5 and < 5.5m => $"{Math.Round(average)}. If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 5.5m, 10)} 5s to bring your average up 5.5",
            >= 4 and < 4.5m => $"{Math.Round(average)}. Uh-oh, you are in trouble, but don't give up! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 4.5m, 10)} 5s to bring your average up 4.5",
            >= 3 and < 3.5m => $"{Math.Round(average)}. Uh-oh, you are in trouble, but don't give up! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 3.5m, 10)} 5s to bring your average up 3.5",
            >= 2 and < 2.5m => $"{Math.Round(average)}. Uh-oh, you are in trouble, but don't give up! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 2.5m, 10)} 5s to bring your average up 2.5",
            >= 1 and < 1.5m => $"{Math.Round(average)}. Uh-oh, you are in trouble, but don't give up! If you want to raise your average you only need {calc.CorrectionCalculator(UsersGrades, average, 1.5m, 10)} 5s to bring your average up 1.5",
            _ => string.Empty
        };

        return DisplayAverage;
        
    }

    string IAverageSystem.UsedGradeSystem(Record AverageDisplay, Record SelectedAverageSystem)
    {
        IAverageSystem calc = this;

        var AverageToDisplay = AverageDisplay.AverageDisplay;
        var SelectedSystem = SelectedAverageSystem.AverageDisplay;

        AverageToDisplay = SelectedSystem switch
        {
            "English" => calc.EnglishAverageSystem(AverageDisplay, SelectedAverageSystem),
            "Hungarian" => calc.HungarianAverageSystem(AverageDisplay, SelectedAverageSystem),
            "Romanian" => calc.RomanianAverageSystem(AverageDisplay, SelectedAverageSystem),
            _ => string.Empty
        };

        return AverageToDisplay;
    }

}