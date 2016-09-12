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
    private System.Windows.Forms.TextBox txtX;
    private System.Windows.Forms.TextBox txtY;
    private System.Windows.Forms.TextBox comment;
    private System.Windows.Forms.Button newObjBtn;
    private List<Point[]> obstaclesUI;
    private System.Drawing.SolidBrush obsBrush;
    private System.Drawing.SolidBrush bugBrush;
    private System.Drawing.SolidBrush endBrush;
    private int clickMode;
    private int bugRadius = 5;
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
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.comment = new System.Windows.Forms.TextBox();
            this.newObjBtn = new System.Windows.Forms.Button();
            this.clickMode = (int)clickType.NONE;

            ((System.ComponentModel.ISupportInitialize)(this.clickFrame)).BeginInit();
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
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(25, 150);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 20);
            this.txtY.TabIndex = 3;
            // 
            // comment
            // 
            this.comment.Location = new System.Drawing.Point(25, 200);
            this.comment.Name = "comment";
            this.comment.Size = new System.Drawing.Size(100, 20);
            this.comment.TabIndex = 4;
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
            this.StartPosition = FormStartPosition.WindowsDefaultLocation;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.clickFrame.Paint+=new PaintEventHandler(this.drawObstacles);
            this.Controls.Add(this.clickFrame);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.newObjBtn);
            this.Name = "Form1";
            this.Text = "Path planning: Bug Algorithm";
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
        return  450-y;
    }
    
    private void drawObstacles(object sender, PaintEventArgs e)
    {
        foreach(Point[] obstacle in obstaclesUI)
            e.Graphics.FillPolygon(obsBrush, obstacle);
        e.Graphics.FillEllipse(bugBrush, X(pos.X - bugRadius), Y(pos.Y - bugRadius), 2 * bugRadius, 2 * bugRadius);
        e.Graphics.FillEllipse(endBrush, X(end.X - bugRadius), Y(end.Y - bugRadius), 2 * bugRadius, 2 * bugRadius);
    }

    private void newObj_Click(object sender, EventArgs e)
    {
        this.clickMode = (int)clickType.OBS;
    }

    private void clickFrame_Click(object sender, EventArgs e)
    {
        // https://msdn.microsoft.com/en-us/library/system.windows.forms.control.mouseclick.aspx
    }
}
