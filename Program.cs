using System.Diagnostics;
using MathGame;

MathGameLogic mathGame = new MathGameLogic();
Random random = new Random();

int firstNumber;
int secondNumber;
int userMenuSelection;
int score = 0;
bool gameOver = false;

DifficultyLevel difficultyLevel = DifficultyLevel.Easy;
while(!gameOver)
{
    userMenuSelection = GetUserMenuSelection(mathGame);

    firstNumber = random.Next(1,101);
    secondNumber = random.Next(1,101);
    switch (userMenuSelection)
    {
        case 1:
            score += await PerformOperation(mathGame, firstNumber, secondNumber, score, '+', difficultyLevel);
            break;
        case 2:
            score += await PerformOperation(mathGame, firstNumber, secondNumber, score, '-', difficultyLevel);
            break;
        case 3:
            score += await PerformOperation(mathGame, firstNumber, secondNumber, score, '*', difficultyLevel);
            break;
        case 4:
            while(firstNumber % secondNumber !=0)
            {
                firstNumber = random.Next(1,101);
                secondNumber = random.Next(1,101);
            }
            score += await PerformOperation(mathGame, firstNumber, secondNumber, score, '/', difficultyLevel);
            break;
        case 5:
            int numberOfQuestions = 99;
            Console.WriteLine("Please enter the number of question you want to attempt.");
            while(!int.TryParse(Console.ReadLine(), out numberOfQuestions))
            {
                Console.WriteLine("Please enter the number of question you want to attempt.");
            }
            while(numberOfQuestions > 0)
            {
                int randomOperation = random.Next(1,5);

                if(randomOperation == 1)
                {
                    firstNumber = random.Next(1,101);
                    secondNumber = random.Next(1,101);
                    score += await PerformOperation(mathGame, firstNumber, secondNumber, score, '+', difficultyLevel);
                }
                else if(randomOperation == 2)
                {   
                    firstNumber = random.Next(1,101);
                    secondNumber = random.Next(1,101);
                    score += await PerformOperation(mathGame, firstNumber, secondNumber, score, '-', difficultyLevel);
                }
                else if(randomOperation == 3)
                {   
                    firstNumber = random.Next(1,101);
                    secondNumber = random.Next(1,101);
                    score += await PerformOperation(mathGame, firstNumber, secondNumber, score, '*', difficultyLevel);
                }
                else if(randomOperation == 4)
                {   
                    firstNumber = random.Next(1,101);
                    secondNumber = random.Next(1,101);
                    while(firstNumber % secondNumber !=0)
                    {
                        firstNumber = random.Next(1,101);
                        secondNumber = random.Next(1,101);
                    }
                    score += await PerformOperation(mathGame, firstNumber, secondNumber, score, '/', difficultyLevel);
                }

                numberOfQuestions--;
            }
            break;
        case 6:
            Console.WriteLine("GAME HISTORY: \n");
            foreach (var operation in mathGame.GameHistory)
            {
                Console.WriteLine($"{operation}");
            }
            break;
        case 7:
            difficultyLevel = ChangeDifficulty();
            DifficultyLevel difficultyEnum = (DifficultyLevel)difficultyLevel;
            Enum.IsDefined(typeof(DifficultyLevel), difficultyEnum);
            Console.WriteLine($"Your ne difficulty level: {difficultyLevel}");
            break;
        case 8:
            gameOver = true;
            Console.WriteLine($"Your finel score is: {score}");
            break;
    }
}



static DifficultyLevel ChangeDifficulty()
{
    int userDifficultySelection = 0;

    Console.WriteLine("Please enter a difficulty level");
    Console.WriteLine("\t1\tEasy");
    Console.WriteLine("\t2\tMedium");
    Console.WriteLine("\t3\tHard");

    while(!int.TryParse(Console.ReadLine(), out userDifficultySelection) || (userDifficultySelection < 1 || userDifficultySelection > 3))
    {
        Console.WriteLine("Please enter a valid choise (Between 1-3)");
    }

    switch (userDifficultySelection)
    {
        
        case 1:
            return DifficultyLevel.Easy;
        case 2:
            return DifficultyLevel.Medium;
        case 3:
            return DifficultyLevel.Hard;
    }

    return DifficultyLevel.Easy;
}

static void DisplayMathGameQuestion(int firstNumber, int secondNumber, char operation)
{
    Console.WriteLine($"{firstNumber} {operation} {secondNumber} = ?");
}

static int GetUserMenuSelection(MathGameLogic mathGame)
{
    int selection = -1;
    mathGame.DisplayMenu();

    while(!int.TryParse(Console.ReadLine(), out selection) || (selection < 1 || selection > 8))
    {
        Console.WriteLine("Please enter a valid choise (Between 1-8)");
    }

    return selection;
}

static async Task<int?> GetUserResponse(DifficultyLevel difficulty)
{
    int response = 0;
    int timeout = (int)difficulty;
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    Task<string?> getUserInputTask = Task.Run(() => Console.ReadLine());

    try
    {
        string? result = await Task.WhenAny(getUserInputTask, Task.Delay(timeout * 1000)) == getUserInputTask ? getUserInputTask.Result : null;

        stopwatch.Stop();

        if(result != null && int.TryParse(result, out response))
        {
            Console.WriteLine($"Time taken to answer: {stopwatch.Elapsed.ToString(@"mm\:ss\:fff")}");
            return response;
        }
        else
        {
            throw new OperationCanceledException();
        }
    }
    catch(OperationCanceledException)
    {
        Console.WriteLine("Time is up!!");
        return null;
    }
}

static int ValidateResult(int result, int? userResponse, int score)
{
    if (result == userResponse)
    {
        Console.WriteLine("Correct!; 5 Points");
        score += 5;
    }
    else
    {
        Console.WriteLine("Try again!");
        Console.WriteLine($"Correct answer is: {result}");
    }
    return score;
}

static async Task<int> PerformOperation(MathGameLogic mathGame, int firstNumber, int secondNumber, int score, char operation, DifficultyLevel difficulty)
{
    int result;
    int? userResponse;
    DisplayMathGameQuestion(firstNumber, secondNumber, operation);
    result = mathGame.MathOperation(firstNumber, secondNumber, operation);
    userResponse = await GetUserResponse(difficulty);
    score += ValidateResult(result, userResponse, score);
    return score;
}

public enum DifficultyLevel
{
    Easy = 45,
    Medium = 30,
    Hard = 15,
}

