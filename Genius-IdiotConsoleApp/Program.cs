using System;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;

namespace GeniusIdiotConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
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
                        Test();
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

        static void Test()
        {
            // Договариваемся что индекс вопроса и индекс овтета на него совпадают 
            // 1. Создаем переменную колличество вопросов и ответов
            int countQA = 5;
            int countDiagnoses = 6;

            // 2. Получение массивов с Вопросами, Ответами, Диагнозами
            string[] questions = GetQuestions(countQA);
            int[] answers = GetAnswers(countQA);
            string[] diagnoses = GetDiagnoses(countDiagnoses);

            Console.WriteLine("Добрый день. Введите свое имя...");
            string nameStudent = Console.ReadLine();

            // 3. Получаем колличество правильных ответов
            int correctAnswers = RunTest(questions, answers);

            // 4. Вывод диагноза 
            Console.WriteLine(nameStudent + ", ваш диагноз: " + diagnoses[correctAnswers]);
        }
        // 1.1 Создание вопросов
        static string[] GetQuestions(int questionsCounts)
        {
            string[] questions = new string[questionsCounts];
            questions[0] = "Сколько будет 2 плюс 2, умноженное на 2?";
            questions[1] = "Бревно нужно распилить на 10 частей. Сколько надо сделать распилов?";
            questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
            questions[3] = "Укол делают каждые полчаса. Сколько нужно минут для трех уколов?";
            questions[4] = "5 свечей горело, 2 потухли. Сколько свечей осталось?";
            return questions;
        }
        // 1.2 Созадние овтетов
        static int[] GetAnswers(int answersCounts)
        {
            int[] answers = new int[answersCounts];
            answers[0] = 6;
            answers[1] = 9;
            answers[2] = 25;
            answers[3] = 60;
            answers[4] = 2;
            return answers;
        }
        // 1.3 Создание диагнозов
        static string[] GetDiagnoses(int countDiagnoses)
        {
            string[] diagnoses = new string[countDiagnoses];
            diagnoses[0] = "Идиот";
            diagnoses[1] = "Кретин";
            diagnoses[2] = "Дурак";
            diagnoses[3] = "Нормальный";
            diagnoses[4] = "Талант";
            diagnoses[5] = "Гений";
            return diagnoses;
        }

        // 3.1 Вопрос -> ответ -> проверка -> счетчик
        static int RunTest(string[] questions, int[] answers)
        {
            int i=1;
            int correctAnswers = 0; // Счетчик правильных ответов
            List<int> indices = new List<int> { 0, 1, 2, 3, 4 };
            Random rand = new Random();
            while (i <= questions.Length)
            {                               
                int randomIndex = rand.Next(indices.Count); // Получаем случайную позицию (индекс) от 0 до длины списка
                int randomElement = indices[randomIndex]; // Берем значение, которое лежит на этой позиции

                Console.Write($"Вопрос № {i}: ");
                Console.WriteLine(questions[randomElement]);
                int answer = TryAnswer();

                if (answer == answers[randomElement])
                {
                    correctAnswers++;
                }
                i++;
                indices.RemoveAt(randomIndex);
            }
            return correctAnswers;
        }
        // Проверка ввода ответа
        static int TryAnswer()
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