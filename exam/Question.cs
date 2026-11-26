using System;
using System.Collections.Generic;
using System.Text;

namespace exam
{
    public abstract class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public Answer[] Answers { get; set; }
        public Answer RightAnswer { get; set; }

        protected Question() : this("question", "", 1, Array.Empty<Answer>(), new Answer()) { }

        protected Question(string header, string body, int mark, Answer[] answers, Answer rightAnswer)
        {
            Header = header;
            Body = body;
            Mark = mark;
            Answers = answers;
            RightAnswer = rightAnswer;
        }

        public abstract object Clone();

        public int CompareTo(Question? other)
        {
            if (other == null) return 1;
            return Mark.CompareTo(other.Mark);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Header} ({Mark} Marks)");
            sb.AppendLine(Body);
            foreach (Answer ans in Answers)
            {
                sb.AppendLine($"  {ans}");
            }
            return sb.ToString();
        }
    }

    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion() : this("True / False", "", 1, new Answer(1, "True")) { }

        public TrueFalseQuestion(string header, string body, int mark, Answer rightAnswer)
            : base(header, body, mark, new Answer[] { new Answer(1, "True"), new Answer(2, "False") }, rightAnswer)
        { }

        public override object Clone()
        {
            Answer[] clonedAnswers = new Answer[Answers.Length];
            for (int i = 0; i < Answers.Length; i++)
                clonedAnswers[i] = (Answer)Answers[i].Clone();

            return new TrueFalseQuestion(Header, Body, Mark, (Answer)RightAnswer.Clone())
            {
                Answers = clonedAnswers
            };
        }
    }

    public class McqQuestion : Question
    {
        public McqQuestion() : base() { }

        public McqQuestion(string header, string body, int mark, Answer[] answers, Answer rightAnswer)
            : base(header, body, mark, answers, rightAnswer)
        { }

        public override object Clone()
        {
            Answer[] clonedAnswers = new Answer[Answers.Length];
            for (int i = 0; i < Answers.Length; i++)
                clonedAnswers[i] = (Answer)Answers[i].Clone();

            return new McqQuestion(Header, Body, Mark, clonedAnswers, (Answer)RightAnswer.Clone());
        }
    }
}
