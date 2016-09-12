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
    private int dx = 5,dy=5;
    private bool isDraw;

 
    private void InitializeAlgorithm()
    {
        this.obstacles = new List<Point[]>();
        this.obstaclesUI = new List<Point[]>(); 
        readFromFile();
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
            poly = new Point[pts];

            for (int j = 0; j < pts; j++)
            {
                str = scan.ReadLine().Split();
                poly[j] = (new Point(Convert.ToInt32(str[0]), Convert.ToInt32(str[1])));
            }
            obstacles.Add(poly);

            for (int j = 0; j < pts; j++)
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
        int newX, newY;
        newX = picBall.Location.X + dx;
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
        this.txtX.Text = newX.ToString();
        this.txtY.Text = newY.ToString();
        this.clickFrame.Invalidate(true);
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
