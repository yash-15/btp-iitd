using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

public partial class Form1 : System.Windows.Forms.Form
{
    private List< Point[] > obstacles;
    private Point pos, delta, start, end;
    private PointF posF, velF, checkpoint;
    private double theta, theta0;
    private int moveMode;
    private enum moveType { Free, Boundary};
    private float speed = 5.0F;
    private Point[] contact;
    private int index;
    private void InitializeAlgorithm()
    {
        this.obstacles = new List<Point[]>();
        this.obstaclesUI = new List<Point[]>(); 
        readFromFile();
        start = new Point(300, 100);
        end = new Point(300, 400);
        theta0 = Math.Atan2(end.Y - start.Y, end.X - start.X);
        posF = pos = start;
        moveMode = (int)moveType.Free;
        checkpoint = start;
    }

    private void readFromFile()
    {
        System.IO.StreamReader scan = new System.IO.StreamReader("Resources/obstacles.txt");
        string line=scan.ReadLine();
        string[] str;
        Point[] poly;
        int obstacleCount = Convert.ToInt32(line),pts;
        for (int i = 0; i < obstacleCount; i++)
        {
            line = scan.ReadLine();
            pts = Convert.ToInt32(line);
            poly = new Point[pts+1];

            for (int j = 0; j < pts; j++)
            {
                str = scan.ReadLine().Split();
                poly[j] = (new Point(Convert.ToInt32(str[0]), Convert.ToInt32(str[1])));
            }
            poly[pts] = poly[0];
            obstacles.Add(poly);

            for (int j = 0; j <= pts; j++)
            {
                poly[j].X = X(poly[j].X);
                poly[j].Y = Y(poly[j].Y);
            }
            obstaclesUI.Add(poly);
        }
        scan.Close();
    }

    private void timer1_Tick(object sender, System.EventArgs e)
    {
        if (moveMode == (int)moveType.Free)
        {
            velF.X = speed * (float)Math.Cos(theta0);
            velF.Y = speed * (float)Math.Sin(theta0);
            posF.X += velF.X;
            posF.Y += velF.Y;
            pos.X = (int)posF.X;
            pos.Y = (int)posF.Y;

            obstacleDetection1();
        }
        else
        {
            theta = Math.Atan2(contact[index].Y - posF.Y, contact[index].X - posF.X);
            velF.X = speed * (float)Math.Cos(theta);
            velF.Y = speed * (float)Math.Sin(theta);
            posF.X += velF.X;
            posF.Y += velF.Y;
            pos.X = (int)posF.X;
            pos.Y = (int)posF.Y;

            //TODO: boundary case translation
        }

        if (reached(posF, end))
            timer1.Stop();

       /* int newX, newY;
       /* newX = picBall.Location.X + dx;
        newY = picBall.Location.Y + dy;

        if (newX > this.ClientSize.Width - picBall.Width)
            dx = -dx;
        if (newX < 0)
            dx = -dx;
        if (newY > this.ClientSize.Height - picBall.Height)
            dy = -dy;
        if (newY < 0)
            dy = -dy;

        picBall.Location = new Point(newX, newY);
        */
        this.txtX.Text = pos.X.ToString();
        this.txtY.Text = pos.Y.ToString();
        this.comment.Text = moveMode.ToString();
        this.clickFrame.Invalidate(true);
    }

    private bool isCollinear(PointF a, PointF b, PointF c)
    {
        return false;
        b.X-=a.X;   b.Y-=a.Y;
        c.X-=a.X;   c.Y-=a.Y;

        if (b.X*c.X<0 || b.Y*c.Y<0)
                return false;

        double dist = Math.Abs(b.Y * c.X - b.X - c.Y)/Math.Sqrt(c.X*c.X+c.Y*c.Y);

        return (dist < (double)bugRadius);
    }

    private void obstacleDetection1()
    {
        foreach (Point[] obs in obstacles)
        {
            for (int i = 1; i < obs.Length; i++)
                if (isCollinear(obs[i - 1], posF, obs[i]))
                {
                    contact = obs;
                    index = i;
                    moveMode = (int)moveType.Boundary;
                    checkpoint = posF;
                    return;
                }
        }
    }

    bool reached(PointF a, PointF b)
    {
        a.X-=b.X;
        a.Y-=b.Y;
        double dist = Math.Sqrt(a.X * a.X + a.Y * a.Y);
        return (dist < (double)bugRadius);
    }
    //private void newObj_Click(object sender, EventArgs e)
    //{
    //    isDraw = true;
    //}

    //private void clickFrame_Click(object sender, EventArgs e)
    //{
    //   // https://msdn.microsoft.com/en-us/library/system.windows.forms.control.mouseclick.aspx
    //}
}
