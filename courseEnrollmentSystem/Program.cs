namespace courseEnrollmentSystem
{
    internal class Program
    {
        static Dictionary<string, HashSet<string>> courses = new Dictionary<string, HashSet<string>>();// Dictionary to store courses and enrolled students
        static List<(string studentName, string courseCode)> WaitList = new List<(string, string)>();// Waitlist to store students who are waiting to enroll in a full course

        static void Main(string[] args)
        {

        }
    }
}
