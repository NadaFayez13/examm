using System;
using System.Collections.Generic;
using System.Text;

namespace exam
{
    public abstract class Exam : ICloneable, IComparable<Exam>
    {
        public int Time { get; set; } 
        public int NumberOfQuestions { get; set; }
        public Question[] Questions { get; set; }

        protected Exam() : this(30, 0) { }

        protected Exam(int time, int numberOfQuestions)
        {
            Time = time;
            NumberOfQuestions = numberOfQuestions;
            Questions = new Question[numberOfQuestions];
        }

        public abstract void ShowExam();

        public abstract object Clone();

        public int CompareTo(Exam? other)
        {
            if (other == null) return 1;
            return NumberOfQuestions.CompareTo(other.NumberOfQuestions);
        }

        public override string ToString()
        {
            return $"Exam Type: {GetType().Name} | time: {Time} min | questions: {NumberOfQuestions}";
        }
    }

    public class FinalExam : Exam
    {
        public FinalExam() : base() { }

        public FinalExam(int time, int numberOfQuestions) : base(time, numberOfQuestions) { }

        public override void ShowExam()
        {
            int[] userAnswers = new int[Questions.Length];
            int grade = 0;
            int totalMarks = 0;

            for (int i = 0; i < Questions.Length; i++)
            {
                Console.WriteLine($"\nQ{i + 1}: {Questions[i]}");
                Console.Write("your answer ID: ");
                int.TryParse(Console.ReadLine(), out userAnswers[i]);
            }

            Console.WriteLine("\n--- Final Exam Result ---");
            for (int i = 0; i < Questions.Length; i++)
            {
                totalMarks += Questions[i].Mark;
                if (userAnswers[i] == Questions[i].RightAnswer.AnswerId)
                    grade += Questions[i].Mark;

                Console.WriteLine($"Q{i + 1}: {Questions[i].Body}");
                Console.WriteLine($"   Your Answer ID: {userAnswers[i]} | Right Answer: {Questions[i].RightAnswer}");
            }
            Console.WriteLine($"Total Grade: {grade} / {totalMarks}");
        }

        public override object Clone()
        {
            FinalExam copy = new FinalExam(Time, NumberOfQuestions);
            for (int i = 0; i < Questions.Length; i++)
                if (Questions[i] != null) copy.Questions[i] = (Question)Questions[i].Clone();
            return copy;
        }
    }

    public class PracticalExam : Exam
    {
        public PracticalExam() : base() { }

        public PracticalExam(int time, int numberOfQuestions) : base(time, numberOfQuestions) { }

        public override void ShowExam()
        {
            int[] userAnswers = new int[Questions.Length];

            for (int i = 0; i < Questions.Length; i++)
            {
                Console.WriteLine($"\nQ{i + 1}: {Questions[i]}");
                Console.Write("Your Answer ID: ");
                int.TryParse(Console.ReadLine(), out userAnswers[i]);
            }

            Console.WriteLine("\n--- Practical Exam Right Answers ---");
            for (int i = 0; i < Questions.Length; i++)
            {
                Console.WriteLine($"Q{i + 1}: Right Answer -> {Questions[i].RightAnswer}");
            }
        }

        public override object Clone()
        {
            PracticalExam copy = new PracticalExam(Time, NumberOfQuestions);
            for (int i = 0; i < Questions.Length; i++)
                if (Questions[i] != null) copy.Questions[i] = (Question)Questions[i].Clone();
            return copy;
        }
    }
}
