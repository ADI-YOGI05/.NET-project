using System;

namespace Stopwatch
{
    class Program
    {
        static void Main(string[] args)
        {
            IState stopwatch = new Context(new InitialScreenState());

            // Press 2nd button => nothing happens
            PrintCurrentState(stopwatch);

            stopwatch = stopwatch.PressSecondButton();
            PrintCurrentState(stopwatch);

            // Start, Lap, Lap, Stop, Restart, Lap, Stop, Reset
            stopwatch = stopwatch.PressFirstButton(); // Started
            Console.WriteLine($"StopWatch {stopwatch.TextOnFirstButton}");
            PrintCurrentState(stopwatch);

            stopwatch = stopwatch.PressSecondButton(); // Lap
            Console.WriteLine($"StopWatch {stopwatch.TextOnFirstButton}");
            PrintCurrentState(stopwatch);

            stopwatch = stopwatch.PressSecondButton(); // Lap
            Console.WriteLine($"StopWatch {stopwatch.TextOnFirstButton}");
            PrintCurrentState(stopwatch);

            stopwatch = stopwatch.PressFirstButton(); // Stop
            Console.WriteLine($"StopWatch {stopwatch.TextOnFirstButton}");
            PrintCurrentState(stopwatch);

            stopwatch = stopwatch.PressFirstButton(); // Restart
            Console.WriteLine($"StopWatch {stopwatch.TextOnFirstButton}");
            PrintCurrentState(stopwatch);

            stopwatch = stopwatch.PressSecondButton(); // Lap
            Console.WriteLine($"StopWatch {stopwatch.TextOnFirstButton}");
            PrintCurrentState(stopwatch);

            stopwatch = stopwatch.PressFirstButton(); // Stop
            Console.WriteLine($"StopWatch {stopwatch.TextOnFirstButton}");
            PrintCurrentState(stopwatch);

            stopwatch = stopwatch.PressSecondButton(); // Reset
            Console.WriteLine($"StopWatch {stopwatch.TextOnFirstButton}");
            PrintCurrentState(stopwatch);
        }

        static void PrintCurrentState(IState stopwatch)
        {
            Console.WriteLine($"Current State: {stopwatch.GetType().Name}, Button1 [{stopwatch.TextOnFirstButton}], Button2 [{stopwatch.TextOnSecondButton}]");
        }
    }
}
