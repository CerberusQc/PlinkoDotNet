using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlinkoDotNet
{
    internal class Line
    {
        public int row;
        public Cell[] cells;

        public Line(int row, Point start)
        {
            this.row = row;
            this.cells = GenerateCells(row, start);
        }

        private Cell[] GenerateCells(int row, Point start)
        {
            Cell[] cells = new Cell[row];

            for (int i = 0; i < row; i++)
            {
                cells[i] = new Cell(i, start);
            }

            return cells;
        }
    }

    internal class Cell
    {
        public int column;
        public Rectangle rectangle;
        private Size circle_size = new Size(20, 20);
        private int circle_spacing = 20;

        public Cell(int column, Point start)
        {
            this.column = column;
            this.rectangle = new Rectangle(start, circle_size);
        }

    }
    internal class Plinko
    {
        private Control board;
        private Graphics graphics;
        private Rectangle rectangle;
        private Point center;
        private int row_count = 10;
        private Line[] lines;




        String user;
        private double bet;
        Color color;
        Brush brush = new SolidBrush(Color.Red);

        public Plinko(Control board, String user, double bet)
        {
            this.board = board;
            this.graphics = board.CreateGraphics();
            this.rectangle = board.DisplayRectangle;
            this.center = new Point(rectangle.Width / 2, rectangle.Height / 2);

            this.user = user;
            this.bet = bet;
            this.color = board.BackColor;
            this.lines = new Line[row_count];
        }

        private void GenerateBoard()
        {
            int count = 1;
            int height = board.Height;

            for (int i = 0; i < row_count; i++)
            {
                int current = height / row_count * i;
                Point start = new Point(center.X, current);
                lines[i] = new Line(i, start);
            }
        }

        public double Play()
        {
            double result = 0;

            GenerateBoard();
            DrawBoard();
            Thread.Sleep(1000);
            graphics.Clear(color);

            return result;
        }

        private void DrawBoard()
        {
            foreach (Line line in lines)
            {
                foreach (Cell cell in line.cells)
                {
                    DrawCircle(cell);
                }
            }
        }

        private void DrawCircle(Cell cell)
        {
            graphics.FillEllipse(brush, cell.rectangle);
        }
    }
}
