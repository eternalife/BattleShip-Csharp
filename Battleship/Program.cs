using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Board(Actions.Initialize, gameBoard);
            Board(Actions.Show, gameBoard);
            Board(Actions.SetPeg, gameBoard, 5, 5,'o');
            Board(Actions.SetPeg, gameBoard, 4, 6, 'x');
            Board(Actions.SetPiece, gameBoard, 3, 2, orientation:Orientation.Vertical, ship:Ships.Carrier);
            Board(Actions.Show, gameBoard);
            for(int i = 0; i < 255; ++i)
            {
                Console.Write(i + " " + (char)i);
            }

            Console.ReadKey();
        }
        public const int IN_HALF = 2;
        public const int BOARD_WIDTH = 10;
        public const int BOARD_HEIGHT = 10;
        public const int BOARD_SIZE = 100;
        public const int BOARD_EMPTY_SYMBOLE = 4;
        public const int CONSOLE_WIDTH = 120;
        public const int CONSOLE_HEIGHT = 30;
        public static char[] gameBoard = new char[BOARD_SIZE];
        //x = Hit peg
        //o = Miss Peg
        //+ = Ship section
        public enum Ships
        {
            Destroyer = 2,
            BattleShip = 3,
            Cruser = 4,
            Submarine = 4,
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
        public static void Board(Actions action, char[] board, int x = 0, int y = 0, char peg = 'o', Orientation orientation = Orientation.Horizontal, Ships ship = Ships.Destroyer)
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
                    board[x + y * BOARD_HEIGHT] = '+';
                    break;
                case Actions.SetPeg:
                    board[x + y * BOARD_HEIGHT] = peg;
                    break;
            }
        }

        public static void DrawTopMargin()
        {
            int TopOfGameBoard = (CONSOLE_HEIGHT / IN_HALF) - (BOARD_HEIGHT / IN_HALF);
            for(int i = 0; i < TopOfGameBoard; ++i)
            {
                Console.Write("\n");
            }
        }
        public static void DrawLeftMargin()
        {
            int LeftOfGameBoard = (CONSOLE_WIDTH / IN_HALF) - (BOARD_WIDTH / IN_HALF);
            for (int i = 0; i < LeftOfGameBoard; ++i)
            {
                Console.Write(" ");
            }
        }
    }
}
