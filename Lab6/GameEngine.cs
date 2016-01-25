using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab6
{
    //recieves 0-9 array of tiles, returns best move
    public class GameEngine
    {
        public int[] board = new int[9];
        public GameEngine(int[] in_board)
        {
            board = in_board;
        }
        public Point ComputerMove()
        {
            Point block = new Point(0, 0);
            bool blockPoint = false;
            for (int i = 0; i < 3; i++)
            {
                // cols
                if((board[i] == board[(i+3)]) && board[i] != 0)
                {
                    if(board[i+6] == 0)
                    {
                        //first two in a column are equal, last empty
                        if (board[i] == 2) { return new Point(i, 2); } //return for win
                        else { block = new Point(i, 2); blockPoint = true; }
                    }
                }
                if ((board[i] == board[(i + 6)]) && board[i] != 0)
                {
                    if (board[i + 3] == 0)
                    {
                        //first and last in a column are equal, last empty
                        if (board[i] == 2) { return new Point(i, 1); } //return for win
                        else { block = new Point(i, 1); blockPoint = true; }
                    }
                }
                if ((board[i + 6] == board[(i + 3)]) && board[i] != 0)
                {
                    if (board[i] == 0)
                    {
                        //last two in a column are equal, last empty
                        if (board[(i + 3)] == 2) { return new Point(i, 0); } //return for win
                        else { block = new Point(i, 0); blockPoint = true; }
                    }
                }
                //rows
                int first = (3 * i);
                int second = (3 * i) + 1;
                int third = (3 * i) + 2;
                if((board[first] == board[second]) && board[first] != 0)
                {
                    if(board[third] == 0)
                    {
                        if (board[first] == 2) { return new Point(2, i); }
                        else { block = new Point(2, i); blockPoint = true; }
                    }
                }
                if((board[first] == board[third]) && board[first] != 0)
                {
                    if(board[second] == 0)
                    {
                        if (board[first] == 2) { return new Point(1, i); }
                        else { block = new Point(1, i); blockPoint = true; }
                    }
                }
                if((board[second] == board[third]) && board[second] != 0)
                for (int j = 0; j < 3; j++)
                {
                    if(board[first] == 0)
                    {
                        if (board[second] == 2) { return new Point(0, i); }
                        else { block = new Point(0, i); blockPoint = true; }
                    }
                }
            }
            //diags
            if ((board[0] == board[4]) && board[0] != 0)
            {
                if (board[8] == 0)
                {
                    if (board[0] == 2) { return new Point(2, 2); }
                    else { block = new Point(2, 2); blockPoint = true; }
                }
            }
            if ((board[0] == board[8]) && board[0] != 0)
            {
                if (board[4] == 0)
                {
                    if (board[0] == 2) { return new Point(1, 1); }
                    else { block = new Point(1, 1); blockPoint = true; }
                }
            }
            if ((board[4] == board[8]) && board[4] != 0)
            {
                if (board[0] == 0)
                {
                    if (board[4] == 2) { return new Point(0, 0); }
                    else { block = new Point(0, 0); blockPoint = true; }
                }
            }
            if ((board[2] == board[4]) && board[2] != 0)
            {
                if (board[6] == 0)
                {
                    if (board[2] == 2) { return new Point(0, 2); }
                    else { block = new Point(0, 2); blockPoint = true; }
                }
            }
            if ((board[2] == board[6]) && board[2] != 0)
            {
                if (board[4] == 0)
                {
                    if (board[2] == 2) { return new Point(1, 1); }
                    else { block = new Point(1, 1); blockPoint = true; }
                }
            }
            if ((board[4] == board[6]) && board[4] != 0)
            {
                if (board[2] == 0)
                {
                    if (board[4] == 2) { return new Point(2, 0); }
                    else { block = new Point(2, 0); blockPoint = true; }
                }
            }
            if(blockPoint)
            {
                return block;
            }
            for(int k = 0; k < 9; k++)
            {
                if(board[k] == 0)
                {
                    return new Point((k % 3), (k / 3));
                }
            }
            return new Point(0, 0); //default
        }
        public int checkWin()
        {
            for(int i = 0; i < 3; i++)
            {
                if ((board[i] == board[(i + 3)]) && (board[i] == board[(i + 6)]) && board[i] != 0)
                {
                    return board[i];
                }
                int first = (3 * i);
                int second = (3 * i) + 1;
                int third = (3 * i) + 2;
                if((board[first] == board[second]) && (board[first] == board[third]) && board[first] != 0)
                {
                    return board[first];
                }

            }
            if ((board[0] == board[4]) && (board[0] == board[8]) && board[0] != 0)
            {
                return board[0];
            }
            if ((board[2] == board[4]) && (board[2] == board[6]) && board[0] != 0)
            {
                return board[2];
            }
            return 0;
        }
    }
}
