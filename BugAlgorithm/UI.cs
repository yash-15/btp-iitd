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
    private System.Windows.Forms.PictureBox picBall;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TextBox txtX;
    private System.Windows.Forms.TextBox txtY;
    private System.Windows.Forms.Button newObjBtn;
    private List<Point[]> obstaclesUI;
    private System.Drawing.SolidBrush brush;
    
    public Form1()
    {
        InitializeAlgorithm();
        InitializeComponent();
        System.Console.WriteLine("width: {0} height {1} ", this.Width, this.Height);
    }

    private void InitializeComponent()
    {
            this.brush = new SolidBrush(Color.Red);
            this.components = new System.ComponentModel.Container();
            this.clickFrame = new System.Windows.Forms.PictureBox();
            this.picBall = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.newObjBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.clickFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBall)).BeginInit();
            this.SuspendLayout();
            // 
            // clickFrame
            // 
            this.clickFrame.Location = new System.Drawing.Point(150, 50);
            this.clickFrame.Name = "clickFrame";
            this.clickFrame.Size = new System.Drawing.Size(600, 500);
            this.clickFrame.TabIndex = 0;
            this.clickFrame.TabStop = false;
            this.clickFrame.Click += new System.EventHandler(this.clickFrame_Click);
            // 
            // picBall
            // 
            this.picBall.ImageLocation = "Resources/bug.jpg";
            this.picBall.Location = new System.Drawing.Point(251, 100);
            this.picBall.Name = "picBall";
            this.picBall.Size = new System.Drawing.Size(32, 32);
            this.picBall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBall.TabIndex = 1;
            this.picBall.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(25, 100);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 20);
            this.txtX.TabIndex = 2;
            this.txtX.Text = "24";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(25, 150);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 20);
            this.txtY.TabIndex = 3;
            this.txtY.Text = "136";
            // 
            // newObjBtn
            // 
            this.newObjBtn.Location = new System.Drawing.Point(25, 50);
            this.newObjBtn.Name = "newObjBtn";
            this.newObjBtn.Size = new System.Drawing.Size(100, 23);
            this.newObjBtn.TabIndex = 0;
            this.newObjBtn.Text = "Add obstacle";
            this.newObjBtn.Click += new System.EventHandler(this.newObj_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.clickFrame.Paint+=new PaintEventHandler(this.drawObstacles);
            this.Controls.Add(this.clickFrame);
            this.Controls.Add(this.picBall);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.newObjBtn);
            this.Name = "Form1";
            this.Text = "Crasher";
            ((System.ComponentModel.ISupportInitialize)(this.clickFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBall)).EndInit();
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
        return  300-y;
    }
    
    private void drawObstacles(object sender, PaintEventArgs e)
    {
        foreach(Point[] obstacle in obstaclesUI)
        {
            e.Graphics.FillPolygon(brush, obstacle);
        }
        
    }

    private void newObj_Click(object sender, EventArgs e)
    {
        isDraw = true;
    }

    private void clickFrame_Click(object sender, EventArgs e)
    {
        // https://msdn.microsoft.com/en-us/library/system.windows.forms.control.mouseclick.aspx
    }
}
