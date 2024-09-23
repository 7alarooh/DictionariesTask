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
                        addNewCourse();
                        break;
                    case "2":
                        Console.Clear();
                        removeCourse();
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
                        ExitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Sorry your choice was wrong !!");
                        break;
                }
                Console.Clear();
            } while (ExitFlag != true);
        }
        static void addNewCourse()
        {
            bool continueAdding = true;
            while (continueAdding)
            {   
                Console.Clear() ;
                Console.WriteLine(":::::: Add New Course ::::::");
                DisplayCourses();
                Console.WriteLine("Enter the course code (e.g., CS101):");
                string courseCode = Console.ReadLine().ToUpper();
                // Error handling for empty or null course code
                if (string.IsNullOrWhiteSpace(courseCode))
                {
                    Console.WriteLine("Error: Course code cannot be empty.");
                    return;
                }
                try
                {
                    // Check if the course code already exists
                    if (courses.ContainsKey(courseCode))
                    {
                        Console.WriteLine($"Error: Course '{courseCode}' already exists.");
                    }
                    else
                    {
                        // Add the new course code to the dictionary
                        courses[courseCode] = new HashSet<string>();
                        Console.WriteLine($"Course '{courseCode}' has been added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    // Catch any unexpected errors
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                // Ask if the user wants to add another course or return to the main menu
                Console.WriteLine("Do you want to add another course? (yes/no)");
                string input = Console.ReadLine()?.ToLower();

                if (input != "yes")
                {
                    continueAdding = false;
                    Console.Clear();
                }

            }
        }
        static void DisplayCourses()
        {
            Console.WriteLine("\n--- All Courses ---");
            if (courses.Count == 0)
            {
                Console.WriteLine("No courses available.");
            }
            else
            {
                foreach (var course in courses)
                {
                    Console.WriteLine($"Course: {course.Key}, Students Enrolled: {course.Value.Count}");
                }
            }
        }
        static void removeCourse()
        {
            bool continueremoving = true;
            while (continueremoving)
            {
                Console.WriteLine(":::::: Remove Course ::::::");
                DisplayCourses(); // Show current courses

                Console.WriteLine("Enter the course code to remove (e.g., CS101):");
                string courseCode = Console.ReadLine().ToUpper();

                // Error handling for empty or null course code
                if (string.IsNullOrWhiteSpace(courseCode))
                {
                    Console.WriteLine("Error: Course code cannot be empty.");
                    return;
                }
                try
                {
                    // Check if the course code exists
                    if (!courses.ContainsKey(courseCode))
                    {
                        Console.WriteLine($"Error: Course '{courseCode}' does not exist.");
                    }
                    else
                    {
                        // Check if there are no enrolled students
                        if (courses[courseCode].Count == 0)
                        {
                            // Remove the course
                            courses.Remove(courseCode);
                            Console.WriteLine($"Course '{courseCode}' has been removed successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Error: Course '{courseCode}' cannot be removed because it has enrolled students.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Catch any unexpected errors
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                // Ask if the user wants to remove another course or return to the main menu
                Console.WriteLine("Do you want to remove another course? (yes/no)");
                string input = Console.ReadLine()?.ToLower();

                if (input != "yes")
                {
                    continueremoving = false;
                    Console.Clear();
                }
            }
        }

    }
}
