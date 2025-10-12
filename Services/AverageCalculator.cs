using AverageAssistant.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace AverageAssistant.Services;

public class AverageCalculator : IAverageSystem
{
    decimal IAverageSystem.Calculator(Record UsersGrades, Record UsersAverages)
    {
        var gradesList = UsersGrades.UsersGrades;
        var AverageList = UsersAverages.UsersAverages;

        decimal sum = gradesList.Sum();
        decimal average = 0;

        if (gradesList.Count == 0)
            return average;
        
        average = sum / gradesList.Count;
        AverageList.Add(average);  
        return average;
           
    }

    string IAverageSystem.EnglishAverageSystem(Record UsersGrades, Record UsersAverages)
    {
        IAverageSystem calc = this;
        decimal average = calc.Calculator(UsersGrades, UsersAverages)*100;

        string DisplayAverage = average switch
        {
            >= 90 and <= 100 => $"{average}% A* - Congrats! ;)",
            >= 80 and <= 90 => $"{average}% A - Well Done!",
            >= 70 and <= 80 => $"{average}% B - Good!",
            >= 60 and <= 70 => $"{average}% C - Average",
            >= 50 and <= 60 => $"{average}% D - Pass",
            >= 40 and <= 50 => $"{average}% E - Low Pass! Next time study harder!",
            >= 30 and <= 40 => $"{average}% F - You are about to fail! But, don't give up! You can fix this! :)",
            _ => string.Empty
        };

        return DisplayAverage;
    }

    string IAverageSystem.HungarianAverageSystem(Record UsersGrades, Record UsersAverages)
    {
        
    }

    string IAverageSystem.RomanianAverageSystem(Record UsersGrades, Record UsersAverages)
    {
        
    }

    string IAverageSystem.UsedGradeSystem()
    {
        
    }

}