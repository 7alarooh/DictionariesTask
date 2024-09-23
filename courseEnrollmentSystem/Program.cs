namespace courseEnrollmentSystem
{
    internal class Program
    {
        static Dictionary<string, HashSet<string>> courses = new Dictionary<string, HashSet<string>>();// Dictionary to store courses and enrolled students
        static List<(string studentName, string courseCode)> WaitList = new List<(string, string)>();// Waitlist to store students who are waiting to enroll in a full course

        static void Main(string[] args)
        {
            mainMenu();
        }

        static void mainMenu() 
        {
            bool ExitFlag = false;
            do
            {
                Console.WriteLine("Welcome Course Enrollment System");
                Console.WriteLine("\n Enter the No. of operation you need :");
                Console.WriteLine("\n 1 .Add a new course");
                Console.WriteLine("\n 2 .Remove Course");
                Console.WriteLine("\n 3 .Enroll a student in a course");
                Console.WriteLine("\n 4 .Remove a student from a course");
                Console.WriteLine("\n 5 .Display all students in a course");
                Console.WriteLine("\n 6 .Display all courses and their students");
                Console.WriteLine("\n 7 .Find courses with common students");
                Console.WriteLine("\n 8 .Withdraw a Student from All Courses");
                Console.WriteLine("\n 0 .singOut");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        //addNewCourse();
                        break;
                    case "2":
                        Console.Clear();
                        //removeCourse();
                        break;
                    case "3":
                        Console.Clear();
                        //EnrollStudentInCourse();
                        break;
                    case "4":
                        Console.Clear();
                        //removeStudentFromCourse();
                        break;
                    case "5":
                        Console.Clear();
                        //displayAllStudentsInCourse();
                        break;
                    case "6":
                        Console.Clear();
                        //displayAllCoursesAndTheirStudents();
                        break;
                    case "7":
                        Console.Clear();
                        //FindCoursesWithCommonStudents();
                        break;
                    case "8":
                        Console.Clear();
                        //withdrawStudentFromAllCourses();
                        break;
                    case "0":
                        Console.WriteLine("\npress Enter key to exit out system");
                        string outsystem = Console.ReadLine();
                        ExitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Sorry your choice was wrong !!");
                        break;
                }
                Console.WriteLine("press Enter key to continue");
                string cont = Console.ReadLine();
                Console.Clear();
            } while (ExitFlag != true);
        }
    }
}
