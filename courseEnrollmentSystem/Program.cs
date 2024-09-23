namespace courseEnrollmentSystem
{
    internal class Program
    {
        static Dictionary<string, HashSet<string>> courses = new Dictionary<string, HashSet<string>>();// Dictionary to store courses and enrolled students
        static List<(string studentName, string courseCode)> WaitList = new List<(string, string)>();// Waitlist to store students who are waiting to enroll in a full course
        static Dictionary<string, int> courseCapacities = new Dictionary<string, int>(); // Dictionary to store course capacities

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
                        EnrollStudentInCourse();
                        break;
                    case "4":
                        Console.Clear();
                        removeStudentFromCourse();
                        break;
                    case "5":
                        Console.Clear();
                        displayAllStudentsInCourse();
                        break;
                    case "6":
                        Console.Clear();
                        displayAllCoursesAndTheirStudents();
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

            } while (ExitFlag != true);
        }
        static void addNewCourse()
        {
            bool continueAdding = true;
            while (continueAdding)
            {   
                try
                {
                    Console.Clear();
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
                    // Check if the course code already exists
                    if (courses.ContainsKey(courseCode))
                    {
                        Console.WriteLine($"Error: Course '{courseCode}' already exists.");
                    }
                    else
                    {
                        Console.WriteLine("Enter the course capacity:");
                        if (!int.TryParse(Console.ReadLine(), out int capacity) || capacity <= 0)
                        {
                            Console.WriteLine("Error: Please enter a valid capacity.");
                            return;
                        }
                        // Ask for confirmation before adding the course
                        Console.WriteLine($"You are about to add the course '{courseCode}' with a capacity of {capacity}. Are you sure? (Y/N)");
                        string confirmation = Console.ReadLine().ToUpper();

                        // If confirmed, add the course
                        if (confirmation == "Y")
                        {
                            courses[courseCode] = new HashSet<string>();
                            courseCapacities[courseCode] = capacity; // Store the course capacity
                            Console.WriteLine($"Course '{courseCode}' has been added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Action cancelled. Course was not added.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Catch any unexpected errors
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                // Ask if the user wants to add another course or return to the main menu
                Console.WriteLine("Do you want to add another course? (y/n)");
                string input = Console.ReadLine()?.ToLower();

                if (input != "y")
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
                            // Ask the user if they are sure about removing the course
                            Console.WriteLine($"Are you sure you want to remove the course '{courseCode}'? (Y/N)");
                            string confirmation = Console.ReadLine().ToUpper();
                            if (confirmation == "Y")
                            {
                                // Remove the course
                                courses.Remove(courseCode);
                                courseCapacities.Remove(courseCode); // Remove capacity data as well if applicable
                                Console.WriteLine($"Course '{courseCode}' has been removed successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Action cancelled. Course was not removed.");
                            }
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
                Console.WriteLine("Do you want to remove another course? (y/n)");
                string input = Console.ReadLine()?.ToLower();

                if (input != "y")
                {
                    continueremoving = false;
                    Console.Clear();
                }
            }
        }
        static void EnrollStudentInCourse() 
        {
            bool continuereEnrollStuden = true;
            while (continuereEnrollStuden)
            {
                try
                {
                    Console.WriteLine(":::::: Enroll Student in Course ::::::");
                    DisplayCourses(); // Display available courses

                    string studentName;
                    // Get the student's name with error handling
                    while (true)
                    {
                        Console.WriteLine("Enter the student's name:");
                        studentName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(studentName))
                        {
                            Console.WriteLine("Error: Student name cannot be empty. Please try again.");
                        }
                        else
                        {
                            break; // Exit loop once valid name is entered
                        }
                    }

                    string courseCode;
                    // Get the course code with error handling
                    while (true)
                    {
                        Console.WriteLine("Enter the course code (e.g., CS101):");
                        courseCode = Console.ReadLine()?.ToUpper(); // Convert to uppercase

                        if (string.IsNullOrWhiteSpace(courseCode))
                        {
                            Console.WriteLine("Error: Course code cannot be empty. Please try again.");
                        }
                        else if (!courses.ContainsKey(courseCode))
                        {
                            Console.WriteLine($"Error: Course '{courseCode}' does not exist. Please try again.");
                        }
                        else
                        {
                            break; // Exit loop once valid course code is entered
                        }
                    }

                    // Check if the student is already enrolled in the course
                    if (courses[courseCode].Contains(studentName))
                    {
                        Console.WriteLine($"Error: Student '{studentName}' is already enrolled in course '{courseCode}'.");
                    }
                    else
                    {
                        // Check if the course has reached its capacity
                        if (courses[courseCode].Count >= courseCapacities[courseCode])
                        {
                            // Add the student to the waitlist if the course is full
                            WaitList.Add((studentName, courseCode));
                            Console.WriteLine($"Course '{courseCode}' is full. Student '{studentName}' has been added to the waitlist.");
                        }
                        else
                        {
                            // Enroll the student in the course
                            courses[courseCode].Add(studentName);
                            Console.WriteLine($"Student '{studentName}' has been enrolled in course '{courseCode}' successfully.");
                        }
                    }
                    // Ask if the user wants to remove another course or return to the main menu
                    Console.WriteLine("Do you want to continue ? (y/n)");
                    string input = Console.ReadLine()?.ToLower();

                    if (input != "y")
                    {
                        continuereEnrollStuden = false;
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    // Catch any unexpected errors
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

            }
        }
        static void removeStudentFromCourse()
        {
            bool continueRemoving = true;
            while (continueRemoving)
            {
                try
                {
                    Console.WriteLine(":::::: Remove Student from Course ::::::");
                    DisplayCourses(); // Display available courses

                    // Get and validate the student's name
                    string studentName = "";
                    while (true)
                    {
                        Console.WriteLine("Enter the student's name:");
                        studentName = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(studentName))
                        {
                            break; // Valid input, exit the loop
                        }

                        Console.WriteLine("Error: Student name cannot be empty. Please try again.");
                    }

                    // Get and validate the course code
                    string courseCode = "";
                    while (true)
                    {
                        Console.WriteLine("Enter the course code (e.g., CS101):");
                        courseCode = Console.ReadLine()?.ToUpper(); // Convert to uppercase

                        if (!string.IsNullOrWhiteSpace(courseCode))
                        {
                            break; // Valid input, exit the loop
                        }

                        Console.WriteLine("Error: Course code cannot be empty. Please try again.");
                    }

                    // Check if the course exists
                    if (!courses.ContainsKey(courseCode))
                    {
                        Console.WriteLine($"Error: Course '{courseCode}' does not exist. Please try again.");
                        continue; // Loop back to retry input
                    }

                    // Check if the student is enrolled in the course
                    if (!courses[courseCode].Contains(studentName))
                    {
                        Console.WriteLine($"Error: Student '{studentName}' is not enrolled in course '{courseCode}'. Please try again.");
                        continue; // Loop back to retry input
                    }

                    // Remove the student from the course
                    courses[courseCode].Remove(studentName);
                    Console.WriteLine($"Student '{studentName}' has been removed from course '{courseCode}' successfully.");

                    // Check if the course now has space for students on the waitlist
                    // Ask for confirmation before removing the student
                    Console.WriteLine($"Are you sure you want to remove student '{studentName}' from course '{courseCode}'? (y/no)");
                    string confirmation = Console.ReadLine()?.ToLower();

                    if (confirmation == "y")
                    {
                        // Remove the student from the course
                        courses[courseCode].Remove(studentName);
                        Console.WriteLine($"Student '{studentName}' has been removed from course '{courseCode}' successfully.");

                        // Check if the course now has space for students on the waitlist
                        var studentOnWaitlist = WaitList.FirstOrDefault(w => w.courseCode == courseCode);
                        if (studentOnWaitlist != default)
                        {
                            // Enroll the first student from the waitlist
                            courses[courseCode].Add(studentOnWaitlist.studentName);
                            WaitList.Remove(studentOnWaitlist);
                            Console.WriteLine($"Student '{studentOnWaitlist.studentName}' from the waitlist has been enrolled in course '{courseCode}'.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Student '{studentName}' was not removed from course '{courseCode}'.");
                    }

                    // Ask if the user wants to continue removing students or return to the main menu
                    Console.WriteLine("Do you want to continue removing students? (y/n)");
                    string input = Console.ReadLine()?.ToLower();

                    if (input != "y")
                    {
                        continueRemoving = false;
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    // Catch any unexpected errors
                    Console.WriteLine($"An error occurred: {ex.Message}. Please try again.");
                }
            }
        }
        static void displayAllStudentsInCourse() {
            bool continueDisplaying = true;
            while (continueDisplaying)
            {
                try
                {
                    Console.WriteLine(":::::: Display All Students in a Course ::::::");
                    DisplayCourses(); // Show available courses to choose from

                    // Get the course code from the user
                    string courseCode;
                    while (true)
                    {
                        Console.WriteLine("Enter the course code (e.g., CS101):");
                        courseCode = Console.ReadLine()?.ToUpper(); // Convert to uppercase

                        if (!string.IsNullOrWhiteSpace(courseCode))
                        {
                            break; // Valid input, exit the loop
                        }

                        Console.WriteLine("Error: Course code cannot be empty. Please try again.");
                    }

                    // Check if the course exists
                    if (!courses.ContainsKey(courseCode))
                    {
                        Console.WriteLine($"Error: Course '{courseCode}' does not exist. Please try again.");
                        continue; // Loop back to retry input
                    }

                    // Check if the course has any students enrolled
                    if (courses[courseCode].Count == 0)
                    {
                        Console.WriteLine($"Course '{courseCode}' has no students enrolled.");
                    }
                    else
                    {
                        Console.WriteLine($"\nStudents enrolled in course '{courseCode}':");
                        foreach (var student in courses[courseCode])
                        {
                            Console.WriteLine($"- {student}");
                        }
                    }

                    // Ask if the user wants to display students from another course or return to the main menu
                    Console.WriteLine("\nDo you want to display students for another course? (y/n)");
                    string input = Console.ReadLine()?.ToLower();

                    if (input != "y")
                    {
                        continueDisplaying = false;
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    // Catch any unexpected errors
                    Console.WriteLine($"An error occurred: {ex.Message}. Please try again.");
                }
            }
        }
        static void displayAllCoursesAndTheirStudents()
        {
            try
            {
                Console.WriteLine(":::::: Display All Courses and Their Students ::::::");

                // Check if there are any courses in the system
                if (courses.Count == 0)
                {
                    Console.WriteLine("No courses available.");
                    return;
                }

                // Iterate through each course in the dictionary
                foreach (var course in courses)
                {
                    string courseCode = course.Key;
                    var students = course.Value;

                    // Display the course code
                    Console.WriteLine($"\nCourse: {courseCode}");

                    // Check if the course has any students enrolled
                    if (students.Count == 0)
                    {
                        Console.WriteLine("  No students enrolled.");
                    }
                    else
                    {
                        Console.WriteLine("  Enrolled Students:");
                        foreach (var student in students)
                        {
                            Console.WriteLine($"  - {student}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


    }
}
