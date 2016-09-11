using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

public class Form1 : System.Windows.Forms.Form
{
    private IContainer components; 
   
    private System.Windows.Forms.PictureBox clickFrame;
    private System.Windows.Forms.PictureBox picTarget;
    private System.Windows.Forms.PictureBox picBall;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TextBox txtX;
    private System.Windows.Forms.TextBox txtY;
    private System.Windows.Forms.Button newObjBtn;

    private int dx = 4;
    private bool isDraw;

    public Form1()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.clickFrame = new System.Windows.Forms.PictureBox();
            this.picTarget = new System.Windows.Forms.PictureBox();
            this.picBall = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.newObjBtn = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.clickFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBall)).BeginInit();
            this.SuspendLayout();

            // 
            // clickFrame
            // 
            this.clickFrame.Location = new System.Drawing.Point(317, 50);
            this.clickFrame.Name = "clickFrame";
            this.clickFrame.Size = new System.Drawing.Size(388, 285);
            this.clickFrame.TabIndex = 0;
            this.clickFrame.TabStop = false;
            this.clickFrame.Click += new System.EventHandler(this.clickFrame_Click);
            // 
            // picTarget
            // 
            this.picTarget.BackColor = System.Drawing.Color.Red;
            this.picTarget.Location = new System.Drawing.Point(160, 240);
            this.picTarget.Name = "picTarget";
            this.picTarget.Size = new System.Drawing.Size(56, 56);
            this.picTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTarget.TabIndex = 0;
            this.picTarget.TabStop = false;
            // 
            // picBall
            // 
            this.picBall.ImageLocation = "Resources/bug.jpg";
            this.picBall.Location = new System.Drawing.Point(98, 211);
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
            this.txtX.Location = new System.Drawing.Point(30, 100);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 20);
            this.txtX.TabIndex = 2;
            this.txtX.Text = "24";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(30, 150);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 20);
            this.txtY.TabIndex = 3;
            this.txtY.Text = "136";
            // 
            // newObj
            // 
            this.newObjBtn.Location = new System.Drawing.Point(30, 50);
            this.newObjBtn.Name = "newObj";
            this.newObjBtn.Size = new System.Drawing.Size(100, 23);
            this.newObjBtn.TabIndex = 0;
            this.newObjBtn.Text = "Add obstacle";
            this.newObjBtn.Click += new System.EventHandler(this.newObj_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 438);
            this.Controls.Add(this.clickFrame);
            this.Controls.Add(this.picBall);
            this.Controls.Add(this.picTarget);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.newObjBtn);
            this.Name = "Form1";
            this.Text = "Crasher";

            ((System.ComponentModel.ISupportInitialize)(this.clickFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    [STAThread]
    static void Main()
    {
        Application.Run(new Form1());
    }

    private void timer1_Tick(object sender, System.EventArgs e)
    {
        int newX, newY;
        newX = picBall.Location.X + dx;
        newY = picBall.Location.Y + dx;

        if (newX > this.Width - picBall.Width)
        {
            dx = -dx;
        }

        if (newX < 0)
        {
            dx = -dx;
        }

        if (picBall.Bounds.IntersectsWith(picTarget.Bounds))
        {
            this.BackColor = Color.Black;
        }
        else
        {
            this.BackColor = Color.White;
        }

        picBall.Location = new Point(newX, newY);
        this.txtX.Text = newX.ToString();
        this.txtY.Text = newY.ToString();
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
