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
        public static char[] playerShotBoard = new char[BOARD_SIZE];
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

        //public class Ship_Attributes<_Name, _Color, _Length>
        //{
        //    public Ship_Attributes(_Name name, _Color color, _Length length)
        //    {
        //        Name = name;
        //        Color = color;
        //        Length = length;
        //    }
        //    public _Name Name { get; set; }
        //    public _Color Color { get; set; }
        //    public _Length Length { get; set; }
        //}
        //public static Dictionary<char, Ship_Attributes<string, ConsoleColor, int>> SHIP_ATTRIBUTES =
        //    new Dictionary<char, Ship_Attributes<string, ConsoleColor, int>>
        //    {
        //        {AIRCRAFT_CARRIER_SYMBOL,   new Ship_Attributes<string, ConsoleColor, int>("Aircraft-Carrier",  ConsoleColor.Yellow,    5) },
        //        {BATTLESHIP_SYMBOL,         new Ship_Attributes<string, ConsoleColor, int>("Battleship",        ConsoleColor.Red,       4) },
        //        {CRUISER_SYMBOL,            new Ship_Attributes<string, ConsoleColor, int>("Cruiser",           ConsoleColor.Magenta,   3) },
        //        {DESTROYER_SYMBOL,          new Ship_Attributes<string, ConsoleColor, int>("Destoryer",         ConsoleColor.Blue,      2) },
        //        {SUBMARINE_SYMBOL,          new Ship_Attributes<string, ConsoleColor, int>("Submarine",         ConsoleColor.Green,     3) },
        //    };

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

        public static Dictionary<int, int> SHIP_LENGTHS = new Dictionary<int, int>{
            { 4, AIRCRAFT_CARRIER_LENGTH },
            { 3, BATTLESHIP_LENGTH },
            { 2, CRUISER_LENGTH },
            { 0, DESTROYER_LENGTH },
            { 1, SUBMARINE_LENGTH },
        };

        public static Dictionary<char, int> SHIP_HIT_COUNT_PLAYER = new Dictionary<char, int>{
            { AIRCRAFT_CARRIER_SYMBOL, AIRCRAFT_CARRIER_LENGTH },
            { BATTLESHIP_SYMBOL, BATTLESHIP_LENGTH },
            { CRUISER_SYMBOL, CRUISER_LENGTH },
            { DESTROYER_SYMBOL, DESTROYER_LENGTH },
            { SUBMARINE_SYMBOL, SUBMARINE_LENGTH },
        };

        public static Dictionary<char, int> SHIP_HIT_COUNT_COMPUTER = new Dictionary<char, int>{
            { AIRCRAFT_CARRIER_SYMBOL,  AIRCRAFT_CARRIER_LENGTH },
            { BATTLESHIP_SYMBOL,        BATTLESHIP_LENGTH },
            { CRUISER_SYMBOL,           CRUISER_LENGTH },
            { DESTROYER_SYMBOL,         DESTROYER_LENGTH },
            { SUBMARINE_SYMBOL,         SUBMARINE_LENGTH },
        };

        public static Dictionary<char, bool> SHIP_PLACED_PLAYER = new Dictionary<char, bool>{
            { AIRCRAFT_CARRIER_SYMBOL,  false },
            { BATTLESHIP_SYMBOL,        false },
            { CRUISER_SYMBOL,           false },
            { DESTROYER_SYMBOL,         false },
            { SUBMARINE_SYMBOL,         false },
        };

        public static Dictionary<char, bool> SHIP_PLACED_COMPUTER = new Dictionary<char, bool>{
            { AIRCRAFT_CARRIER_SYMBOL,  false },
            { BATTLESHIP_SYMBOL,        false },
            { CRUISER_SYMBOL,           false },
            { DESTROYER_SYMBOL,         false },
            { SUBMARINE_SYMBOL,         false },
        };

        public enum PlayerType
        {
            Player,
            Computer,
        };
        /// <summary>
        /// Ship types
        /// </summary>
        public enum Ships
        {
            /// <summary>
            /// A size 2 Destroyer ship
            /// </summary>
            Destroyer,
            /// <summary>
            /// A size 3 Submarine ship
            /// </summary>
            Submarine,
            /// <summary>
            /// A size 3 Cruiser ship
            /// </summary>
            Cruiser,
            /// <summary>
            /// A size 4 BattleShip ship
            /// </summary>
            BattleShip,
            /// <summary>
            /// A size 5 Aircraft_Carrier ship
            /// </summary>
            Aircraft_Carrier
        };
        /// <summary>
        /// Board actions
        /// </summary>
        public enum Actions
        {
            /// <summary>
            /// Initialize the board array passed in to ♦
            /// </summary>
            Initialize,
            /// <summary>
            /// Displays a board array to the center of the screen
            /// </summary>
            Show,
            /// <summary>
            /// Sets a specified ship type to a specified x and y location and orientation
            /// </summary>
            SetShip,
            /// <summary>
            /// Sets a peg to specified x and y location displaying either a hit 'h' or miss 'm'
            /// </summary>
            SetShot,
            /// <summary>
            /// Randomly places all ship types on a given board
            /// </summary>
            RandomizeShips,
            /// <summary>
            /// Randomly places a specified ship type on a given board
            /// </summary>
            RandomShip,
            /// <summary>
            /// Randomly places a peg on a given board
            /// </summary>
            RandomShot,
            /// <summary>
            /// Returns whether passed in PlayerType has any more ships alive or not. 
            /// Still a ship alive returns true, no ships alive returns false.
            /// </summary>
            WinCondition,
        };
        /// <summary>
        /// Ship orientations
        /// </summary>
        public enum Orientation
        {
            /// <summary>
            /// Align ship horizontally
            /// </summary>
            Horizontal,
            /// <summary>
            /// Align ship vertically
            /// </summary>
            Vertical
        };
        /// <summary>
        /// Provides a selection menu for the player to choose what type of game
        /// they would like to play
        /// </summary>
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
                    SHIP_HIT_COUNT_COMPUTER = new Dictionary<char, int>{
                        { AIRCRAFT_CARRIER_SYMBOL, AIRCRAFT_CARRIER_LENGTH },
                        { BATTLESHIP_SYMBOL, BATTLESHIP_LENGTH },
                        { CRUISER_SYMBOL, CRUISER_LENGTH },
                        { DESTROYER_SYMBOL, DESTROYER_LENGTH },
                        { SUBMARINE_SYMBOL, SUBMARINE_LENGTH },
                    };
                    SHIP_HIT_COUNT_PLAYER = new Dictionary<char, int>{
                        { AIRCRAFT_CARRIER_SYMBOL, AIRCRAFT_CARRIER_LENGTH },
                        { BATTLESHIP_SYMBOL, BATTLESHIP_LENGTH },
                        { CRUISER_SYMBOL, CRUISER_LENGTH },
                        { DESTROYER_SYMBOL, DESTROYER_LENGTH },
                        { SUBMARINE_SYMBOL, SUBMARINE_LENGTH },
                    };
                    SHIP_PLACED_PLAYER = new Dictionary<char, bool>{
                        { AIRCRAFT_CARRIER_SYMBOL,  false },
                        { BATTLESHIP_SYMBOL,        false },
                        { CRUISER_SYMBOL,           false },
                        { DESTROYER_SYMBOL,         false },
                        { SUBMARINE_SYMBOL,         false },
                    };
                    SHIP_PLACED_COMPUTER = new Dictionary<char, bool>{
                        { AIRCRAFT_CARRIER_SYMBOL,  false },
                        { BATTLESHIP_SYMBOL,        false },
                        { CRUISER_SYMBOL,           false },
                        { DESTROYER_SYMBOL,         false },
                        { SUBMARINE_SYMBOL,         false },
                    };
                    Tests t = new Tests();
                    break;
                case "5":
                    exit = true;
                    break;
            }
            if (!exit)
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
        public static bool Board(Actions action, char[] board, int x = 0, int y = 0, Orientation orientation = Orientation.Horizontal, Ships ship = Ships.Destroyer, bool messageFlag = true, PlayerType player = PlayerType.Player)
        {
            Random random = new Random();
            switch (action)
            {
                case Actions.Initialize:
                case Actions.Show:
                    int row = 0;
                    if (action == Actions.Show)
                    {
                        Console.Clear();
                        DrawTopMargin();
                        DrawLeftMargin();
                        Console.Write(row++);
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
                                    if(row < 10)
                                    Console.Write(row++);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case Actions.SetShip:
                    bool setpiece = true;
                    switch (orientation)
                    {
                        case Orientation.Horizontal:
                            if (x + SelectPiece(ship).Key < BOARD_WIDTH && x >= 0 && y < BOARD_HEIGHT && y >= 0)
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
                                    switch(player)
                                    {
                                        case PlayerType.Player:
                                            if (!SHIP_PLACED_PLAYER[SelectPiece(ship).Value])
                                            {
                                                for (int i = 0; i < SelectPiece(ship).Key; ++i)
                                                {
                                                    board[(x + i) + y * BOARD_WIDTH] = SelectPiece(ship).Value;
                                                }
                                                if (player == PlayerType.Player)
                                                {
                                                    SHIP_PLACED_PLAYER[SelectPiece(ship).Value] = true;
                                                }
                                            }
                                            else
                                            {
                                                Console.Write("Ship already placed.\n");
                                                Console.ReadKey();
                                            }
                                            break;
                                        case PlayerType.Computer:
                                            if (!SHIP_PLACED_COMPUTER[SelectPiece(ship).Value])
                                            {
                                                for (int i = 0; i < SelectPiece(ship).Key; ++i)
                                                {
                                                    board[(x + i) + y * BOARD_WIDTH] = SelectPiece(ship).Value;
                                                }
                                                if (player == PlayerType.Player)
                                                {
                                                    SHIP_PLACED_COMPUTER[SelectPiece(ship).Value] = true;
                                                }
                                            }
                                            else
                                            {
                                                Console.Write("Ship already placed.\n");
                                                Console.ReadKey();
                                            }
                                            break;
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
                            if (y + SelectPiece(ship).Key < BOARD_HEIGHT && y >= 0 && x < BOARD_WIDTH && x >= 0)
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
                                    switch (player)
                                    {
                                        case PlayerType.Player:
                                            if (!SHIP_PLACED_PLAYER[SelectPiece(ship).Value])
                                            {
                                                for (int i = 0; i < SelectPiece(ship).Key; ++i)
                                                {
                                                    board[x + (y + i) * BOARD_HEIGHT] = SelectPiece(ship).Value;
                                                }
                                                if (player == PlayerType.Player)
                                                {
                                                    SHIP_PLACED_PLAYER[SelectPiece(ship).Value] = true;
                                                }
                                            }
                                            else
                                            {
                                                Console.Write("Ship already placed.\n");
                                                Console.ReadKey();
                                            }
                                            break;
                                        case PlayerType.Computer:
                                            if (!SHIP_PLACED_COMPUTER[SelectPiece(ship).Value])
                                            {
                                                for (int i = 0; i < SelectPiece(ship).Key; ++i)
                                                {
                                                    board[x + (y + i) * BOARD_HEIGHT] = SelectPiece(ship).Value;
                                                }
                                                if (player == PlayerType.Player)
                                                {
                                                    SHIP_PLACED_COMPUTER[SelectPiece(ship).Value] = true;
                                                }
                                            }
                                            else
                                            {
                                                Console.Write("Ship already placed.\n");
                                                Console.ReadKey();
                                            }
                                            break;
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
                case Actions.SetShot:
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
                            switch(player)
                            {
                                case PlayerType.Player:
                                    SHIP_HIT_COUNT_PLAYER[board[x + y * BOARD_HEIGHT]]--;
                                    break;
                                case PlayerType.Computer:
                                    SHIP_HIT_COUNT_COMPUTER[board[x + y * BOARD_HEIGHT]]--;
                                    break;
                            }

                            Board(Actions.Show, board);

                            switch (player)
                            {
                                case PlayerType.Player:
                                    Console.Write("HIT!! Player's " + shipname);
                                    if (SHIP_HIT_COUNT_PLAYER[board[x + y * BOARD_HEIGHT]] == 0)
                                    {
                                        Console.Write(" and SUNK IT!!");
                                    }
                                    break;
                                case PlayerType.Computer:
                                    Console.Write("HIT!! Computer's " + shipname);
                                    if (SHIP_HIT_COUNT_COMPUTER[board[x + y * BOARD_HEIGHT]] == 0)
                                    {
                                        Console.Write(" and SUNK IT!!");
                                    }
                                    break;
                            }
                            

                            board[x + y * BOARD_HEIGHT] = HIT_SYMBOL;

                            Console.ReadKey();
                            return true;
                        }
                        if (board[x + y * BOARD_HEIGHT] == (char)BOARD_EMPTY_SYMBOL)
                        {
                            board[x + y * BOARD_HEIGHT] = MISS_SYMBOL;
                            Board(Actions.Show, board);
                            Console.Write("Miss");
                            Console.ReadKey();
                            return true;
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
                    random = new Random();
                    for (int i = 0; i < NUMBER_OF_SHIPS; ++i)
                    {
                        int __x;
                        int __y;
                        int __orientation;
                        do
                        {
                            __x = random.Next(0, BOARD_WIDTH);
                            __y = random.Next(0, BOARD_HEIGHT);
                            __orientation = random.Next(0, 2);
                        } while (Board(Actions.SetShip, board, __x, __y, (Orientation)__orientation, (Ships)i, false, player) == false);
                    }
                    break;
                case Actions.RandomShip:
                    int _x;
                    int _y;
                    int _orientation;
                    do
                    {
                        _x = random.Next(0, BOARD_WIDTH);
                        _y = random.Next(0, BOARD_HEIGHT);
                        _orientation = random.Next(0, 2);
                    } while (Board(Actions.SetShip, board, _x, _y, (Orientation)_orientation, ship, false, player) == false);
                    break;
                case Actions.RandomShot:
                    bool hit = false;
                    while (!hit)
                    {
                        random = new Random();
                        hit = Board(Actions.SetShot, board, random.Next(0, BOARD_WIDTH), random.Next(0, BOARD_HEIGHT),messageFlag:false,player:player);
                    }
                    break;
                case Actions.WinCondition:
                    switch(player)
                    {
                        case PlayerType.Player:

                            int summed_ship_counts = 0;
                            foreach (var ship_hit_count in SHIP_HIT_COUNT_PLAYER)
                            {
                                summed_ship_counts += ship_hit_count.Value;
                            }
                            if (summed_ship_counts == 0)
                            {
                                Console.Write("Computer Wins!\n");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                            break;
                        case PlayerType.Computer:
                            summed_ship_counts = 0;
                            foreach(var ship_hit_count in SHIP_HIT_COUNT_COMPUTER)
                            {
                                summed_ship_counts += ship_hit_count.Value;
                            }
                            if (summed_ship_counts == 0)
                            {
                                Console.Write("Player Wins!\n");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                            break;
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
        /// <summary>
        /// Draws new line characters "\n" to center the board vertically.
        /// Adds numbering from 0-9 across the top of the board.
        /// </summary>
        public static void DrawTopMargin()
        {
            int TopOfGameBoard = (Console.WindowHeight / IN_HALF) - (BOARD_HEIGHT / IN_HALF);
            for(int i = 0; i < TopOfGameBoard - 1; ++i)
            {
                Console.Write("\n");
            }
            int LeftOfGameBoard = (Console.WindowWidth / IN_HALF) - (BOARD_WIDTH / IN_HALF);
            for (int i = 0; i < LeftOfGameBoard; ++i)
            {
                Console.Write(" ");
            }
            for (int i = 0; i < 10; ++i)
            {
                Console.Write(i);
            }
            Console.Write("\n");
        }
        /// <summary>
        /// Draws space characters " " to center the board horizontally
        /// Leaves a single space on the left for numbering the board horizontally
        /// </summary>
        public static void DrawLeftMargin()
        {
            int LeftOfGameBoard = (Console.WindowWidth / IN_HALF) - (BOARD_WIDTH / IN_HALF);
            for (int i = 0; i < LeftOfGameBoard - 1; ++i)
            {
                Console.Write(" ");
            }
        }
    }
}
