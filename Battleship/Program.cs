using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }
        public const int IN_HALF = 2;
        public const int BOARD_WIDTH = 10;
        public const int BOARD_HEIGHT = 10;
        public const int BOARD_SIZE = 100;
        public const int BOARD_EMPTY_SYMBOLE = 4;
        public static char[] gameBoard = new char[BOARD_SIZE];
        //x = Hit peg
        //o = Miss Peg
        //+ = Ship section
        public enum Ships
        {
            Destroyer = 2,
            Submarine = 3,
            Cruiser = 3,
            BattleShip = 4,
            Carrier = 5
        };
        public enum Actions
        {
            Initialize,
            Show,
            SetPiece,
            SetPeg,
        };
        public enum Orientation
        {
            Horizontal,
            Vertical
        };
        public static void MainMenu()
        {
            string input = null;
            bool exit = false;
            do
            {
                Console.Clear();
                if (input != null)
                {
                    Console.WriteLine("Sorry that option doesn't exist.");
                }
                else
                {
                    Console.Write("\n");
                }
                
                Console.WriteLine("Please select from the following options:");
                Console.WriteLine("1: Play against Mathematical AI");
                Console.WriteLine("2: Play against Deep Neural Network AI");
                Console.WriteLine("3: Play against another player");
                Console.WriteLine("4: Run all test cases");
                Console.WriteLine("5: Exit Game");
                input = Console.ReadLine();
            } while (
            input.CompareTo("1") != 0 &&
            input.CompareTo("2") != 0 &&
            input.CompareTo("3") != 0 &&
            input.CompareTo("4") != 0 &&
            input.CompareTo("5") != 0
            );

            switch (input)
            {
                case "1":
                    Console.WriteLine("You selected option #1");
                    break;
                case "2":
                    Console.WriteLine("You selected option #2");
                    break;
                case "3":
                    Console.WriteLine("You selected option #3");
                    break;
                case "4":
                    Console.WriteLine("You selected option #4");
                    Console.ReadKey();
                    Tests t = new Tests();
                    break;
                case "5":
                    exit = true;
                    break;
            }
            if(!exit)
            {
                Console.ReadKey();
                MainMenu();
            }
        }
        public static void Board(Actions action, char[] board, int x = 0, int y = 0, Orientation orientation = Orientation.Horizontal, Ships ship = Ships.Destroyer)
        {
            switch (action)
            {
                case Actions.Initialize:
                case Actions.Show:
                    if (action == Actions.Show)
                    {
                        Console.Clear();
                        DrawTopMargin();
                        DrawLeftMargin();
                    }
                    for (int i = 0; i < BOARD_SIZE; ++i)
                    {
                        switch (action)
                        {
                            case Actions.Initialize:
                                board[i] = (char)BOARD_EMPTY_SYMBOLE;
                                break;
                            case Actions.Show:
                                Console.Write(board[i]);
                                if ((i + 1) % BOARD_WIDTH == 0)
                                {
                                    Console.Write("\n");
                                    DrawLeftMargin();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case Actions.SetPiece:
                    bool setpiece = true;
                    switch (orientation)
                    {
                        case Orientation.Horizontal:
                            if (x + (int)ship < BOARD_WIDTH && x >= 0 && y < BOARD_HEIGHT && y >= 0)
                            {
                                for (int i = 0; i < (int)ship; ++i)
                                {
                                    if (board[(x + i) + y * BOARD_HEIGHT] == '+')
                                    {
                                        setpiece = false;
                                        Console.Write("Sorry can't set it there.\n");
                                        Console.ReadKey();
                                    }
                                }
                                if (setpiece)
                                {
                                    for (int i = 0; i < (int)ship; ++i)
                                    {
                                        board[(x + i) + y * BOARD_HEIGHT] = '+';
                                    }
                                }
                            }
                            else
                            {
                                Console.Write("Sorry can't set it there.\n");
                                Console.ReadKey();
                            }
                            break;
                        case Orientation.Vertical:
                            if (y + (int)ship < BOARD_HEIGHT && y >= 0 && x < BOARD_WIDTH && x >= 0)
                            {
                                for (int i = 0; i < (int)ship; ++i)
                                {
                                    if (board[x + (y + i) * BOARD_HEIGHT] == '+')
                                    {
                                        setpiece = false;
                                        Console.Write("Sorry can't set it there.\n");
                                        Console.ReadKey();
                                    }
                                }
                                if (setpiece)
                                {
                                    for (int i = 0; i < (int)ship; ++i)
                                    {
                                        board[x + (y + i) * BOARD_HEIGHT] = '+';
                                    }
                                }
                            }
                            else
                            {
                                Console.Write("Sorry can't set it there.\n");
                                Console.ReadKey();
                            }
                            break;
                    }
                    break;
                case Actions.SetPeg:
                    if (x < BOARD_WIDTH && x >= 0 & y < BOARD_HEIGHT && y >= 0)
                    {
                        if (board[x + y * BOARD_HEIGHT] == '+')
                        {
                            board[x + y * BOARD_HEIGHT] = 'h';
                            Board(Actions.Show, gameBoard);
                            Console.Write("HIT!!");
                            Console.ReadKey();
                        }
                        if (board[x + y * BOARD_HEIGHT] == (char)BOARD_EMPTY_SYMBOLE)
                        {
                            board[x + y * BOARD_HEIGHT] = 'm';
                            Board(Actions.Show, gameBoard);
                            Console.Write("Miss");
                            Console.ReadKey();
                        }
                    }
                    break;
            }
        }

        public static void DrawTopMargin()
        {
            int TopOfGameBoard = (Console.WindowHeight / IN_HALF) - (BOARD_HEIGHT / IN_HALF);
            for(int i = 0; i < TopOfGameBoard; ++i)
            {
                Console.Write("\n");
            }
        }
        public static void DrawLeftMargin()
        {
            int LeftOfGameBoard = (Console.WindowWidth / IN_HALF) - (BOARD_WIDTH / IN_HALF);
            for (int i = 0; i < LeftOfGameBoard; ++i)
            {
                Console.Write(" ");
            }
        }
    }
}
