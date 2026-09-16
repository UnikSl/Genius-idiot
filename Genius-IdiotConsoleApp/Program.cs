namespace GeniusIdiotConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            List<string> questions = GetQuestions();
            List<int> testAnswers = GetAnswers();
            bool isRunning = true;
            while (isRunning)
            {                
                Console.WriteLine();
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1. Пройти тест.");
                Console.WriteLine("2. Выйти");
                Console.WriteLine("3. Режим преподавателя.");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Вы выбрали пройти тест.");
                        StartTest(questions, testAnswers);
                        break;
                    case "2":
                        Console.WriteLine("Вы выбрали выйти.");
                        isRunning = false;
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Вы выбрали Режим преподавателя.");
                        TeacherMode(questions, testAnswers);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        private static void StartTest(List<string> questions, List<int> testAnswers)
        {
            string studentName = GetStudentName();
            int questionNumber = 1;
            int correctAnswersCount = 0;
            List<int> questionIndices = [];

            for (int i = 0; i < questions.Count; i++)
            {
                questionIndices.Add(i);
            }

            while (questionNumber <= questions.Count)
            {
                int randomIndex = Random.Shared.Next(questionIndices.Count);
                int questionIndex = questionIndices[randomIndex];
                Console.Write($"Вопрос № {questionNumber}: ");
                Console.WriteLine(questions[questionIndex]);
                int studentAnswer = GetStudentAnswer();
                if (studentAnswer == testAnswers[questionIndex])
                {
                    correctAnswersCount++;
                }
                questionNumber++;
                questionIndices.RemoveAt(randomIndex);
            }

            string diagnosis = GetDiagnosis(correctAnswersCount);
            Console.WriteLine(studentName + ", ваш диагноз: " + diagnosis);
        }

        private static List<string> GetQuestions()
        {
            List<string> questions =
            [
                "Сколько будет 2 плюс 2, умноженное на 2?",
                "Бревно нужно распилить на 10 частей. Сколько надо сделать распилов?",
                "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
                "Укол делают каждые полчаса. Сколько нужно минут для трех уколов?",
                "5 свечей горело, 2 потухли. Сколько свечей осталось?",
            ];
            return questions;
        }
        private static string GetStudentName()
        {
            Console.WriteLine("Добрый день. Введите свое имя...");
            string studentName = Console.ReadLine();
            return studentName;
        }
        private static List<int> GetAnswers()
        {
            List<int> testAnswers = [6, 9, 25, 60, 2];
            return testAnswers;
        }

        private static string GetDiagnosis(int correctAnswersCount)
        {
            List<string> diagnoses =
            [
                "Идиот",
                "Кретин",
                "Дурак",
                "Нормальный",
                "Талант",
                "Гений",
            ];
            return diagnoses[correctAnswersCount];
        }

        private static int GetStudentAnswer()
        {
            int studentAnswer = 0;
            while (true)
            {
                string inputAnswer = Console.ReadLine();
                if (int.TryParse(inputAnswer, out studentAnswer))
                {
                    Console.WriteLine($"Ваш ответ {studentAnswer}");
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ввели не число");
                }
            }
            return studentAnswer;
        }

        private static void TeacherMode(List<string> questions, List<int> testAnswers)
        {
            if (!TeacherLogin())
            {
                return;
            }
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1. Просмотреть вопросы и ответы.");
                Console.WriteLine("2. Изменить вопрос или ответ.");
                Console.WriteLine("3. Выйти из режима преподавателя.");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Вы решили просмотреть вопросы и ответы:");
                        DisplayQuestionsAndAnswers(questions, testAnswers);
                        break;
                    case "2":
                        Console.WriteLine("Вы решили изменить вопрос или ответ.");
                        ChangeQuestionAndAnswer(questions, testAnswers);
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
        private static bool TeacherLogin()
        {
            Console.WriteLine("Чтобы войти в Режим преподавателя, введите пароль: ");
            string password = Console.ReadLine();
            if (password == "admin")
            {
                Console.WriteLine("Добро пожаловать в Режим преподавателя!");
                return true;
                
            }            
            Console.WriteLine("Неверный пароль."); 
            return false;
        }
        private static void DisplayQuestionsAndAnswers(List<string> questions, List<int> testAnswers)
        {
            Console.WriteLine("Вопросы и ответы:");
            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"Вопрос {i + 1}: {questions[i]} - Ответ: {testAnswers[i]}");
            }
        }
        private static void ChangeQuestionAndAnswer(List<string> questions, List<int> testAnswers)
        {
            DisplayQuestionsAndAnswers(questions, testAnswers);
            Console.WriteLine("Введите номер вопроса, который хотите изменить: ");
            while (true)
            {
                string inputNumber = Console.ReadLine();
                if (int.TryParse(inputNumber, out int questionNumber) && questionNumber >= 1 && questionNumber <= questions.Count)
                {
                    Console.WriteLine("Введите новый вопрос: ");
                    string newQuestion = Console.ReadLine();
                    questions[questionNumber - 1] = newQuestion;
                    Console.WriteLine("Введите новый ответ: ");
                    while (true)
                    {
                        string inputAnswer = Console.ReadLine();
                        if (int.TryParse(inputAnswer, out int newAnswer))
                        {
                            testAnswers[questionNumber - 1] = newAnswer;
                            Console.WriteLine("Вопрос и ответ успешно изменены.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода");
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ввели не число");
                }
            }

        }
    }
}