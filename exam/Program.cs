namespace exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Subject subject = new Subject(1, "OOP");

            Console.Write("Choose Exam Type (1 = Final, 2 = Practical): ");
            int type = int.Parse(Console.ReadLine() ?? "1");

            Console.Write("Enter Exam Time (minutes): ");
            int time = int.Parse(Console.ReadLine() ?? "30");

            Console.Write("Enter Number of Questions: ");
            int count = int.Parse(Console.ReadLine() ?? "1");

            subject.CreateExam(type, time, count);
            Exam exam = subject.SubjectExam!;

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n--- Question {i + 1} ---");

                int qType = 2; // MCQ
                if (exam is FinalExam)
                {
                    Console.Write("Type (1 = True/False, 2 = MCQ): ");
                    qType = int.Parse(Console.ReadLine() ?? "2");
                }

                Console.Write("Header: ");
                string header = Console.ReadLine() ?? "Q";

                Console.Write("Body: ");
                string body = Console.ReadLine() ?? "";

                Console.Write("Mark: ");
                int mark = int.Parse(Console.ReadLine() ?? "1");

                if (qType == 1 && exam is FinalExam)
                {
                    Console.Write("Right Answer (1 = True, 2 = False): ");
                    int rightId = int.Parse(Console.ReadLine() ?? "1");
                    Answer rightAns = rightId == 1 ? new Answer(1, "True") : new Answer(2, "False");
                    exam.Questions[i] = new TrueFalseQuestion(header, body, mark, rightAns);
                }
                else
                {
                    Console.Write("Number of Choices: ");
                    int choicesCount = int.Parse(Console.ReadLine() ?? "2");
                    Answer[] answers = new Answer[choicesCount];

                    for (int j = 0; j < choicesCount; j++)
                    {
                        Console.Write($"Choice {j + 1} text: ");
                        string text = Console.ReadLine() ?? "";
                        answers[j] = new Answer(j + 1, text);
                    }

                    Console.Write("Right Answer ID: ");
                    int rightId = int.Parse(Console.ReadLine() ?? "1");
                    Answer rightAns = answers[rightId - 1];

                    exam.Questions[i] = new McqQuestion(header, body, mark, answers, rightAns);
                }
            }

            Console.WriteLine("\n" + subject);
            exam.ShowExam();
        }
    }
    }

