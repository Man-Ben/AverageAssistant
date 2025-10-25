/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This file contains the commonly used variables (records).
----------------------- */

namespace AverageAssistant.Models;

public record Record
{
    //This list holds all the grades entered by the user. 
    public List<int> UsersGrades { get; set; } = new();

    //This variable holds the number of lessons entered by the user (only in Romanian system).
    public int NumberOfLessons;

    //This variable stores the grade entered by the user.
    public string Grade { get; set; } = string.Empty;

    //This variable keeps the subject's name entered by the user.
    public string SubjectName { get; set; } = string.Empty;

    //This variable holds the average system selected by the user.
    public string SelectedAverageSystem { get; set; } = string.Empty;

    //This variable contains the calculated and formatted average.
    public string? Average {get; set;}
}

