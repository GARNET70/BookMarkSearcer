namespace BookMarkSearcer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            textBox3 = new TextBox();
            label4 = new Label();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(162, 30);
            label1.TabIndex = 0;
            label1.Text = "BookMarkFolder";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 42);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(665, 35);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 92);
            label2.Name = "label2";
            label2.Size = new Size(117, 30);
            label2.TabIndex = 2;
            label2.Text = "WorkFolder";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 125);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(665, 35);
            textBox2.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 176);
            label3.Name = "label3";
            label3.Size = new Size(126, 30);
            label3.TabIndex = 4;
            label3.Text = "TargetFolder";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 209);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(665, 38);
            comboBox1.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(712, 37);
            button1.Name = "button1";
            button1.Size = new Size(131, 40);
            button1.TabIndex = 6;
            button1.Text = "Read";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(712, 120);
            button2.Name = "button2";
            button2.Size = new Size(131, 40);
            button2.TabIndex = 7;
            button2.Text = "Get";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(712, 207);
            button3.Name = "button3";
            button3.Size = new Size(131, 40);
            button3.TabIndex = 8;
            button3.Text = "Search";
            button3.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 305);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(831, 95);
            textBox3.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 263);
            label4.Name = "label4";
            label4.Size = new Size(70, 30);
            label4.TabIndex = 10;
            label4.Text = "Result";
            // 
            // button4
            // 
            button4.Location = new Point(12, 419);
            button4.Name = "button4";
            button4.Size = new Size(185, 53);
            button4.TabIndex = 11;
            button4.Text = "ACCESS";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(227, 419);
            button5.Name = "button5";
            button5.Size = new Size(185, 53);
            button5.TabIndex = 12;
            button5.Text = "OPEN";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(443, 419);
            button6.Name = "button6";
            button6.Size = new Size(185, 53);
            button6.TabIndex = 13;
            button6.Text = "MOVE";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(658, 419);
            button7.Name = "button7";
            button7.Size = new Size(185, 53);
            button7.TabIndex = 14;
            button7.Text = "COMPLETE";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(855, 484);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private Label label3;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox textBox3;
        private Label label4;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
    }
}
