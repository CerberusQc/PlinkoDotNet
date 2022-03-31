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
        public Point position;
        public List<Cell> cells;
        public static int spacing = 20;
        public static Size spacing_size = new Size(spacing, spacing);

        public Line(int row, Point position)
        {
            this.row = row;
            this.position = position;
            this.cells = GenerateCells(row, position);
        }

        private List<Cell> GenerateCells(int row, Point position)
        {
            List<Cell> cells = new List<Cell> { };

            for (int i = 0; i <= row; i++)
            {

                if (row % 2 == 0)
                {

                    if (i % 2 != 0)
                    {
                        int offset = i * spacing;
                        cells.Add(new Cell(i * -1, new Point(position.X - (offset * -1), position.Y)));
                        cells.Add(new Cell(i, new Point(position.X - (offset), position.Y)));
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        cells.Add(new Cell(i, position));
                    }
                    else if (i % 2 == 0)
                    {
                        int offset = i * spacing;
                        cells.Add(new Cell(i * -1, new Point(position.X - (offset * -1), position.Y)));
                        cells.Add(new Cell(i, new Point(position.X - (offset), position.Y)));
                    }
                }

            }

            return cells;
        }
    }

    internal class Cell
    {
        public int column;
        public Rectangle rectangle;
        private static int cell_size = 15;
        private Size circle_size = new Size(cell_size, cell_size);


        public Cell(int column, Point position)
        {
            this.column = column;
            this.rectangle = new Rectangle(position, circle_size);
        }

    }

    internal class Prize
    {
        public float value;
        public Color color;
        public Pen pen;

        public Prize(float value, Color color)
        {
            this.value = value;
            this.color = color;
            this.pen = new Pen(color);
        }
    }
    internal class Plinko
    {
        private Control board;
        private Graphics graphics;
        private Rectangle rectangle;
        private Point center;
        private int row_count = 12;
        private int padding = 40;
        private Line[] lines;

        String user;
        private double bet;
        Color color;
        Brush cell_brush = new SolidBrush(Color.DarkGray);
        Brush text_brush = new SolidBrush(Color.Black);
        Font font = new Font(FontFamily.GenericSerif, 12);

        Prize best_prize = new Prize(5.0f, Color.Gold);
        Prize good_prize = new Prize(2.0f, Color.Green);
        Prize medium_prize = new Prize(1.5f, Color.Blue);
        Prize even_prize = new Prize(1.0f, Color.Yellow);
        Prize bad_prize = new Prize(0.0f, Color.Red);

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
            int height = board.Height - padding;

            for (int i = 1; i < row_count + 1; i++)
            {
                int current = height / row_count * i;
                Point start = new Point(center.X, current);
                lines[i - 1] = new Line(i, start);
            }
        }

        public double Play()
        {
            double result = 0;

            // debug draw center line
            graphics.DrawLine(Pens.Red, 0, center.Y, board.Width, center.Y);

            GenerateBoard();
            DrawBoard();
            DrawPrizes();
            Thread.Sleep(5000);
            graphics.Clear(color);
            graphics.Dispose();
            board.Refresh();

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
            graphics.FillEllipse(cell_brush, cell.rectangle);
        }

        private void DrawPrizes()
        {
            int prizes_count = row_count - 2 / 2;
            Line last_line = lines[lines.Length - 1];
            Point position = new Point(last_line.position.X, last_line.position.Y + padding / 2);
            DrawPrize(bad_prize, position);


            for (int i = 1; i <= prizes_count; i++)
            {

            }
        }

        private void DrawPrize(Prize prize, Point position)
        {
            graphics.DrawString(String.Format("{0}X", prize.value), font, text_brush, position);
            Point left_line_top = new Point(position.X - Line.spacing, position.Y + Line.spacing);
            Point left_line_bottom = new Point(position.X - Line.spacing, position.Y - Line.spacing);
            Point right_line_top = new Point(position.X + Line.spacing, position.Y + Line.spacing);
            Point right_line_bottom = new Point(position.X + Line.spacing, position.Y - Line.spacing);
            graphics.DrawLine(prize.pen, left_line_top, left_line_bottom);
            graphics.DrawLine(prize.pen, right_line_top, right_line_bottom);
        }
    }
}
