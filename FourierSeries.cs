using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace FourierSeriesDemo
{
    // struct for a 2-d point
    public struct P2D
    {
        public double x;
        public double y;
    }

    public struct Segment
    {
        // struct for a line segment - two 2-d points
        public double x1;
        public double y1;
        public double x2;
        public double y2;
    }

    public class FourierSeries
    {
        // properties
        private double Period;
        private ArrayList ControlPLine = new ArrayList();
        private ArrayList Points = new ArrayList();
        private int Iters;

        // Getters
        public double GetPeriod()
        {
            return Period;
        }

        public int GetIters()
        {
            return Iters;
        }

        // Setters
        public void SetPeriod(string MyStr)
        {
            Period = Double.Parse(MyStr);
        }

        public void SetIters(decimal MyVal)
        {
            Iters = Decimal.ToInt32(MyVal);
        }

        // method to read in the array describing the control curve from an input file
        public int ReadArray(string TheFileName, ListBox lb)
        {
            FileStream fs = new FileStream(TheFileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string value;
            Segment seg;
            int IsValid = 0;

            ControlPLine.Clear();

            try
            {
                while (sr.Peek() >= 0)
                {
                    value = sr.ReadLine();
                    seg.x1 = Convert.ToDouble(value);
                    value = sr.ReadLine();
                    seg.y1 = Convert.ToDouble(value);

                    value = sr.ReadLine();
                    seg.x2 = Convert.ToDouble(value);
                    value = sr.ReadLine();
                    seg.y2 = Convert.ToDouble(value);

                    ControlPLine.Add(seg);
                }
                // check that all the segments are not overlapping
                IsValid = CheckArray(lb);
                // the following line is important, in the sense that without it,
                // if you try to open another file in the same running of the program,
                // an exception will be triggered
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return IsValid;
        }

        /* method to check that array describing the control curve
         * meets certain conditions
         */
        private int CheckArray(ListBox l)
        {
            Segment seg;
            string str, msg;
            double h;
            int IsValid = 0;

            // first check is that the x-ord of the second point is
            // greater than that of the first one
            try {
                for (int i = 0; i < ControlPLine.Count; i++)
                {
                    seg = (Segment)ControlPLine[i];
                    if (seg.x1 > seg.x2)
                    {
                        /* if first node is to the left of the second node,
                         * switch them around
                         */
                        h = seg.x1;
                        seg.x1 = seg.x2;
                        seg.x2 = h;
                        h = seg.y1;
                        seg.y1 = seg.y2;
                        seg.y2 = h;

                        ControlPLine[i] = seg;
                    }
                    else if (seg.x1 == seg.x2)
                    {
                        // cannot have them equal - throw the exception
                        throw (new VerticalSegmentException("There is a vertical segment somewhere. Please check the input file."));
                    }
                }
            } catch (VerticalSegmentException vex)
            {
                MessageBox.Show(vex.Message.ToString());
                //Console.WriteLine(vex.Message.ToString());
                //Console.ReadLine();
                IsValid = 1;
            }

            try
            {
                for (int i = 0; i < ControlPLine.Count; i++)
                {
                    seg = (Segment)ControlPLine[i];
                    if (seg.x1 < -Period || seg.x2 < -Period || seg.x1 > Period || seg.x2 > Period)
                    {
                        /* here, it is checking that both points are inside the period range
                         * which is (-Period to +Period)
                         * where not, throw the exception
                         */
                        throw (new NodesOutOfRangeException("Some segments are outside the period range. Please check the input file."));
                    }
                }
            }
            catch (NodesOutOfRangeException nex)
            {
                MessageBox.Show(nex.Message.ToString());
                //Console.WriteLine(nex.Message.ToString());
                //Console.ReadLine();
                IsValid = 1;
            }

            for (int i = 0; i < ControlPLine.Count; i++)
            {
                seg = (Segment)ControlPLine[i];
                str = String.Format("({0:n3}, {1:n3}) - ({2:n3}, {3:n3})", seg.x1, seg.y1, seg.x2, seg.y2);
                //msg = string.Format(str, seg.x1, seg.y1, seg.x2, seg.y2);

                l.Items.Add(str);
            }

            return IsValid; // this being false will stop the "One More Iteration" button from being enabled
        }

        public void PlotGraph(PictureBox pb)
        {
            int PW = pb.ClientSize.Width;
            int PH = pb.ClientSize.Height;
            double XLeft = -3.2 * Period;
            double XRight = -XLeft;
            double YTop = PH * XRight / PW;
            double YBot = -YTop;

            double a, b;
            double an, bn;
            double angle, cosine, sine;
            
            Segment seg;
            Graphics Gr = pb.CreateGraphics();
            Pen PenRed = new Pen(Color.Red);
            Pen PenBlack = new Pen(Color.Black);
            Pen PenBlue = new Pen(Color.Blue);
            Pen PenMagenta = new Pen(Color.Magenta);
            Pen PenOrange = new Pen(Color.Orange);
            Pen PenGreen = new Pen(Color.FromArgb(0, 128, 0));
            Pen PenCyan = new Pen(Color.Cyan);

            Gr.Clear(Color.White);
            Points.Clear();

            // draw the x-axis
            DrawLineOnPB(Gr, pb, PenBlue, XLeft, 0, XRight, 0, XLeft, XRight, YTop, YBot);
            a = Math.Floor(XRight);
            for (int i = 0; i <= (int)(2 * a * 5); i++)
            {
                b = (i * 0.2) - a;
                if (i % 5 == 0)
                {
                    DrawLineOnPB(Gr, pb, PenGreen, b, -0.1, b, 0.1, XLeft, XRight, YTop, YBot);
                }
                else
                {
                    DrawLineOnPB(Gr, pb, PenCyan, b, -0.1, b, 0.1, XLeft, XRight, YTop, YBot);
                }
            }

            // draw the y-axis
            DrawLineOnPB(Gr, pb, PenBlue, 0, YTop, 0, YBot, XLeft, XRight, YTop, YBot);
            a = Math.Floor(YTop);
            for (int i = 0; i <= (int)(2 * a * 5); i++)
            {
                b = (i * 0.2) - a;
                if (i % 5 == 0)
                {
                    DrawLineOnPB(Gr, pb, PenGreen, -0.1, b, 0.1, b, XLeft, XRight, YTop, YBot);
                }
                else
                {
                    DrawLineOnPB(Gr, pb, PenCyan, -0.1, b, 0.1, b, XLeft, XRight, YTop, YBot);
                }
            }

            // draw the periods on the x-axis
            a = Math.Floor(XRight / Period);
            for (int i = 0; i <= (int)a; i++)
            {
                b = ((2 * i) - a) * Period;
                DrawLineOnPB(Gr, pb, PenRed, b, -0.5, b, 0.5, XLeft, XRight, YTop, YBot);
            }

            // draw in the control polyline
            for (int i = 0; i < ControlPLine.Count; i++)
            {
                seg = (Segment)ControlPLine[i];
                DrawLineOnPB(Gr, pb, PenMagenta, seg.x1, seg.y1, seg.x2, seg.y2, XLeft, XRight, YTop, YBot);
            }

            // set up the Points array
            int PointsCount = (int)Math.Floor((double)pb.ClientSize.Width / 2);
            double xord, yord = 0;
            P2D myPoint;

            // assign x and y values to each point in the Points array
            for (int i = 0; i < PointsCount; i++)
            {
                myPoint.x = XLeft + (i * (XRight - XLeft) / PointsCount);
                myPoint.y = yord;
                Points.Add(myPoint);
            }

            for (int n = 0; n < Iters; n++)
            {
                // Get the a- and b-coefficients
                an = 0;
                bn = 0;

                // Go through each segment, and calculate each segment's contribution to the final coefficient
                // where n = 0, just integrate the segment across the bounds, otherwise
                // calculate the integral of the function times the cosine
                // also where n = 0, the b-coefficient = 0
                for (int j = 0; j < ControlPLine.Count; j++)
                {
                    seg = (Segment)ControlPLine[j];
                    an = an + ACoeffSeg(seg, n);

                    if (n > 0) // get the b-coefficient, note that b0 = 0
                    {
                        bn = bn + BCoeffSeg(seg, n);
                    }
                }

                // Calculate the coordinates for each of the points on the graph
                for (int j = 0; j < Points.Count; j++)
                {
                    myPoint = (P2D)Points[j]; // note that Points is an array of P2D struct instances
                    yord = myPoint.y;
                    xord = XLeft + (j * (XRight - XLeft) / Points.Count);
                    if (n == 0)
                    {
                        yord = yord + (an / 2);
                    }
                    else
                    {
                        angle = Math.PI * n * xord / Period;
                        cosine = Math.Cos(angle);
                        sine = Math.Sin(angle);
                        yord = yord + (an * cosine) + (bn * sine);
                    }
                    myPoint.y = yord;
                    Points[j] = myPoint;
                }
            }

            P2D myNextPoint;

            // plot onto the picture box
            for (int i = 0; i < Points.Count - 1; i++)
            {
                myPoint = (P2D)Points[i];
                myNextPoint = (P2D)Points[i + 1];
                DrawLineOnPB(Gr, pb, PenBlack, myPoint.x, myPoint.y, myNextPoint.x, myNextPoint.y, XLeft, XRight, YTop, YBot);
            }
        }

        // calculate the a-coefficient
        private double ACoeffSeg(Segment s, int n)
        {
            double coeffa, coeffb, m, c;
            double pie = Math.PI;
            double anglea = pie * n * s.x1 / Period;
            double angleb = pie * n * s.x2 / Period;
            double sinea = Math.Sin(anglea);
            double sineb = Math.Sin(angleb);
            double cosinea = Math.Cos(anglea);
            double cosineb = Math.Cos(angleb);
            double pin = pie * n;
            double pin2 = pin * pin;

            m = (s.y2 - s.y1) / (s.x2 - s.x1);
            c = s.y1 - (m * s.x1);

            if (n == 0)
            {
                // first stage - just integrate the section of the control curve
                coeffa = 1 * ((m * s.x1 * s.x1 / 2) + (c * s.x1)) / Period;
                coeffb = 1 * ((m * s.x2 * s.x2 / 2) + (c * s.x2)) / Period;
            }
            else
            {
                coeffa = (m * ((s.x1 * sinea / pin) + (Period * cosinea / pin2))) + (c * sinea / pin);
                coeffb = (m * ((s.x2 * sineb / pin) + (Period * cosineb / pin2))) + (c * sineb / pin);
            }
            return coeffb - coeffa;
        }

        // calculate the b-coefficient
        private double BCoeffSeg(Segment s, int n)
        {
            double coeffa, coeffb, m, c;
            double pie = Math.PI;
            double anglea = pie * n * s.x1 / Period;
            double angleb = pie * n * s.x2 / Period;
            double sinea = Math.Sin(anglea);
            double sineb = Math.Sin(angleb);
            double cosinea = Math.Cos(anglea);
            double cosineb = Math.Cos(angleb);
            double pin = pie * n;
            double pin2 = pin * pin;

            m = (s.y2 - s.y1) / (s.x2 - s.x1);
            c = s.y1 - (m * s.x1);

            coeffa = (m * ((Period * sinea / pin2) - (s.x1 * cosinea / pin))) - (c * cosinea / pin);
            coeffb = (m * ((Period * sineb / pin2) - (s.x2 * cosineb / pin))) - (c * cosineb / pin);
            return coeffb - coeffa;
        }

        private void DrawLineOnPB(Graphics G, PictureBox P, Pen C, double X0, double Y0, double XA, double YA, double XL, double XR, double YT, double YB)
        {
            double U1, V1, U2, V2;
            int iU1, iV1, iU2, iV2;
            int PW = P.ClientSize.Width;
            int PH = P.ClientSize.Height;

            U1 = PW * (X0 - XL) / (XR - XL);
            U2 = PW * (XA - XL) / (XR - XL);
            V1 = PH * (Y0 - YT) / (YB - YT);
            V2 = PH * (YA - YT) / (YB - YT);

            iU1 = (int)U1;
            iV1 = (int)V1;
            iU2 = (int)U2;
            iV2 = (int)V2;

            G.DrawLine(C, iU1, iV1, iU2, iV2);

        }


    }

    // user defined exception to throw in the event of any segments being vertical
    // resulting in an infinite slope when we go to calculate the coefficients
    public class VerticalSegmentException: Exception
    {
        public VerticalSegmentException(string message)
            :base (message)
        {

        }
    }

    // user defined exception to throw in the event of any segments being out of
    // the range of the period, i.e. (-Period to +Period)
    public class NodesOutOfRangeException: Exception
    {
        public NodesOutOfRangeException(string message)
            :base(message)
        {

        }
    }
}
