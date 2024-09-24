namespace courseEnrollmentSystem
{
    internal class Program
    {
        static Dictionary<string, HashSet<string>> courses = new Dictionary<string, HashSet<string>>();// Dictionary to store courses and enrolled students
        static List<(string studentName, string courseCode)> WaitList = new List<(string, string)>();// Waitlist to store students who are waiting to enroll in a full course
        static Dictionary<string, int> courseCapacities = new Dictionary<string, int>(); // Dictionary to store course capacities




        static void Main(string[] args)
        {
            InitializeStartupData();
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
                Console.WriteLine("\n 9 .View Waiting List");
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
                        findCoursesWithCommonStudents();
                        break;
                    case "8":
                        Console.Clear();
                        withdrawStudentFromAllCourses();
                        break;
                    case "9":
                        Console.Clear();
                        viewWaitingList();
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
                    string courseCode = GetInput("Enter the course code (e.g., CS101) (or type 'exit' to cancel):").ToUpper();
                    ;
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

                        int capacity;
                        while (true) // Loop until a valid capacity is entered
                        {
                            if (!int.TryParse(Console.ReadLine(), out capacity) || capacity <= 0)
                            {
                                Console.WriteLine("Error: Please enter a valid positive capacity.");
                                Console.WriteLine("Enter the course capacity:");
                            }
                            else
                            {
                                break; // Exit the loop if a valid capacity is entered
                            }
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
                Console.Clear();
                Console.WriteLine(":::::: Remove Course ::::::");
                DisplayCourses(); // Show current courses


                string courseCode = GetInput("Enter the course code (e.g., CS101) (or type 'exit' to cancel):").ToUpper();

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
            bool continueEnrollStudent = true;
            while (continueEnrollStudent)
            {
                try
                {
                    Console.WriteLine(":::::: Enroll Student in Course ::::::");
                    DisplayCourses(); // Display available courses

                    // Check if there are any courses available
                    if (courses.Count == 0)
                    {
                        Console.WriteLine("No courses are available at the moment.");
                        return; // End function if no courses are available
                    }

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

                    // Display the courses the student is already enrolled in
                    Console.WriteLine($"Courses that '{studentName}' is currently enrolled in:");
                    bool studentEnrolledInAnyCourse = false;
                    foreach (var course in courses)
                    {
                        if (course.Value.Contains(studentName))
                        {
                            Console.WriteLine($"Course: {course.Key}");
                            studentEnrolledInAnyCourse = true;
                        }
                    }

                    if (!studentEnrolledInAnyCourse)
                    {
                        Console.WriteLine($"{studentName} is not enrolled in any course.");
                    }

                    string courseCode;
                    // Get the course code with error handling
                    while (true)
                    {
                        Console.WriteLine("Enter the course code to enroll the student (e.g., CS101):");
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
                        Console.WriteLine($"Student '{studentName}' is already enrolled in course '{courseCode}'.");

                        // Ask the user if they want to remove the student from the course
                        Console.WriteLine("Do you want to remove this student from the course? (y/n)");
                        string removeInput = Console.ReadLine()?.ToLower();

                        if (removeInput == "y")
                        {
                            courses[courseCode].Remove(studentName);
                            Console.WriteLine($"Student '{studentName}' has been removed from course '{courseCode}' successfully.");
                        }
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

                    // Ask if the user wants to continue enrolling students
                    Console.WriteLine("Do you want to continue enrolling students in courses? (y/n)");
                    string input = Console.ReadLine()?.ToLower();

                    if (input != "y")
                    {
                        continueEnrollStudent = false;
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
                    string studentName;
                    while (true)
                    {
                        Console.WriteLine("Enter the student's name:");
                        studentName = GetInput("Enter the student's name (or type 'exit' to cancel):");
                        if (!string.IsNullOrWhiteSpace(studentName))
                        {
                            break; // Valid input, exit the loop
                        }

                        Console.WriteLine("Error: Student name cannot be empty. Please try again.");
                    }

                    // Get and validate the course code
                    string courseCode;
                    // Get the course code with error handling
                    while (true)
                    {
                        Console.WriteLine("Enter the course code to enroll the student (e.g., CS101):");
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



                    // Check if the student is enrolled in the course
                    if (!courses[courseCode].Contains(studentName))
                    {
                        Console.WriteLine($"Error: Student '{studentName}' is not enrolled in course '{courseCode}'. Please try again.");
                        continue; // Loop back to retry input
                    }

                    
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

                    // Ask if the user wants to continue removing student or return to the main menu
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

                        courseCode = GetInput("Enter the course code (e.g., CS101) (or type 'exit' to cancel):")?.ToUpper(); // Convert to uppercase

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
                DisplayCourses();
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
                string keyOut=Console.ReadLine();
                Console.Clear();
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static void findCoursesWithCommonStudents()
        {
            bool continueFind = true;
            while (continueFind)
            {
                try
                {
                    Console.WriteLine(":::::: Find Common Students Between Two Courses ::::::");

                    // Get the first course code from the user
                    string firstCourseCode = GetInput("Enter the course code (e.g., CS101) (or type 'exit' to cancel):").ToUpper();

                    // Error handling for empty or null course code
                    if (string.IsNullOrWhiteSpace(firstCourseCode))
                    {
                        Console.WriteLine("Error: Course code cannot be empty.");
                        return;
                    }

                    // Get the second course code from the user
                    Console.WriteLine("Enter the second course code (e.g., MATH202):");
                    string secondCourseCode = Console.ReadLine().ToUpper();

                    // Error handling for empty or null course code
                    if (string.IsNullOrWhiteSpace(secondCourseCode))
                    {
                        Console.WriteLine("Error: Course code cannot be empty.");
                        return;
                    }

                    // Check if both courses exist in the dictionary
                    if (!courses.ContainsKey(firstCourseCode))
                    {
                        Console.WriteLine($"Error: Course '{firstCourseCode}' does not exist.");
                        return;
                    }
                    if (!courses.ContainsKey(secondCourseCode))
                    {
                        Console.WriteLine($"Error: Course '{secondCourseCode}' does not exist.");
                        return;
                    }

                    // Get the students from both courses
                    HashSet<string> firstCourseStudents = courses[firstCourseCode];
                    HashSet<string> secondCourseStudents = courses[secondCourseCode];

                    // Find the intersection of both sets (common students)
                    HashSet<string> commonStudents = new HashSet<string>(firstCourseStudents);
                    commonStudents.IntersectWith(secondCourseStudents);

                    // Display the common students
                    if (commonStudents.Count > 0)
                    {
                        Console.WriteLine("Common students between the two courses:");
                        foreach (var student in commonStudents)
                        {
                            Console.WriteLine($"  - {student}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No students are enrolled in both courses.");
                    }
                    // Ask if the user wants to find Courses With Common Students  or return to the main menu
                    Console.WriteLine("Do you want to continue ? (y/n)");
                    string input = Console.ReadLine()?.ToLower();

                    if (input != "y")
                    {
                        continueFind = false;
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
        static void withdrawStudentFromAllCourses()
        {
            bool continueWithdraw = true;
            while (continueWithdraw)
            {
                try
                {
                    Console.WriteLine(":::::: Withdraw Student from All Courses ::::::");

                    // Get the student's name
                    string studentName = GetInput("Enter the student's name (or type 'exit' to cancel):");

                    // Error handling for empty or null student name
                    if (string.IsNullOrWhiteSpace(studentName))
                    {
                        Console.WriteLine("Error: Student name cannot be empty.");
                        return;
                    }

                    // Flag to check if the student was found in any course
                    bool studentFound = false;
                    List<string> coursesToWithdraw = new List<string>(); // To store courses from which student will be withdrawn

                    // Iterate over all courses and check if the student is enrolled
                    foreach (var course in courses)
                    {
                        if (course.Value.Contains(studentName)) // Check if the student is enrolled in the course
                        {
                            coursesToWithdraw.Add(course.Key); // Add course to the list
                            studentFound = true;
                        }
                    }

                    if (!studentFound)
                    {
                        Console.WriteLine($"Student '{studentName}' is not enrolled in any course.");
                    }
                    else
                    {
                        // Show the user all courses the student is enrolled in and ask for confirmation
                        Console.WriteLine($"Student '{studentName}' is currently enrolled in the following courses:");
                        foreach (var course in coursesToWithdraw)
                        {
                            Console.WriteLine(course);
                        }

                        // Ask for confirmation
                        Console.WriteLine("Are you sure you want to withdraw the student from all of these courses? (y/n)");
                        string confirm = Console.ReadLine()?.ToLower();

                        if (confirm == "y")
                        {
                            // If confirmed, remove the student from all courses
                            foreach (var courseCode in coursesToWithdraw)
                            {
                                courses[courseCode].Remove(studentName);
                                Console.WriteLine($"Student '{studentName}' has been withdrawn from course '{courseCode}'.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Operation canceled. The student has not been withdrawn from any courses.");
                        }
                    }

                    // Ask if the user wants to continue withdrawing students or return to the main menu
                    Console.WriteLine("Do you want to continue withdrawing students? (y/n)");
                    string input = Console.ReadLine()?.ToLower();

                    if (input != "y")
                    {
                        continueWithdraw = false;
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
        static void InitializeStartupData()
        {
            // Example data: Courses and their enrolled students (cross-over students)
            courses["CS101"] = new HashSet<string> { "Alice", "Bob", "Charlie" };   // CS101 has Alice, Bob, Charlie
            courses["MATH202"] = new HashSet<string> { "David", "Eva", "Bob" };     // MATH202 has David, Eva, and Bob (cross-over with CS101)
            courses["ENG303"] = new HashSet<string> { "Frank", "Grace", "Charlie" };// ENG303 has Frank, Grace, and Charlie (cross-over with CS101)
            courses["BIO404"] = new HashSet<string> { "Ivy", "Jack", "David" };     // BIO404 has Ivy, Jack, and David (cross-over with MATH202)

            // Set course capacities (varying)
            courseCapacities["CS101"] = 3;  // CS101 capacity of 3 (currently full)
            courseCapacities["MATH202"] = 5; // MATH202 capacity of 5 (can accept more students)
            courseCapacities["ENG303"] = 3;  // ENG303 capacity of 3 (currently full)
            courseCapacities["BIO404"] = 4;  // BIO404 capacity of 4 (can accept more students)

            // Waitlist for courses (students waiting to enroll in full courses)
            WaitList.Add(("Helen", "CS101"));   // Helen waiting for CS101
            WaitList.Add(("Jack", "ENG303"));   // Jack waiting for ENG303
            WaitList.Add(("Alice", "BIO404"));  // Alice waiting for BIO404
            WaitList.Add(("Eva", "ENG303"));    // Eva waiting for ENG303

            Console.WriteLine("Startup data initialized.");
        }
        static void viewWaitingList()
        {
            // Check if the waitlist is empty
            if (WaitList.Count == 0)
            {
                Console.WriteLine("There are no students on the waitlist.");
                return;
            }

            // Group students by courseCode
            var groupedWaitList = WaitList
                .GroupBy(w => w.courseCode)
                .ToDictionary(g => g.Key, g => g.Select(w => w.studentName).ToList());

            Console.WriteLine(":::::: Waitlist for Courses ::::::");

            // Iterate through each course's waitlist
            foreach (var entry in groupedWaitList)
            {
                string courseCode = entry.Key;
                List<string> students = entry.Value;

                Console.WriteLine($"\nCourse: {courseCode}");
                if (students.Count > 0)
                {
                    Console.WriteLine("  Students on waitlist:");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"  - {student}");
                    }
                }
                else
                {
                    Console.WriteLine("  No students on the waitlist.");
                }
            }
        }
        //Utility function for getting user input with exit option
        static string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            // Check if the user wants to exit
            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Exiting the current operation...");
                return null; // Return null if "exit" is entered
            }

            return input; // Return the valid input
        }




    }
}
