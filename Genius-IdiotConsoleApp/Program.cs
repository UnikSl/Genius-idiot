namespace GeniusIdiotConsoleApp
{
    internal class Program
    {
        private static void Main()
        {            
            bool isRunning = true;
            while (isRunning)
            {
                List<string> questions = [];
                List<int> answers = [];
                List<string> diagnoses = [];
                FillQuestions(questions);
                FillAnswers(answers);
                FillDiagnoses(diagnoses);
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
                        StartTest(questions, answers, diagnoses);
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

        private static void StartTest(List<string> questions, List<int> answers, List<string> diagnoses)
        {                      

            Console.WriteLine("Добрый день. Введите свое имя...");
            string studentName = Console.ReadLine();
            int correctAnswers = ConductTest(questions, answers);                        
            Console.WriteLine(studentName + ", ваш диагноз: " + diagnoses[correctAnswers]);
        }

        private static int ConductTest(List<string> questions, List<int> answers)
        {
            int questionNumber = 1;
            int correctAnswers = 0;            
            List<int> indices = [];
            for (int i = 0; i < questions.Count; i++)
            {
                indices.Add(i);
            }
            while (questionNumber <= questions.Count)
            {
                int randomIndex = Random.Shared.Next(indices.Count);
                int questionIndex = indices[randomIndex];

                Console.Write($"Вопрос № {questionNumber}: ");
                Console.WriteLine(questions[questionIndex]);
                int answer = GetValidInput();

                if (answer == answers[questionIndex])
                {
                    correctAnswers++;
                }
                questionNumber++;
                indices.RemoveAt(randomIndex);
            }
            return correctAnswers;
        }

        private static void FillQuestions(List<string> questions)
        {            
            questions.Add("Сколько будет 2 плюс 2, умноженное на 2?");
            questions.Add("Бревно нужно распилить на 10 частей. Сколько надо сделать распилов?");
            questions.Add("На двух руках 10 пальцев. Сколько пальцев на 5 руках?");
            questions.Add("Укол делают каждые полчаса. Сколько нужно минут для трех уколов?");
            questions.Add("5 свечей горело, 2 потухли. Сколько свечей осталось?");            
        }

        private static void FillAnswers(List<int> answers)
        {            
            answers.Add(6);
            answers.Add(9);
            answers.Add(25);
            answers.Add(60);
            answers.Add(2);            
        }

        private static void FillDiagnoses(List<string> diagnoses)
        {
            diagnoses.Add("Идиот");
            diagnoses.Add("Кретин");
            diagnoses.Add("Дурак");
            diagnoses.Add("Нормальный");
            diagnoses.Add("Талант");
            diagnoses.Add("Гений");            
        }              
        
        private static int GetValidInput()
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