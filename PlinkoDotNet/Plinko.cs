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
    internal class Settings
    {
        internal const int lineSpacing = 15;
        internal const int cellSize = 10;
        internal const int boardPadding = 30;
        internal static int animationSpeed = 200;

        internal static Color ballColor = Color.White;
        internal static Color boardColor = Color.DarkGray;

        // prizes should always be odd numbers (bests are 11-13-15)
        internal static Prize[] prizes = {
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
    }
    internal class Line
    {
        public int row;
        public Point position;
        public List<Cell> cells;
        public static int spacing = Settings.lineSpacing;
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
                        cells.Add(new Cell(i * -1, new Point(position.X + offset, position.Y),Settings.boardColor));
                        cells.Add(new Cell(i, new Point(position.X - (offset), position.Y), Settings.boardColor));
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        cells.Add(new Cell(i, position, Settings.boardColor));
                    }
                    else if (i % 2 == 0)
                    {
                        int offset = i * spacing;
                        cells.Add(new Cell(i * -1, new Point(position.X + offset, position.Y), Settings.boardColor));
                        cells.Add(new Cell(i, new Point(position.X - (offset), position.Y), Settings.boardColor));
                    }
                }
            }

            return cells;
        }
    }

    internal class Cell
    {
        private static int cell_size = Settings.cellSize;

        private Brush brush;
        private Size circle_size = new Size(cell_size, cell_size);

        public int column;
        public Rectangle rectangle;

        public Cell(int column, Point position, Color color)
        {
            this.column = column;
            brush = new SolidBrush(color);
            rectangle = new Rectangle(position, circle_size);
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(brush, rectangle);
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
            pen = new Pen(color);
            brush = new SolidBrush(color);
        }
    }

    internal class PrizeRect
    {
        private Font font = new Font(FontFamily.GenericSansSerif, 7);

        public Prize prize;
        public Rectangle rectangle;

        public PrizeRect(Prize prize, Rectangle rectangle)
        {
            this.prize = prize;
            this.rectangle = rectangle;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(prize.pen, rectangle);
            Point textLocation = new Point(rectangle.Location.X, rectangle.Location.Y + 2);
            graphics.DrawString(String.Format("{0}X", prize.value), font, prize.brush, textLocation);
        }
    }
    internal class Plinko
    {
        private static Prize[] prizes = Settings.prizes;
        private static PrizeRect[] prizeRects = new PrizeRect[prizes.Length];
        private static int row_count = prizes.Length - 1;

        private bool debug = false;
        private const int padding = Settings.boardPadding;
        private int speed = Settings.animationSpeed;
        private int offset_left = -Line.spacing;
        private int offset_right = Line.spacing;
        private int offset_x = 0;
        private Random random = new Random();

        private Line[] lines;
        private Control board;
        private Graphics graphics;
        private Rectangle rectangle;
        private Point center;
        private Cell ball;
        private string user;
        private Color color;
        private double bet;

        public Plinko(Control board, string user, double bet)
        {
            this.board = board;
            this.user = user;
            this.bet = bet;

            graphics = board.CreateGraphics();
            rectangle = board.DisplayRectangle;
            center = new Point(rectangle.Width / 2, rectangle.Height / 2);
            color = board.BackColor;
            lines = new Line[row_count];
        }

        // ENTRYPOINT
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
            DropBall();
            Prize prize = CheckPrize();
            Clear();

            return bet * prize.value;
        }

        private void GenerateBoard()
        {
            int height = board.Height - padding;

            for (int i = 1; i < row_count + 1; i++)
            {
                int current = height / row_count * i;
                Point start = new Point(center.X - (Settings.cellSize / 2), current);
                lines[i - 1] = new Line(i, start);
            }
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
                prizeRect.Draw(graphics);
                position.Offset(Line.spacing * 2, 0);
            }
        }

        private void DrawBoard()
        {
            foreach (Line line in lines)
            {
                foreach (Cell cell in line.cells)
                {
                    cell.Draw(graphics);
                }
            }
        }

        private void DropBall()
        {
            Thread.Sleep(1000);

            Line current_line = lines[0];
            Cell first_cell = current_line.cells[0];
            Point position = first_cell.rectangle.Location;
            ball = new Cell(0, position, );
            ball.brush = ball_brush;

            foreach (Line line in lines)
            {
                UpdateBall(line.position);
                current_line = line;
            }

            current_line.position.Offset(0, Line.spacing + Settings.cellSize + 5);
            UpdateBall(current_line.position);
        }

        private void UpdateBall(Point position)
        {
            position.X = ball.rectangle.X;
            position.Offset(offset_x, 0);
            ball.rectangle.Location = position;
            ball.Draw(graphics);

            offset_x = random.Next(2) == 0 ? offset_left : offset_right; // pick a side to fall
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


        private void Clear()
        {
            Thread.Sleep(2000);

            graphics.Clear(color);
            graphics.Dispose();
            board.Refresh();
        }
    }
}
