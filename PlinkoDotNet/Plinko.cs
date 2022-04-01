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
        public static int spacing = 15;
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
                        cells.Add(new Cell(i * -1, new Point(position.X + offset, position.Y)));
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
                        cells.Add(new Cell(i * -1, new Point(position.X + offset, position.Y)));
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
        public Brush brush = new SolidBrush(Color.DarkGray);

        public static int cell_size = 10;
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
        public Brush brush;

        public Prize(float value, Color color)
        {
            this.value = value;
            this.color = color;
            this.pen = new Pen(color);
            this.brush = new SolidBrush(color);
        }
    }

    internal class PrizeRect
    {
        public Prize prize;
        public Rectangle rectangle;

        public PrizeRect(Prize prize, Rectangle rectangle)
        {
            this.prize = prize;
            this.rectangle = rectangle;
        }
    }
    internal class Plinko
    {
        private Control board;
        private Graphics graphics;
        private Rectangle rectangle;
        private Point center;
        private const int padding = 30;
        private bool debug = false;
        private Cell ball;
        private Brush ball_brush = new SolidBrush(Color.White);
        private int speed = 200;
        private int offset_left = -Line.spacing;
        private int offset_right = Line.spacing;
        private int offset_y = Line.spacing - 5;
        private int offset_x = 0;
        private Random random = new Random();

        // prizes should always be odd numbers
        private static Prize[] prizes = {
            new Prize(10.0f, Color.Blue),
            new Prize(5.0f, Color.BlueViolet),
            new Prize(2.5f, Color.MediumPurple),
            new Prize(1.5f, Color.MediumPurple),
            new Prize(1.0f, Color.MediumPurple),
            new Prize(0.5f, Color.Purple),
            new Prize(0.0f, Color.Purple),
            new Prize(0.0f, Color.Purple),
            new Prize(0.0f, Color.Purple),
            new Prize(0.5f, Color.Purple),
            new Prize(1.0f, Color.MediumPurple),
            new Prize(1.5f, Color.MediumPurple),
            new Prize(2.5f, Color.MediumPurple),
            new Prize(5.0f, Color.BlueViolet),
            new Prize(10.0f, Color.Blue)
         };

        private static PrizeRect[] prizeRects = new PrizeRect[prizes.Length];

        private static int row_count = prizes.Length - 1;

        Brush cell_brush = new SolidBrush(Color.DarkGray);
        Font text_font = new Font(FontFamily.GenericSansSerif, 7);

        String user;
        private double bet;
        Color color;
        private Line[] lines;

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
                Point start = new Point(center.X - (Cell.cell_size / 2), current);
                lines[i - 1] = new Line(i, start);
            }
        }

        public double Play()
        {
            if (debug)
            {
                // debug draw center line
                graphics.DrawLine(Pens.Red, 0, center.Y, board.Width, center.Y);
                graphics.DrawLine(Pens.Red, center.X, 0, center.X, board.Height);
            }

            GenerateBoard();
            DrawPrizes();
            DrawBoard();
            Thread.Sleep(1000);
            DropBall();
            Prize prize = CheckPrize();
            Thread.Sleep(2000);

            Clear();

            return bet * prize.value;
        }

        private void Clear()
        {
            graphics.Clear(color);
            graphics.Dispose();
            board.Refresh();

        }

        private void DropBall()
        {
            Line current_line = lines[0];
            Cell first_cell = current_line.cells[0];
            Point position = first_cell.rectangle.Location;
            ball = new Cell(0, position);
            ball.brush = ball_brush;


            foreach (Line line in lines)
            {
               UpdateBall(line.position);
               current_line = line;
            }

            current_line.position.Offset(0, Line.spacing + Cell.cell_size);
            UpdateBall(current_line.position);
        }

        private void UpdateBall(Point position)
        {
            position.X = ball.rectangle.X;
            position.Offset(offset_x, 0);
            ball.rectangle.Location = position;
            DrawCell(ball);

            offset_x =  random.Next(2) == 0 ? offset_left : offset_right; // pick a side to fall
            Thread.Sleep(speed);
        }

        private Prize CheckPrize()
        {
            foreach (PrizeRect prizeRect in prizeRects)
            {
                if (prizeRect.rectangle.Contains(ball.rectangle.Location))
                {
                    return prizeRect.prize;
                }
            }

            return new Prize(0.0f, Color.Purple);
        }

        private void DrawBoard()
        {
            foreach (Line line in lines)
            {
                foreach (Cell cell in line.cells)
                {
                    DrawCell(cell);
                }
            }
        }

        private void DrawCell(Cell cell)
        {
            graphics.FillEllipse(cell.brush, cell.rectangle);
        }

        private void DrawPrizes()
        {
            int offset_count = prizes.Length / 2;
            int start_offset = offset_count * (Line.spacing * 2) + Line.spacing;
            Point position = new Point(center.X - start_offset, board.Height - padding);
            Size size = new Size(Line.spacing * 2, Line.spacing * 2);

            for (int i = 0; i < prizes.Length; i++)
            {
                Rectangle rectangle = new Rectangle(position, size);
                PrizeRect prizeRect = new PrizeRect(prizes[i], rectangle);
                prizeRects[i] = prizeRect;
                DrawPrize(prizeRect);
                position.Offset(Line.spacing * 2, 0);
            }
        }

        private void DrawPrize(PrizeRect prizeRect)
        {
            graphics.DrawRectangle(prizeRect.prize.pen, prizeRect.rectangle);
            Point textLocation = new Point(prizeRect.rectangle.Location.X, prizeRect.rectangle.Location.Y + 2);
            graphics.DrawString(String.Format("{0}X", prizeRect.prize.value), text_font, prizeRect.prize.brush, textLocation);
        }
    }
}
