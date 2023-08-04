using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Velkommen til quizzen! Hav det sjovt! ===");
            Console.ResetColor();

            Console.WriteLine("\nTryk på en vilkårlig tast for at starte quizzen...");
            Console.ReadKey();

            Console.Clear(); // Czyścimy konsolę przed rozpoczęciem quizu

            QuizData quizData = LoadQuizDataFromJson("json.json");

            int score = 0;
            foreach (QuizQuestion question in quizData.Spørgsmål)
            {
                Console.WriteLine($"Spørgsmål: {question.Spørgsmål1}");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nMuligheder:");
                Console.ResetColor();

                int optionNumber = 1;
                foreach (string option in question.Muligheder)
                {
                    Console.WriteLine($"{optionNumber}. {option}");
                    optionNumber++;
                }

                Console.Write("\nIndtast nummeret på det rigtige svar (1-3): ");
                int userAnswer = GetUserAnswer();

                if (userAnswer - 1 == question.CorrectAnswerIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Det rigtige svar!\n");
                    score++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Forkert svar. Det rigtige svar er: {question.CorrectAnswerIndex + 1} - {question.Muligheder[question.CorrectAnswerIndex]}\n");
                }

                Console.ResetColor();
            }

            Console.WriteLine($"Din score: {score}/{quizData.Spørgsmål.Count}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Tak fordi du deltog i quizzen! ===");
            Console.ResetColor();
        }

        static int GetUserAnswer()
        {
            int userAnswer;
            while (!int.TryParse(Console.ReadLine(), out userAnswer) || userAnswer < 1 || userAnswer > 3)
            {
                Console.WriteLine("Indtast nummeret på det rigtige svar (1-3).");
            }
            return userAnswer;
        }

        static QuizData LoadQuizDataFromJson(string jsonFileName)
        {
            string json = System.IO.File.ReadAllText("C:\\Users\\kak\\source\\repos\\Quiz\\Quiz\\json.json");
            return JsonConvert.DeserializeObject<QuizData>(json);
        }
    }

    public class QuizQuestion
    {
        public string Spørgsmål1 { get; set; }
        public List<string> Muligheder { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Forklaring { get; set; }
    }

    public class QuizData
    {
        public List<QuizQuestion> Spørgsmål { get; set; }
    }
}
