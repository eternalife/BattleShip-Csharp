using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            
            MainMenu();
        }

        public static char[] playerBoard = new char[BOARD_SIZE];
        public static char[] computerBoard = new char[BOARD_SIZE];

        public const int IN_HALF = 2;
        public const int BOARD_WIDTH = 10;
        public const int BOARD_HEIGHT = 10;
        public const int BOARD_SIZE = 100;
        public const int BOARD_EMPTY_SYMBOL = 4;
        public const int NUMBER_OF_SHIPS = 5;

        public const char HIT_SYMBOL = 'h';
        public const char MISS_SYMBOL = 'm';

        public const char AIRCRAFT_CARRIER_SYMBOL = 'A';
        public const char BATTLESHIP_SYMBOL = 'B';
        public const char CRUISER_SYMBOL = 'C';
        public const char DESTROYER_SYMBOL = 'D';
        public const char SUBMARINE_SYMBOL = 'S';

        public const int AIRCRAFT_CARRIER_LENGTH = 5;
        public const int BATTLESHIP_LENGTH = 4;
        public const int CRUISER_LENGTH = 3;
        public const int DESTROYER_LENGTH = 2;
        public const int SUBMARINE_LENGTH = 3;

        public static Dictionary<char, string> SHIP_NAMES = new Dictionary<char, string>{
            { AIRCRAFT_CARRIER_SYMBOL, "Aircraft-Carrier" },
            { BATTLESHIP_SYMBOL, "Battleship" },
            { CRUISER_SYMBOL, "Cruiser" },
            { DESTROYER_SYMBOL, "Destroyer" },
            { SUBMARINE_SYMBOL, "Submarine" },
        };

        public static Dictionary<char, ConsoleColor> SHIP_COLORS = new Dictionary<char, ConsoleColor>{
            { AIRCRAFT_CARRIER_SYMBOL, ConsoleColor.Yellow },
            { BATTLESHIP_SYMBOL, ConsoleColor.Red },
            { CRUISER_SYMBOL, ConsoleColor.Magenta },
            { DESTROYER_SYMBOL, ConsoleColor.Blue },
            { SUBMARINE_SYMBOL, ConsoleColor.Green },
        };

        public static Dictionary<char, int> SHIP_HIT_COUNT = new Dictionary<char, int>{
            { AIRCRAFT_CARRIER_SYMBOL, AIRCRAFT_CARRIER_LENGTH },
            { BATTLESHIP_SYMBOL, BATTLESHIP_LENGTH },
            { CRUISER_SYMBOL, CRUISER_LENGTH },
            { DESTROYER_SYMBOL, DESTROYER_LENGTH },
            { SUBMARINE_SYMBOL, SUBMARINE_LENGTH },
        };
        
        public enum Ships
        {
            Destroyer,
            Submarine,
            Cruiser,
            BattleShip,
            Aircraft_Carrier
        };
        public enum Actions
        {
            Initialize,
            Show,
            SetPiece,
            SetPeg,
            RandomizeShips,
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
                    //ConsoleKeyInfo ki = Console.ReadKey();
                    //Console.Write(ki.KeyChar);
                    //char c = ki.KeyChar;
                    //if (c == '\u001b')
                    //{
                    //    Console.Write("YES");
                    //}
                    //Console.ReadKey();
                    Board(Actions.RandomizeShips, playerBoard);

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
        /// <summary>
        /// Provides the API for all the Board actions and functionality.
        /// </summary>
        /// <param name="action">Determines which action to take upon the board passed in.</param>
        /// <param name="board">Board to take actions upon</param>
        /// <param name="x">Horizontal coordinate of the board to take action upon.</param>
        /// <param name="y">Vertical coordinate of the board to take action upon.</param>
        /// <param name="orientation">If action passed is set to SetPiece the ship will be placed to passed orientation.</param>
        /// <param name="ship">If action passed is set to SetPiece then the ship passed will be set to the board at location x, y and orientation passed in.</param>
        /// <returns></returns>
        public static bool Board(Actions action, char[] board, int x = 0, int y = 0, Orientation orientation = Orientation.Horizontal, Ships ship = Ships.Destroyer, bool messageFlag = true)
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
                                board[i] = (char)BOARD_EMPTY_SYMBOL;
                                break;
                            case Actions.Show:
                                ConsoleColor originalColor = Console.ForegroundColor;
                                if(SHIP_COLORS.ContainsKey(board[i]))
                                {
                                    Console.ForegroundColor = SHIP_COLORS[board[i]];
                                }
                                Console.Write(board[i]);
                                Console.ForegroundColor = originalColor;
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
                                for (int i = 0; i < SelectPiece(ship).Key; ++i)
                                {
                                    if (board[(x + i) + y * BOARD_HEIGHT] != (char)BOARD_EMPTY_SYMBOL)
                                    {
                                        setpiece = false;
                                        if (messageFlag)
                                        {
                                            Console.Write("Sorry can't set it there.\n");
                                            Console.ReadKey();
                                        }
                                        return false;
                                    }
                                }
                                if (setpiece)
                                {
                                    
                                    for (int i = 0; i < SelectPiece(ship).Key; ++i)
                                    {
                                        board[(x + i) + y * BOARD_HEIGHT] = SelectPiece(ship).Value;
                                    }
                                }
                            }
                            else
                            {
                                if (messageFlag)
                                {
                                    Console.Write("Sorry can't set it there.\n");
                                    Console.ReadKey();
                                }
                                return false;
                            }
                            break;
                        case Orientation.Vertical:
                            if (y + (int)ship < BOARD_HEIGHT && y >= 0 && x < BOARD_WIDTH && x >= 0)
                            {
                                for (int i = 0; i < SelectPiece(ship).Key; ++i)
                                {
                                    if (board[x + (y + i) * BOARD_HEIGHT] != (char)BOARD_EMPTY_SYMBOL)
                                    {
                                        setpiece = false;
                                        if (messageFlag)
                                        {
                                            Console.Write("Sorry can't set it there.\n");
                                            Console.ReadKey();
                                        }
                                        return false;
                                    }
                                }
                                if (setpiece)
                                {
                                    for (int i = 0; i < SelectPiece(ship).Key; ++i)
                                    {
                                        board[x + (y + i) * BOARD_HEIGHT] = SelectPiece(ship).Value;
                                    }
                                }
                            }
                            else
                            {
                                if (messageFlag)
                                {
                                    Console.Write("Sorry can't set it there.\n");
                                    Console.ReadKey();
                                }
                                return false;
                            }
                            break;
                    }
                    break;
                case Actions.SetPeg:
                    if (x < BOARD_WIDTH && x >= 0 & y < BOARD_HEIGHT && y >= 0)
                    {
                        if(board[x + y * BOARD_HEIGHT] == HIT_SYMBOL || board[x + y * BOARD_HEIGHT] == MISS_SYMBOL)
                        {
                            if (messageFlag)
                            {
                                Console.Write("Sorry can't set it there.\n");
                                Console.ReadKey();
                            }
                            return false;
                        }
                        else if (board[x + y * BOARD_HEIGHT] != (char)BOARD_EMPTY_SYMBOL)
                        {
                            string shipname = SHIP_NAMES[board[x + y * BOARD_HEIGHT]];
                            //update hit count
                            SHIP_HIT_COUNT[board[x + y * BOARD_HEIGHT]]--;

                            Board(Actions.Show, playerBoard);

                            Console.Write("HIT!! " + shipname);
                            if (SHIP_HIT_COUNT[board[x + y * BOARD_HEIGHT]] == 0)
                            {
                                Console.Write(" and SUNK IT!!");
                            }

                            board[x + y * BOARD_HEIGHT] = HIT_SYMBOL;

                            Console.ReadKey();
                        }
                        if (board[x + y * BOARD_HEIGHT] == (char)BOARD_EMPTY_SYMBOL)
                        {
                            board[x + y * BOARD_HEIGHT] = MISS_SYMBOL;
                            Board(Actions.Show, playerBoard);
                            Console.Write("Miss");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        if (messageFlag)
                        {
                            Console.Write("Sorry can't set it there.\n");
                            Console.ReadKey();
                        }
                    }
                    break;
                case Actions.RandomizeShips:
                    Random random = new Random();
                    Board(Actions.Initialize, playerBoard);
                    for (int i = 0; i < NUMBER_OF_SHIPS; ++i)
                    {
                        int _x;
                        int _y;
                        int _orientation;
                        do
                        {
                            _x = random.Next(0, 9);
                            _y = random.Next(0, 9);
                            _orientation = random.Next(0, 2);
                        } while (Board(Actions.SetPiece, playerBoard, _x, _y, (Orientation)_orientation, (Ships)i, false) == false);
                        Board(Actions.Show, playerBoard);
                    }
                    break;
            }
            return true;
        }
        /// <summary>
        /// Method returns the Length and the Symbol of the ship value passed in.
        /// </summary>
        /// <param name="ship">One of the 5 ships</param>
        /// <returns>integer lenght of ship and character symbol representing the ship</returns>
        public static KeyValuePair<int,char> SelectPiece(Ships ship)
        {
            switch(ship)
            {
                case Ships.BattleShip:
                    return new KeyValuePair <int,char>(BATTLESHIP_LENGTH, BATTLESHIP_SYMBOL);
                case Ships.Aircraft_Carrier:
                    return new KeyValuePair<int, char>(AIRCRAFT_CARRIER_LENGTH, AIRCRAFT_CARRIER_SYMBOL);
                case Ships.Cruiser:
                    return new KeyValuePair<int, char>(CRUISER_LENGTH, CRUISER_SYMBOL);
                case Ships.Destroyer:
                    return new KeyValuePair<int, char>(DESTROYER_LENGTH, DESTROYER_SYMBOL);
                case Ships.Submarine:
                    return new KeyValuePair<int, char>(SUBMARINE_LENGTH, SUBMARINE_SYMBOL);
            }
            return new KeyValuePair<int, char>(0, '\0');
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
