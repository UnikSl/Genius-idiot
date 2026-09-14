using System;
using System.Collections.Generic;

namespace GeniusIdiotConsoleApp
{
    internal class Program
    {               
        static void Main(string[] args)
        {            
            List<StudentResult> students = new List<StudentResult>();
            List<Question> questions = new List<Question>();
            List<Diagnosis> diagnoses = new List<Diagnosis>();
            FillQuestions(questions);
            FillDiagnoses(diagnoses);
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1. Пройти тест.");
                Console.WriteLine("2. Выйти");
                Console.WriteLine("3. Режим преподавателя");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Вы выбрали пройти тест.");
                        Test(students, questions, diagnoses);
                        break;
                    case "2":
                        Console.WriteLine("Вы выбрали выйти.");
                        isRunning = false;
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Вы выбрали Режим преподавателя.");
                        TeacherLogin(students, questions);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }                
            }                                    
        }
                
        static void TeacherLogin(List<StudentResult> students, List<Question> questions)
        {
            Console.WriteLine("Чтобы войти в Режим преподавателя, введите пароль: ");
            string password = Console.ReadLine();
            if (password == "admin")
            {
                Console.WriteLine("Добро пожаловать в Режим преподавателя!");
                TeacherMenu(students, questions);
            }
            else
            {
                Console.WriteLine("Неверный пароль.");
                return;
            }
        }
        
        static void TeacherMenu(List<StudentResult> students, List<Question> questions)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1. Просмотреть вопросы и ответы.");
                Console.WriteLine("2. Статистика группы.");
                Console.WriteLine("3. Выйти из режима преподавателя.");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Вы решили просмотреть вопросы и ответы:");
                        ShowQuestionsAnswers(questions);
                        break;
                    case "2":
                        Console.WriteLine("Вы решили просмотреть статистику группы.");
                        ShowGroupStatistics(students);
                        break;
                    case "3":
                        Console.WriteLine("Выход из режима преподавателя.");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        
        static void ShowGroupStatistics(List<StudentResult> students)
        {
            Console.WriteLine("Статистика группы:");
            foreach (var student in students)
            {
                Console.WriteLine($"Имя: {student.StudentName}, Правильные ответы: {student.CorrectAnswers}, Диагноз: {student.Diagnosis}");
            }
        }
                
        public class StudentResult
        {
            public string StudentName { get; set; }
            public int CorrectAnswers { get; set; }
            public string Diagnosis { get; set; }
        }
                
        public class Question
        {
            public string Text { get; set; }
            public int Answer { get; set; }
        }

        public class Diagnosis
        {
            public string DiagnosisName { get; set; }
            public int DiagnosisCode { get; set; }            
        }

        static void ShowQuestionsAnswers(List<Question> questions)
        {
            foreach (var question in questions)
            {
                Console.WriteLine($"Вопрос: {question.Text}, Ответ: {question.Answer}");
            }
        }
                
        static void Test(List<StudentResult> students, List<Question> questions, List<Diagnosis> diagnoses)
        {               
            List<Question> questionsList = new List<Question>(questions);
            Console.WriteLine("Добрый день. Введите свое имя...");
            string studentName = Console.ReadLine();
            int i = 1;
            int correctAnswers = 0; 
            while (questionsList.Count > 0)
            {
                
                int randomIndex = Random.Shared.Next(questionsList.Count); 
                Console.Write($"Вопрос № {i}: ");
                Console.WriteLine(questionsList[randomIndex].Text);
                int answer = GetValidInput();

                if (answer == questionsList[randomIndex].Answer)
                {
                    correctAnswers++;
                }                
                questionsList.RemoveAt(randomIndex);
                i++;
            }
            string diagnosisName = "Неизвестно";
            foreach (var diagnosis in diagnoses)
            {
                if (diagnosis.DiagnosisCode == correctAnswers)
                {
                    diagnosisName = diagnosis.DiagnosisName;
                    break;
                }
            }
            students.Add(new StudentResult { StudentName = studentName, CorrectAnswers = correctAnswers, Diagnosis = diagnosisName });            
            
            Console.WriteLine(studentName + ", ваш диагноз: " + diagnosisName);
        }
        
        static void FillQuestions (List<Question> questions)
        {
            questions.Add(new Question { Text = "Сколько будет 2 плюс 2, умноженное на 2?", Answer = 6 });
            questions.Add(new Question { Text = "Бревно нужно распилить на 10 частей. Сколько надо сделать распилов?", Answer = 9 });
            questions.Add(new Question { Text = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?", Answer = 25 });
            questions.Add(new Question { Text = "Укол делают каждые полчаса. Сколько нужно минут для трех уколов?", Answer = 60 });
            questions.Add(new Question { Text = "5 свечей горело, 2 потухли. Сколько свечей осталось?", Answer = 2 });            
        }
                
        static void FillDiagnoses(List<Diagnosis> diagnoses)
        {
            diagnoses.Add(new Diagnosis { DiagnosisName = "Гений", DiagnosisCode = 5 });
            diagnoses.Add(new Diagnosis { DiagnosisName = "Идиот", DiagnosisCode = 0 });
            diagnoses.Add(new Diagnosis { DiagnosisName = "Кретин", DiagnosisCode = 1 });
            diagnoses.Add(new Diagnosis { DiagnosisName = "Талант", DiagnosisCode = 4 });
            diagnoses.Add(new Diagnosis { DiagnosisName = "Дурак", DiagnosisCode = 2 });
            diagnoses.Add(new Diagnosis { DiagnosisName = "Нормальный", DiagnosisCode = 3 });
            
            
        }
        static int GetValidInput()
        {            
            int answer = 0;
            while (true)
            {
                string inputAnswer = Console.ReadLine();
                if (int.TryParse(inputAnswer, out answer))
                {
                    Console.WriteLine($"Ваш ответ {answer}");
                    break;
                }
                Console.WriteLine("Вы ввели не число");                
            }
            return answer;
        }
    }
}