using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

public partial class Form1 : System.Windows.Forms.Form
{
    private IContainer components;

    private System.Windows.Forms.PictureBox clickFrame;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Label txtX;
    private System.Windows.Forms.Label txtY;
    private System.Windows.Forms.Label txtLen;
    private System.Windows.Forms.Label txtTitle;
    private System.Windows.Forms.Button newObjBtn;
    private System.Windows.Forms.Button StartPosBtn;
    private System.Windows.Forms.Button EndPosBtn;
    private System.Windows.Forms.Button pauseBtn;
    private List<Point[]> obstaclesUI;
    private System.Drawing.SolidBrush obsBrush;
    private System.Drawing.SolidBrush bugBrush;
    private System.Drawing.SolidBrush endBrush;
    private int clickMode;
    private enum clickType {NONE,OBS,INITIAL,FINAL};

    public Form1()
    {
        InitializeAlgorithm();
        InitializeComponent();
        System.Console.WriteLine("width: {0} height {1} ", this.Width, this.Height);
    }

    private void InitializeComponent()
    {
            this.obsBrush = new SolidBrush(Color.FromArgb(76,182,235));
            this.bugBrush = new SolidBrush(Color.Red);
            this.endBrush = new SolidBrush(Color.Green); 

            this.components = new System.ComponentModel.Container();
            this.clickFrame = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtX = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.Label();
            this.txtLen = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.Label();
            this.newObjBtn = new System.Windows.Forms.Button();
            this.StartPosBtn = new System.Windows.Forms.Button();
            this.EndPosBtn = new System.Windows.Forms.Button();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.clickMode = (int)clickType.NONE;

            ((System.ComponentModel.ISupportInitialize)(this.clickFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // clickFrame
            // 
            this.clickFrame.Location = new System.Drawing.Point(200, 50);
            this.clickFrame.Name = "clickFrame";
            this.clickFrame.Size = new System.Drawing.Size(505, 505);
            this.clickFrame.TabIndex = 0;
            this.clickFrame.TabStop = false;
            this.clickFrame.Click += new System.EventHandler(this.clickFrame_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.timer1.Interval = 5;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(25, 320);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 30);
            this.txtX.Font = new Font(this.txtX.Font.Name, 11);
            this.txtX.Text = "X: " + posF.X.ToString("0.00");
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(25, 350);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 30);
            this.txtY.Font = new Font(this.txtY.Font.Name, 11);
            this.txtY.Text = "Y: " + posF.Y.ToString("0.00");
            //
            // txtLen
            // 
            this.txtLen.Location = new System.Drawing.Point(25, 400);
            this.txtLen.Name = "txtLen";
            this.txtLen.Size = new System.Drawing.Size(275, 30);
            this.txtLen.Font = new Font(this.txtLen.Font.Name, 11);
            this.txtLen.Text = "Path Length: " + pathLength.ToString("0.00");//
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(325, 10);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(300, 60);
            this.txtTitle.Font = new Font(this.txtLen.Font.Name, 15);
            this.txtTitle.Text = "Bug Algorithm Simulation";
            // 
            // newObjBtn
            // 
            this.newObjBtn.Location = new System.Drawing.Point(25, 250);
            this.newObjBtn.Name = "newObjBtn";
            this.newObjBtn.Size = new System.Drawing.Size(100, 23);
            this.newObjBtn.TabIndex = 3;
            this.newObjBtn.Text = "Add obstacle";
            this.newObjBtn.Click += new System.EventHandler(this.newObj_Click);
            // 
            // startPosBtn
            // 
            this.StartPosBtn.Location = new System.Drawing.Point(25, 150);
            this.StartPosBtn.Name = "startPosBtn";
            this.StartPosBtn.Size = new System.Drawing.Size(100, 23);
            this.StartPosBtn.TabIndex = 1;
            this.StartPosBtn.Text = "Set Start Position";
            this.StartPosBtn.Click += new System.EventHandler(this.startPos_Click);
            // 
            // endPosBtn
            // 
            this.EndPosBtn.Location = new System.Drawing.Point(25, 200);
            this.EndPosBtn.Name = "endPosBtn";
            this.EndPosBtn.Size = new System.Drawing.Size(100, 23);
            this.EndPosBtn.TabIndex = 2;
            this.EndPosBtn.Text = "Set Final Position";
            this.EndPosBtn.Click += new System.EventHandler(this.endPos_Click);
            // 
            // pauseBtn
            // 
            this.pauseBtn.Location = new System.Drawing.Point(25, 100);
            this.pauseBtn.Name = "newObjBtn";
            this.pauseBtn.Size = new System.Drawing.Size(100, 23);
            this.pauseBtn.TabIndex = 0;
            this.pauseBtn.Text = "Start";
            this.pauseBtn.Click += new System.EventHandler(this.pause_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.White;
            this.StartPosition = FormStartPosition.WindowsDefaultLocation;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.clickFrame.Paint+=new PaintEventHandler(this.drawObstacles);
            this.Controls.Add(this.clickFrame);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtLen);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.newObjBtn);
            this.Controls.Add(this.StartPosBtn);
            this.Controls.Add(this.EndPosBtn);
            this.Controls.Add(this.pauseBtn);
            this.Name = "Form1";
            this.Text = "Motion planning: Bug Algorithm";
            ((System.ComponentModel.ISupportInitialize)(this.clickFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    public static void Main()
    {
        Application.Run(new Form1());
    }

    private int X(int x)
    {
        return x;
    }
    private int Y(int y)
    {
        return  500-y;
    }
    
    private void drawObstacles(object sender, PaintEventArgs e)
    {
        e.Graphics.FillEllipse(endBrush, X(end.X - bugRadius), Y(end.Y + bugRadius), 2 * bugRadius, 2 * bugRadius);
        e.Graphics.FillEllipse(bugBrush, X(pos.X - bugRadius), Y(pos.Y + bugRadius), 2 * bugRadius, 2 * bugRadius);
        foreach (Point[] obstacle in obstaclesUI)
            e.Graphics.DrawLines(Pens.Black, obstacle);
        e.Graphics.DrawLine(Pens.Blue, new Point(X(0), Y(0)), new Point(X(500), Y(0)));
        e.Graphics.DrawLine(Pens.Blue, new Point(X(500), Y(0)), new Point(X(500), Y(500)));
        e.Graphics.DrawLine(Pens.Blue, new Point(X(500), Y(500)), new Point(X(0), Y(500)));
        e.Graphics.DrawLine(Pens.Blue, new Point(X(0), Y(500)), new Point(X(0), Y(0)));
    }

    private void newObj_Click(object sender, EventArgs e)
    {
        if (this.clickMode == (int)clickType.OBS)
            this.clickMode = (int)clickType.NONE;
        this.clickMode = (int)clickType.OBS;
    }
    
    private void startPos_Click(object sender, EventArgs e)
    {
        if(this.clickMode == (int)clickType.INITIAL)
            this.clickMode = (int)clickType.NONE;
        this.clickMode = (int)clickType.INITIAL;
    }
    
    private void endPos_Click(object sender, EventArgs e)
    {
        if (this.clickMode == (int)clickType.FINAL)
            this.clickMode = (int)clickType.NONE;
        this.clickMode = (int)clickType.FINAL;
    }

    private void clickFrame_Click(object sender, EventArgs e)
    {
        // https://msdn.microsoft.com/en-us/library/system.windows.forms.control.mouseclick.aspx
    }

    private void pause_Click(object sender, EventArgs e)
    {
        if (timer1.Enabled)
        {
            timer1.Stop();
            pauseBtn.Text = "Resume";
        }
        else
        {
            timer1.Enabled = true;
            pauseBtn.Text = "Pause";
        }
    }
}
