using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

public partial class Form1 : System.Windows.Forms.Form
{
    private bool debug = false;
    private int bugType;
    private List<Point[]> obstacles;
    Dictionary<Point[], Line[]> edges;
    private Point pos,end;
    private PointF startF, endF, posF, velF, checkpoint,target,breakoff;
    private double theta,pathLength=0,dist;
    private int moveMode;
    private enum moveType { Free, Boundary};
    private float speed;
    private int bugRadius;
    private Point[] contact;
    private int index;
    private const float eps = 1.5F,del = 0.001F;
    //int frameCount = 0;
    private Line l00;
    string[] dirs;
    private void InitializeAlgorithm()
    {
        string[] dirs0 = System.IO.Directory.GetFiles(System.Environment.CurrentDirectory, "obs*.txt");
        string[] dirs1 = new string[0];
        try
        {
            dirs1 = System.IO.Directory.GetFiles(System.Environment.CurrentDirectory + "\\Resources", "obs*.txt");
        }
        catch (Exception e)
        { }
        dirs = new string[dirs0.Length+dirs1.Length];
        for (int i = 0; i < dirs0.Length; i++)
            dirs[i] = dirs0[i].Replace(System.Environment.CurrentDirectory + "\\", "");
        for (int i = 0; i < dirs1.Length; i++)
            dirs[i+dirs0.Length] = dirs1[i].Replace(System.Environment.CurrentDirectory + "\\", "");
        //if (dirs.Length == 0)
        //{
        //    throw new Exception("No input obstacle files found in application directory!");
       // }
        this.bugType = 0;
        this.bugRadius = 10;
        this.speed = 0.9F;
        if (dirs.Length >= 1)
            readFromFile(dirs[0]);
        else
            readFromFile("");
        startF = new Point(50, 50);
        endF = new Point(450, 400);
        posF = startF;
        pos.X = (int)posF.X;    pos.Y = (int)posF.Y;
        end.X = (int)endF.X;    end.Y = (int)endF.Y;
        l00 = new Line(startF, endF);
        moveMode = (int)moveType.Free;
        checkpoint = startF;
    }
    
    private void timer1_Tick(object sender, System.EventArgs e)
    {
       // Console.WriteLine("tim {0} {1}  {2} {3}", target.X, target.Y, breakoff.X, breakoff.Y);
        theta = Math.Atan2(target.Y - posF.Y, target.X - posF.X);
        velF.X = speed * (float)Math.Cos(theta);
        velF.Y = speed * (float)Math.Sin(theta);
        posF.X += velF.X;
        posF.Y += velF.Y;
        pos.X = (int)posF.X;
        pos.Y = (int)posF.Y;
        pathLength += speed;
        if (moveMode == (int)moveType.Free)
        {
            obstacleDetection();
        }
        else
        {
            if (bugType == 0)
                bug0();
            else if (bugType == 1)
                bug1();
            else if (bugType == 2)
                bug2();
        }

        if (reached(posF, endF))
        {
            pathLength += distance(posF, end);
            posF = endF;
            pos.X = (int)posF.X;
            pos.Y = (int)posF.Y;
            timer1.Stop();
            this.txtLen.Font = new Font(txtLen.Font, FontStyle.Bold);
            this.pauseBtn.Text = "Done";
        }

        this.update_text();
        this.clickFrame.Invalidate(true);
    }

    private void bug0() 
    {
        if (reached(posF, target))
        {
            pathLength += distance(posF, target);
            posF = target;
            this.clickFrame.Invalidate(true);
            obstacleDetection();
        }
    }

    private void bug1()
    { }

    private void bug2()
    {
     //     Console.WriteLine(" Target {0} {1}, Break-off {0} {1}", target.X, target.Y, breakoff.X, breakoff.Y);
        if (reached(posF, target))
        {
            pathLength += distance(posF, target);
            posF = target;
            this.clickFrame.Invalidate(true);
            index = (index + 1) %(contact.Length-1);
            target = contact[index];
            Console.WriteLine("{0}x+{1}y+{2}=0 ; {3}x+{4}y+{5}=0", l00.a, l00.b, l00.c, 
                edges[contact][index].a,edges[contact][index].b, edges[contact][index].c);
            breakoff = intersect(l00, edges[contact][index]);
            Console.WriteLine("Point: {0} {1}", breakoff.X, breakoff.Y);
        }
        else if (reached(posF, breakoff))
        {
            if (distance(posF, endF) < dist)
            {
                pathLength += distance(posF, breakoff);
                posF = breakoff;
                this.clickFrame.Invalidate(true);
                obstacleDetection();
            }
        }
    }


    struct Line 
    {
        public float a, b, c;
        public Line(float x, float y, float z)
        {
            this.a = x;
            this.b = y;
            this.c = z;
        }
        public Line(PointF a, PointF b)
        {
            this.a = a.Y - b.Y;
            this.b = b.X - a.X;
            this.c = a.X * b.Y - b.X * a.Y;
        }
    };

    bool isParallel(Line l1, Line l2)
    {
        return (l1.a * l2.b == l1.b * l2.a);
    }

    PointF intersect(Line l1, Line l2)
    {
        PointF x=new PointF(0,0);
        x.X = (l1.b * l2.c - l2.b * l1.c) / (l1.a * l2.b - l2.a * l1.b);
        x.Y = (l1.a * l2.c - l2.a * l1.c) / (l1.b * l2.a - l2.b * l1.a);
        return x;
    }

    PointF footOfPerp(Line l, PointF p)
    {
        PointF fop = new PointF();
        fop.X = p.X - ((l.a * (l.a * p.X + l.b * p.Y + l.c)) / (l.a * l.a + l.b * l.b));
        fop.Y = p.Y - ((l.b * (l.a * p.X + l.b * p.Y + l.c)) / (l.a * l.a + l.b * l.b));
        return fop;
    }

    float distance(PointF a, PointF b)
    {
        float x1=a.X-b.X, x2=a.Y-b.Y;
        return (float)Math.Sqrt(x1 * x1 + x2 * x2);
    }

    bool inOrder(PointF a, PointF b, PointF c, int offset)
    {
        if (distance(a, b) > (offset + distance(a, c)))
            return false;
        if (distance(c, b) > (offset + distance(a, c)))
            return false;
        return true;
    }

    PointF xPt;

    private bool isAligned(PointF a, PointF b, PointF c, Line l1, Line l2)
    {
       // Console.Write("{0}x+{1}y+{2}=0  {3}x+{4}y+{5}=0  ", l1.a, l1.b, l1.c, l2.a, l2.b, l2.c);
        if (isParallel(l1, l2))
            return false;
        xPt = intersect(l1, l2);
        if(debug)
            Console.WriteLine(" {0} {1}  {2} {3}  {4} {5}  {6} {7} ", a.X, a.Y, c.X, c.Y, b.X, b.Y, xPt.X, xPt.Y); 
        if (!inOrder(a, xPt, c, 0))
            return false;
        if (!inOrder(b, xPt, end, 0))
            return false;
        if (distance(xPt, b) > 2 * eps)
            return false;
        if (distance(xPt, c) < eps)
            return false;
        return true;
    }

    private void obstacleDetection()
    {
        target = end;
        Line toOrigin = new Line(posF, end);
        foreach (Point[] obs in obstacles)
        {
            Line[] edg = edges[obs];
            for (int i = 0; i < edg.Length; i++){
            //    Console.WriteLine(" ");
            //    Console.WriteLine(" {6} :: {0} {1}  {2} {3}  {4} {5} ", obs[i].X, obs[i].Y, obs[i+1].X, obs[i+1].Y, posF.X, posF.Y,i);
                if (!isAligned(obs[i], posF, obs[i+1],edg[i],toOrigin))
                    continue;
                if (distance(xPt,posF)<eps)
                    if (!PointinPoly(obs, extendedPoint(xPt, end, -2*eps)))
                        continue;
                contact = obs;
                index = i;
                if (debug)
                    Console.WriteLine("Intersection found! {0} {1}",index,contact.Length);
                moveMode = (int)moveType.Boundary;
                checkpoint = posF;
                pathLength += distance(posF, xPt);
                posF = xPt;
                target = obs[i+1];
                breakoff = endF;
                dist = distance(posF, endF);
                Console.WriteLine("obs det {0} {1}  {2} {3}", target.X, target.Y, breakoff.X, breakoff.Y);
                return;
            }
        }
        moveMode = (int)moveType.Free;
    }

    bool reached(PointF a, PointF b)
    {
        a.X -= b.X;
        a.Y -= b.Y;
        double dist = Math.Sqrt(a.X * a.X + a.Y * a.Y);
        if (dist > eps)
            return false;

        //Console.WriteLine("Reached Destination!");
        //if (a.X * velF.X + a.Y * velF.Y >= -eps)
        return true;
        
    }

    private bool isAligned_withRadius(PointF a, PointF b, PointF c)
    {
    //    System.Console.WriteLine("!!! a = {0},{1}  b = {2},{3} c = {4},{5}", a.X, a.Y, b.X, b.Y, c.X, c.Y);
        Line l1,l2;
        l1 = new Line(a,c);
        l2 = new Line(b,endF);
        PointF tmp1 = footOfPerp(l1, b);
        if (distance(tmp1, b) < bugRadius && inOrder(a,b,c,0))
        {
            Console.Write(" correction: {0} {1}", posF.X, posF.Y);
            posF = extendedPoint(tmp1, b, -bugRadius*1.05F);
            //noContact = false;
            Console.WriteLine(" : {0} {1}", posF.X, posF.Y);
        //    return false;
        }
        if (isParallel(l1, l2))
            return false;
        xPt = intersect(l1, l2);
      //  System.Console.WriteLine(" {0},{1} ", pt.X, pt.Y);
        if(!inOrder(a,xPt,c,bugRadius))
        if(distance(b,endF) > (bugRadius+distance(xPt,endF)))
            return false;
        if(distance(b,endF) < distance(xPt,endF))
            return false;
   //     System.Console.WriteLine("!!! a = {0},{1}  b = {2},{3} c = {4},{5}",a.X,a.Y,b.X,b.Y,c.X,c.Y);
        return true;
    }

    private void obstacleDetection_withRadius()
    {
        foreach (Point[] obs in obstacles)
        {
            for (int i = 1; i < obs.Length; i++)
                if (isAligned_withRadius(obs[i - 1], posF, obs[i]))
                {
                    contact = obs;
                    index = i;
                    moveMode = (int)moveType.Boundary;
                    checkpoint = posF;
                    posF = extendedPoint(xPt, end, bugRadius);
                    Console.WriteLine("{0} {1} :: {2} {3}", xPt.X, xPt.Y, posF.X, posF.Y);
                    target = similarPoint(extendedPoint(obs[i], xPt, bugRadius * 1.1F));
                    //     timer1.Stop();
                    return;
                }
        }
        moveMode = (int)moveType.Free;
    }

    PointF similarPoint(PointF a)
    {
        return new PointF(a.X + pos.X - xPt.X,a.Y + pos.Y - xPt.Y);
    }

    PointF extendedPoint(PointF a, PointF b, float dist)
    {
        double tht = Math.Atan2(a.Y - b.Y, a.X - b.X);
        dist += distance(a, b);
        return new PointF(b.X+dist*(float)Math.Cos(tht),b.Y+dist*(float)Math.Sin(tht));
    }
    
    bool reached_withRadius(PointF a, PointF b)
    {
        a.X-=b.X;
        a.Y-=b.Y;
        double dist = Math.Sqrt(a.X * a.X + a.Y * a.Y);
        if (dist > (double)bugRadius)
            return false;
        if (a.X * velF.X + a.Y * velF.Y < 0)
            return true;
        else
            return false;
    }

    private bool PointinPoly(Point[] obs, PointF pt) 
    {
        Console.WriteLine("Checking {0} {1}", pt.X, pt.Y);
        bool ans = false;
        for (int i = 1; i < obs.Length; i++)
        {
            if (!inOrder(obs[i - 1].Y, pt.Y, obs[i].Y))
                continue;
            if (obs[i - 1].X > pt.X && obs[i].X > pt.X)
                continue;
            if (!(obs[i - 1].X <= pt.X && obs[i].X <= pt.X))
            {
                float x3=obs[i].X+(obs[i].X-obs[i-1].X)*(pt.Y-obs[i].Y)/(obs[i].Y-obs[i-1].Y);
                if (x3 >= pt.X)
                    continue;
            }
            ans ^= true;
        }
        return ans;
    }

    bool inOrder(float a, float b, float c)
    {
        if (a <= b && b <= c)
            return true;
        if (a >= b && b >= c)
            return true;
        return false;
    }

    private void readFromFile(string path)
    {
        this.obstacles = new List<Point[]>();
        this.edges = new Dictionary<Point[], Line[]>();
        this.obstaclesUI = new List<Point[]>();
        try
        {
            System.IO.StreamReader scan = new System.IO.StreamReader(path);
            string line = scan.ReadLine();
            string[] str;
            Point[] poly, polyUI;
            Line[] edg;
            int obstacleCount = Convert.ToInt32(line), pts;
            Console.WriteLine("Obstacles count: {0}", obstacleCount);
            for (int i = 0; i < obstacleCount; i++)
            {
                line = scan.ReadLine();
                pts = Convert.ToInt32(line);
                poly = new Point[pts + 1];
                polyUI = new Point[pts + 1];
                edg = new Line[pts];
                for (int j = 0; j < pts; j++)
                {
                    str = scan.ReadLine().Split();
                    poly[j] = (new Point(Convert.ToInt32(str[0]), Convert.ToInt32(str[1])));
                }
                poly[pts] = poly[0];
                obstacles.Add(poly);

                for (int j = 0; j <= pts; j++)
                {
                    polyUI[j].X = X(poly[j].X);
                    polyUI[j].Y = Y(poly[j].Y);
                }
                obstaclesUI.Add(polyUI);

                for (int j = 0; j < pts; j++)
                {
                    edg[j] = new Line(poly[j], poly[j + 1]);
                }
                edges.Add(poly, edg);
            }
            scan.Close();
        }
        catch (Exception e) 
        {
            Console.WriteLine("No files found!");
        }
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
