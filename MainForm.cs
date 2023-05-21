using System.Collections.Immutable;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.DataFormats;

namespace ComputationalGeometry
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private int labsCount = 11;
        private int[] eachLabExerciseCount = new int[] { 3, 3, 2, 2, 1, 3, 1, 3, 1, 1, 1};
        PictureBox pictureBox1;
        private void MainForm_Load(object sender, EventArgs e)
        {
            pictureBox1 = new PictureBox()
            {
                Location = new Point(20, 35 * labsCount),
                Size = new Size(this.Width - 50, this.Height - 35 * labsCount - 50),
                BorderStyle = BorderStyle.FixedSingle,
                Parent = this,
            };
            pictureBox1.Click += DrawPointOnPictureBox;

            List<List<Button>> buttons = new List<List<Button>>();

            for(int i=0;i<labsCount;i++)
            {
                List<Button> buttonsRow = new List<Button>();
                for(int j = 0; j < eachLabExerciseCount[i];j++)
                {
                    Button button = new Button()
                    {
                        Text = $"Lab {(i + 1).ToString()} Exercise {(j + 1).ToString()}",
                        Name = $"Lab{(i + 1).ToString()}Exercise{(j + 1).ToString()}",
                        Size = new Size(Size.Width / eachLabExerciseCount[i], 30),
                        Location = new Point(j * Size.Width / eachLabExerciseCount[i], i * 35),
                        //Click += $"{Name}_Click",
                        Parent = this,
                    };
                    
                    switch (i)
                    {
                        case 0: 
                            switch (j) 
                            {
                                case 0: button.Click += Lab1Exercise1_Click; break;
                                case 1: button.Click += Lab1Exercise2_Click; break;
                                case 2: button.Click += Lab1Exercise3_Click; break;
                            }
                            break;
                        case 1:
                            switch (j)
                            {
                                case 0: button.Click += Lab2Exercise1_Click; break;
                                case 1: button.Click += Lab2Exercise2_Click; break;
                                case 2: button.Click += Lab2Exercise3_Click; break;
                            }
                            break;
                        case 2:
                            switch (j)
                            {
                                case 0: button.Click += Lab3Exercise1_Click; break;
                                case 1: button.Click += Lab3Exercise2_Click; break;
                            }
                            break;
                        case 3:
                            switch (j)
                            {
                                case 0: button.Click += Lab4Exercise1_Click; break;
                                case 1: button.Click += Lab4Exercise2_Click; break;
                            }
                            break;
                        case 4: button.Click += Lab5Exercise1_Click; break;
                        case 5:
                            switch (j)
                            {
                                case 0: button.Click += Lab6Exercise1_Click; break;
                                case 1: button.Click += Lab6Exercise2_Click; break;
                                case 2: button.Click += Lab6Exercise3_Click; break;
                            }
                            break;
                        case 6: button.Click += Lab7Exercise1_Click; break;
                        case 7:
                            switch (j)
                            {
                                case 0: button.Click += Lab8Exercise1_Click; break;
                                case 1: button.Click += Lab8Exercise2_Click; break;
                                case 2: button.Click += Lab8Exercise3_Click; break;
                            }
                            break;
                        case 8: button.Click += Lab9Exercise1_Click; break;
                        case 9: button.Click += Lab10Exercise1_Click; break;
                        case 10: button.Click += Lab11Exercise1_Click; break;
                        default: throw new IndexOutOfRangeException();
                    }
                    buttonsRow.Add(button);
                }
                buttons.Add(buttonsRow);
            }
        }


        private void DrawPointOnPictureBox(object? sender, EventArgs e)
        {
            if(Engine.DrawingEnabled )
            {
                Point click = (e as MouseEventArgs).Location;
                MyPoint point = new MyPoint(click.X, click.Y);
                Graphics g = pictureBox1.CreateGraphics();
                GraphicsHelper.DrawPoint(point, g);
                Engine.drawnPoints.Add(new MyPoint(click.X, click.Y));

                if (Engine.drawnPoints.Count()>1)
                {
                    GraphicsHelper.DrawSegment(new Segment(point, Engine.drawnPoints[Engine.drawnPoints.Count - 2]), g);
                    if (Engine.drawnPoints.First().Distance(Engine.drawnPoints[Engine.drawnPoints.Count-1]) < 10)
                    {
                        Engine.DrawingEnabled = false;
                        Engine.Complete = true;
                        GraphicsHelper.DrawSegment(new Segment(Engine.drawnPoints.First(), Engine.drawnPoints[Engine.drawnPoints.Count - 1]), g);
                        Engine.drawnPoints.RemoveAt(Engine.drawnPoints.Count - 1);
                    }
                }

            }
        }

        private void Lab1Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.MinimumAreaRectangle(pictureBox1);
        }

        private void Lab1Exercise2_Click(object? sender, EventArgs e)
        {
            Engine.TwoSetsUnion(pictureBox1);
        }

        private void Lab1Exercise3_Click(object? sender, EventArgs e)
        {
            Engine.FindLargestCircle(pictureBox1);
        }

        private void Lab2Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.FindPointsCloserThanD(pictureBox1);
        }

        private void Lab2Exercise2_Click(object? sender, EventArgs e)
        {
            Engine.FindSmallestAreaTriangleSorting(pictureBox1);
        }

        private void Lab2Exercise3_Click(object? sender, EventArgs e)
        {
            Engine.MinimumEnclosingCircle(pictureBox1);
        }

        private void Lab3Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.ShortestPairsBetweenPoints(pictureBox1);
        }

        private void Lab3Exercise2_Click(object? sender, EventArgs e)
        {
            Engine.SweepLineSegmentsIntersection(pictureBox1);
        }

        private void Lab4Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.ConvexHullSlow(pictureBox1);
        }

        private void Lab4Exercise2_Click(object? sender, EventArgs e)
        {
            Engine.ConvexHullJarvis(pictureBox1);
        }

        private void Lab5Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.ConvexHullJarvis(pictureBox1);
        }

        private void Lab6Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.RandomPolygonTriangulation(pictureBox1);
        }

        private void Lab6Exercise2_Click(object? sender, EventArgs e)
        {
            Engine.DrawInteractivePolygon(pictureBox1);
        }

        private void Lab6Exercise3_Click(object? sender, EventArgs e)
        {
            Engine.ConvexPolygonTriangulation(pictureBox1);
        }

        private void Lab7Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.PolygonTriangulationUsingDiagonals(pictureBox1);
        }

        private void Lab8Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.PolygonTriangulationUsingEarClipping(pictureBox1);
        }

        private void Lab8Exercise2_Click(object? sender, EventArgs e)
        {
            Engine.PolygonColoringUsingEarClipping(pictureBox1);
        }

        private void Lab8Exercise3_Click(object? sender, EventArgs e)
        {
            Engine.FindPolygonArea(pictureBox1);
        }

        private void Lab9Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.SimplePolygonIntoMonotonePolygons(pictureBox1);
        }

        private void Lab10Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.SimplePolygonIntoMonotonePolygonsLinear(pictureBox1);
        }

        private void Lab11Exercise1_Click(object? sender, EventArgs e)
        {
            Engine.PolygonIntoConvexPolygonsLinear(pictureBox1);
        }

    }
}