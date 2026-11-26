using System;
using System.Collections.Generic;
using System.Text;

namespace exam
{
    public class Subject : ICloneable, IComparable<Subject>
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam? SubjectExam { get; set; }

        public Subject() : this(0, "") { }

        public Subject(int id, string name, Exam? subjectExam = null)
        {
            SubjectId = id;
            SubjectName = name;
            SubjectExam = subjectExam;
        }

        public void CreateExam(int type, int time, int numQuestions)
        {
            if (type == 1)
                SubjectExam = new FinalExam(time, numQuestions);
            else if (type == 2)
                SubjectExam = new PracticalExam(time, numQuestions);
            else
                Console.WriteLine("Invalid exam type.");
        }

        public object Clone()
        {
            return new Subject(SubjectId, SubjectName, SubjectExam != null ? (Exam)SubjectExam.Clone() : null);
        }

        public int CompareTo(Subject? other)
        {
            if (other == null) return 1;
            return SubjectId.CompareTo(other.SubjectId);
        }

        public override string ToString()
        {
            return $"Subject [{SubjectId}]: {SubjectName}";
        }
    }
}
