namespace MathGame
{
    public class MathGameLogic
    {
        public List<string> GameHistory { get; set; } = new List<string>();

        public void DisplayMenu()
        {
            Console.WriteLine("Please select an operation");
            Console.WriteLine("\t1\tAddition");
            Console.WriteLine("\t2\tSubtraction");
            Console.WriteLine("\t3\tMultiplication");
            Console.WriteLine("\t4\tDivision");
            Console.WriteLine("\t5\tRandom Game");
            Console.WriteLine("\t6\tShow History");
            Console.WriteLine("\t7\tChange Difficulty");
            Console.WriteLine("\t8\tExit");
        }

        public int MathOperation(int firstNumber, int secondNumber, char operation)
        {
            switch (operation)
            {
                case '+':
                    GameHistory.Add($"{firstNumber} + {secondNumber} = {firstNumber + secondNumber}");
                    return firstNumber + secondNumber;
                case '-':
                    GameHistory.Add($"{firstNumber} - {secondNumber} = {firstNumber - secondNumber}");
                    return firstNumber - secondNumber;
                case '*':
                    GameHistory.Add($"{firstNumber} * {secondNumber} = {firstNumber * secondNumber}");
                    return firstNumber * secondNumber;
                case '/':
                    while(firstNumber < 0 || firstNumber > 100)
                    {
                        try
                        {
                            Console.WriteLine("Please enter a number between 0 and 100");
                            firstNumber = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (System.Exception)
                        {
                            // do nothing
                        }
                    }
                    GameHistory.Add($"{firstNumber} / {secondNumber} = {firstNumber / secondNumber}");
                    return firstNumber / secondNumber;
            }
            return 0;
        }
    }
}