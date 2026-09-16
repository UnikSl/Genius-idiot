namespace GeniusIdiotConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1. Пройти тест.");
                Console.WriteLine("2. Выйти");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Вы выбрали пройти тест.");
                        StartTest();
                        break;
                    case "2":
                        Console.WriteLine("Вы выбрали выйти.");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        private static void StartTest()
        {
            string studentName = GetStudentName();
            List<string> questions = GetQuestions();
            List<int> testAnswers = GetAnswers();
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

            string diagnosis = GetDiagnoses(correctAnswersCount);
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
            List<int> answers = [6, 9, 25, 60, 2];
            return answers;
        }

        private static string GetDiagnoses(int correctAnswersCount)
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
            int answer = 0;
            while (true)
            {
                string inputAnswer = Console.ReadLine();
                if (int.TryParse(inputAnswer, out answer))
                {
                    Console.WriteLine($"Ваш ответ {answer}");
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ввели не число");
                }
            }
            return answer;
        }
    }
}