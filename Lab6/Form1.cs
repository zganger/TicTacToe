using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private const float clientSize = 100;
        private const float lineLength = 80;
        private const float block = lineLength / 3;
        private const float offset = 10;
        private const float delta = 5;
        public int[] board = new int[9];
        public int turncounter;
        public bool gameOver = false;

        private enum CellSelection { N, O, X };
        private CellSelection[,] grid = new CellSelection[3, 3];

        private float scale;    //current scale factor
        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            ApplyTransform(g);
            PointF[] p = { new Point(e.X, e.Y) };
            g.TransformPoints(CoordinateSpace.World,
                CoordinateSpace.Device, p);
            if (p[0].X < 0 || p[0].Y < 0) return;
            int i = (int)(p[0].X / block);
            int j = (int)(p[0].Y / block);
            if (i > 2 || j > 2) return;
            else if (grid[i, j] == CellSelection.N) //only allow setting empty cells
            {
                if (!gameOver)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        grid[i, j] = CellSelection.X;
                        GameEngine engine = new GameEngine(board);
                        if (engine.checkWin() != 0)
                        {
                            if (engine.checkWin() == 1)
                            {
                                MessageBox.Show("You win!");
                                gameOver = true;
                                Invalidate();
                            }
                            else
                            {
                                MessageBox.Show("Computer wins");
                                gameOver = true;
                                Invalidate();
                            }
                        }
                        board[((3 * j) + i)] = 1; //1 means O
                        turncounter++;
                    }
                    if ((turncounter % 2 == 1) && !gameOver)//default turncounter = 0, player goes first
                    {
                        GameEngine engine = new GameEngine(board);
                        if (engine.checkWin() != 0)
                        {
                            if (engine.checkWin() == 1)
                            {
                                Invalidate();
                                MessageBox.Show("You win!");
                                gameOver = true;
                                Invalidate();
                            }
                            else
                            {
                                Invalidate();
                                MessageBox.Show("Computer wins");
                                gameOver = true;
                                Invalidate();
                            }
                        }
                        if (!gameOver && turncounter < 9)
                        {
                            Point move = engine.ComputerMove();
                            grid[move.X, move.Y] = CellSelection.O;
                            board[((3 * move.Y) + move.X)] = 2;
                            if (engine.checkWin() != 0)
                            {
                                if (engine.checkWin() == 1)
                                {
                                    Invalidate();
                                    MessageBox.Show("You win!");
                                    gameOver = true;
                                    Invalidate();
                                }
                                else
                                {
                                    Invalidate();
                                    MessageBox.Show("Computer wins");
                                    gameOver = true;
                                    Invalidate();
                                }
                            }
                        }
                        turncounter++;
                    }
                }
                else
                {
                    turncounter = 0;
                    for (int ii = 0; ii < 3; ii++)
                    {
                        for (int jj = 0; jj < 3; jj++)
                        {
                            board[((3 * ii) + jj)] = 0;
                            grid[ii, jj] = CellSelection.N;
                        }
                    }
                    gameOver = false;
                }
            }
            else
            {
                MessageBox.Show("Invalid Move");
            }
            Invalidate();
        }

        private void ApplyTransform(Graphics g)
        {
            scale = Math.Min(ClientRectangle.Width / clientSize,
                ClientRectangle.Height / clientSize);
            if (scale == 0f) return;
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(offset, offset);
        }
        private void DrawX(int i, int j, Graphics g)
        {
            g.DrawLine(Pens.Black, i * block + delta, j * block + delta,
                (i * block) + block - delta, (j * block) + block - delta);
            g.DrawLine(Pens.Black, (i * block) + block - delta, j * block + delta,
               (i * block) + delta, (j * block) + block - delta);
        }
        private void DrawO(int i, int j, Graphics g)
        {
            g.DrawEllipse(Pens.Black, i * block + delta, j * block + delta,
                block - 2 * delta, block - 2 * delta);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ApplyTransform(g);

            //draw board
            g.DrawLine(Pens.Black, block, 0, block, lineLength);
            g.DrawLine(Pens.Black, 2 * block, 0, 2 * block, lineLength);
            g.DrawLine(Pens.Black, 0, block, lineLength, block);
            g.DrawLine(Pens.Black, 0, 2 * block, lineLength, 2 * block);

            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                    if (grid[i, j] == CellSelection.O) DrawO(i, j, g);
                    else if (grid[i, j] == CellSelection.X) DrawX(i, j, g);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < 9; i++)
            {
                board[i] = 0;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    board[((3*i) + j)] = 0;
                    grid[i, j] = CellSelection.N;
                }
            }
            gameOver = false;
            turncounter = 0;
            Invalidate();
        }

        private void computerStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < 9; i++ )
            {
                if(board[i] == 0)
                {
                    count++;
                }
            }
            if (count == 9)
            {
                grid[0, 0] = CellSelection.O;
                board[0] = 2;
                Invalidate();
            }
            else { MessageBox.Show("You've already started. Make a new game to let the AI go first."); }
        }
    }
}
