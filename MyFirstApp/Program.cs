// See https://aka.ms/new-console-template for more information

// Literal value is a constant value that never changes

/*
Floating points integers

    Float Type    Precision
    ----------------------------
    float         ~6-9 digits (add F to end of number - called a literal suffix)
    double        ~15-17 digits (don't need to append anything t the end)
    decimal        28-29 digits (append m or M to the end)
*/

/*
Variable Name Rules

    - Can contain alphanumeric characters / underscore, no special characters
    - Must begin with underscore or alphabetical letter.
    - Case sensitive (i.e. string Value; and string value; are different).
    - Must not be a C# keyword.

Variable Name Conventions
    - Use camel case (string thisIsCamelCase)
    - better to start with alphabetical letter, underscore is used by developers for different purposes
    - descriptive and meaningful
    - 1+ words appended together, don't use contractions or abbreviations
    - dont include the data type of the variable
*/

/*
    Combine strings using string interpolation:
    - best not to use intermediate variables and just to print in the WriteLine or Write functions
*/

/*
Perform Math Operations


*/

using System;

class Program
{
    static void stuff() {
        var name = "Eyal";
        Console.WriteLine($"Hello from my first C# app - {name.ToUpper()}!");
        Console.WriteLine($"The current time is {DateTime.Now}");

        Console.WriteLine('b');
        Console.WriteLine(123);
        Console.WriteLine(0.25F);
        Console.WriteLine(2.625);
        Console.WriteLine(12.39816m);
        Console.WriteLine(true);
        Console.WriteLine(false);

        string firstName;
        firstName = "Bob";
        Console.WriteLine(firstName);

        // Implicitly applies the datatype, good for developing applications when you are unsure what the datatype will be
        var lastName = "Doiye";
        Console.WriteLine(lastName);

        int numberMessage = 3;
        float temperature = 34.4F;

        Console.Write($"Hello, {firstName}! You have {numberMessage} messaged in your inbox. The temperature is {temperature} celsius.");

        // Special Characters
        Console.WriteLine("Hello\nWorld!");
        Console.WriteLine("Hello\tWorld!");
        Console.WriteLine("Hello \"World\"!");
        Console.WriteLine("c:\\source\\repos");

        Console.WriteLine("Generating invoices for customer \"Contoso Corp\" ...\n");
        Console.WriteLine("Invoice: 1021\t\tComplete!");
        Console.WriteLine("Invoice: 1022\t\tComplete!");
        Console.WriteLine("\nOutput Directory:\t");
        Console.Write(@"c:\invoices");

        // To generate Japanese invoices:
        // Nihon no seikyū-sho o seisei suru ni wa:
        Console.Write("\n\n\u65e5\u672c\u306e\u8acb\u6c42\u66f8\u3092\u751f\u6210\u3059\u308b\u306b\u306f\uff1a\n\t");
        // User command to run an application
        Console.WriteLine(@"c:\invoices\app.exe -j");
    }

    static void temp() {
        int fahrenheit = 94;
        double celsius = ((double)fahrenheit - 32) * 5 / 9;
        Console.WriteLine($"The temperature is {celsius:F2} degrees Celsius.");
        
    }

    static void calculateAndPrintStudentGrades() {
       // initialize variables - graded assignments 
        int currentAssignments = 5;

        int sophia1 = 93;
        int sophia2 = 87;
        int sophia3 = 98;
        int sophia4 = 95;
        int sophia5 = 100;

        int nicolas1 = 80;
        int nicolas2 = 83;
        int nicolas3 = 82;
        int nicolas4 = 88;
        int nicolas5 = 85;

        int zahirah1 = 84;
        int zahirah2 = 96;
        int zahirah3 = 73;
        int zahirah4 = 85;
        int zahirah5 = 79;

        int jeong1 = 90;
        int jeong2 = 92;
        int jeong3 = 98;
        int jeong4 = 100;
        int jeong5 = 97;

        double averageSophia = (sophia1 + sophia2 + sophia3 + sophia4 + sophia5)/(double)currentAssignments;
        double averageNicolas = (nicolas1 + nicolas2 + nicolas3 + nicolas4 + nicolas5)/(double)currentAssignments;
        double averageZahirah = (zahirah1 + zahirah2 + zahirah3 + zahirah4 + zahirah5)/(double)currentAssignments;
        double averageJeong = (jeong1 + jeong2 + jeong3 + jeong4 + jeong5)/(double)currentAssignments;
        
        Console.WriteLine($@"
        Student   Grade
        Sophia    {averageSophia}  A
        Nicolas   {averageNicolas}  B
        Zahirah   {averageZahirah}  B
        Jeong     {averageJeong}  A
        ");
    }

    static void calculateFinalGPA() {
        string studentName = "Sophia Johnson";
        string course1Name = "English 101";
        string course2Name = "Algebra 101";
        string course3Name = "Biology 101";
        string course4Name = "Computer Science I";
        string course5Name = "Psychology 101";

        int course1Credit = 3;
        int course2Credit = 3;
        int course3Credit = 4;
        int course4Credit = 4;
        int course5Credit = 3;

        int gradeA = 4;
        int gradeB = 3;

        int course1Grade = gradeA;
        int course2Grade = gradeB;
        int course3Grade = gradeB;
        int course4Grade = gradeB;
        int course5Grade = gradeA;

        int totalCreditHours = 0;
        totalCreditHours += course1Credit;
        totalCreditHours += course2Credit;
        totalCreditHours += course3Credit;
        totalCreditHours += course4Credit;
        totalCreditHours += course5Credit;

        int totalGradePoints = 0;
        totalGradePoints += course1Credit * course1Grade;
        totalGradePoints += course2Credit * course2Grade;
        totalGradePoints += course3Credit * course3Grade;
        totalGradePoints += course4Credit * course4Grade;
        totalGradePoints += course5Credit * course5Grade;

        decimal gradePointAverage = (decimal) totalGradePoints/totalCreditHours;
        int leadingDigit = (int) gradePointAverage;
        int firstDigit = (int) (gradePointAverage * 10) % 10;
        int secondDigit = (int) (gradePointAverage * 100 ) % 10;

        Console.WriteLine($"Student: {studentName}\n");
        Console.WriteLine("Course\t\t\t\tGrade\tCredit Hours");

        Console.WriteLine($"{course1Name}\t\t\t{course1Grade}\t\t{course1Credit}");
        Console.WriteLine($"{course2Name}\t\t\t{course2Grade}\t\t{course2Credit}");
        Console.WriteLine($"{course3Name}\t\t\t{course3Grade}\t\t{course3Credit}");
        Console.WriteLine($"{course4Name}\t\t{course4Grade}\t\t{course4Credit}");
        Console.WriteLine($"{course5Name}\t\t\t{course5Grade}\t\t{course5Credit}");

        Console.WriteLine($"\nFinal GPA:\t\t\t {leadingDigit}.{firstDigit}{secondDigit}");
    }

    static void Main(string[] args)
    {
        // stuff();
        // temp();
        // calculateAndPrintStudentGrades();
        calculateFinalGPA();


    }
}

