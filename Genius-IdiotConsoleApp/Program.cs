namespace GeniusIdiotConsoleApp
{
    internal class Program
    {
        private static void Main()
        {            
            List<string> questions = GetQuestions();
            List<int> answers = GetAnswers();

            while (true)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Вы выбрали пройти тест.");
                        StartTest(questions, answers);
                        break;

                    case "2":
                        Console.WriteLine("Вы выбрали выйти.");
                        return;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Вы выбрали Режим преподавателя.");
                        TeacherMode(questions, answers);
                        break;

                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пункт меню: ");
            Console.WriteLine("1. Пройти тест.");
            Console.WriteLine("2. Выйти");
            Console.WriteLine("3. Режим преподавателя.");
        }
        
        private static void StartTest(List<string> questions, List<int> answers)
        {
            string studentName = GetStudentName();
            int currentQuestionNumber = 1;
            int correctAnswersCount = 0;
            List<int> questionIndices = [];

            for (int i = 0; i < questions.Count; i++)
            {
                questionIndices.Add(i);
            }

            while (currentQuestionNumber <= questions.Count)
            {
                int randomQuestionIndex = Random.Shared.Next(questionIndices.Count);
                int currentQuestionIndex = questionIndices[randomQuestionIndex];

                Console.Write($"Вопрос № {currentQuestionNumber}: ");
                Console.WriteLine(questions[currentQuestionIndex]);

                int studentAnswer = GetStudentAnswer();

                if (studentAnswer == answers[currentQuestionIndex])
                {
                    correctAnswersCount++;
                }

                currentQuestionNumber++;
                questionIndices.RemoveAt(randomQuestionIndex);
            }

            string diagnosis = GetDiagnosis(correctAnswersCount);
            Console.WriteLine(studentName + ", ваш диагноз: " + diagnosis);
        }

        private static List<string> GetQuestions()
        {
            return
            [
                "Сколько будет 2 плюс 2, умноженное на 2?",
                "Бревно нужно распилить на 10 частей. Сколько надо сделать распилов?",
                "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
                "Укол делают каждые полчаса. Сколько нужно минут для трех уколов?",
                "5 свечей горело, 2 потухли. Сколько свечей осталось?",
            ];
        }

        private static string GetStudentName()
        {
            Console.WriteLine("Добрый день. Введите свое имя...");
            string name = Console.ReadLine();
            return name;
        }

        private static List<int> GetAnswers()
        {
            return [6, 9, 25, 60, 2];            
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
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int answer))
                {
                    Console.WriteLine($"Ваш ответ {answer}");
                    return answer;
                }
                
                Console.WriteLine("Вы ввели не число");                
            }
            
        }

        private static void TeacherMode(List<string> questions, List<int> answers)
        {
            if (!TeacherLogin())
            {
                return;
            }
            
            while (true)
            {
                ShowTeacherMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Вы решили просмотреть данные теста:");
                        DisplayTestData(questions, answers);
                        break;

                    case "2":
                        Console.WriteLine("Вы решили изменить данные теста.");
                        ChangeTestData(questions, answers);
                        break;

                    case "3":
                        Console.WriteLine("Выход из режима преподавателя.");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        private static void ShowTeacherMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пункт меню: ");
            Console.WriteLine("1. Просмотреть данные теста.");
            Console.WriteLine("2. Изменить данные теста.");
            Console.WriteLine("3. Выйти из режима преподавателя.");
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

        private static void DisplayTestData(List<string> questions, List<int> answers)
        {
            Console.WriteLine("Вопросы и ответы:");
            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"Вопрос {i + 1}: {questions[i]} - Ответ: {answers[i]}");
            }
        }

        private static void ChangeTestData(List<string> questions, List<int> answers)
        {
            DisplayTestData(questions, answers);
            Console.WriteLine("Введите номер вопроса, который хотите изменить: ");

            bool IsValidQuestionNumber(int number)
            {
                return number >= 1 && number <= questions.Count;
            }

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int questionNumber) && IsValidQuestionNumber(questionNumber))
                {
                    Console.WriteLine("Введите новый вопрос: ");
                    string newQuestion = Console.ReadLine();
                    questions[questionNumber - 1] = newQuestion;
                    Console.WriteLine("Введите новый ответ: ");

                    while (true)
                    {
                        input = Console.ReadLine();
                        if (int.TryParse(input, out int newAnswer))
                        {
                            answers[questionNumber - 1] = newAnswer;
                            Console.WriteLine("Вопрос и ответ успешно изменены.");
                            break;
                        }

                        Console.WriteLine("Ошибка ввода");                        
                    }
                    break;
                }

                Console.WriteLine("Некорректный номер вопроса. Попробуйте ещё раз.");                
            }

        }
    }
}