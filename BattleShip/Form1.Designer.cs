namespace BattleShip
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
            this.btnConsola = new System.Windows.Forms.Button();
            this.txtConsola = new System.Windows.Forms.TextBox();
            this.btnMain1 = new System.Windows.Forms.Button();
            this.btnMain2 = new System.Windows.Forms.Button();
            this.btnMain3 = new System.Windows.Forms.Button();
            this.btn4Test = new System.Windows.Forms.Button();
            this.btn4Main = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConsola
            // 
            this.btnConsola.Location = new System.Drawing.Point(12, 12);
            this.btnConsola.Name = "btnConsola";
            this.btnConsola.Size = new System.Drawing.Size(74, 22);
            this.btnConsola.TabIndex = 0;
            this.btnConsola.Text = "Clear cons.";
            this.btnConsola.UseVisualStyleBackColor = true;
            this.btnConsola.Click += new System.EventHandler(this.btnConsola_Click);
            // 
            // txtConsola
            // 
            this.txtConsola.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtConsola.Location = new System.Drawing.Point(93, 12);
            this.txtConsola.Multiline = true;
            this.txtConsola.Name = "txtConsola";
            this.txtConsola.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsola.Size = new System.Drawing.Size(915, 406);
            this.txtConsola.TabIndex = 1;
            // 
            // btnMain1
            // 
            this.btnMain1.Location = new System.Drawing.Point(12, 52);
            this.btnMain1.Name = "btnMain1";
            this.btnMain1.Size = new System.Drawing.Size(75, 23);
            this.btnMain1.TabIndex = 2;
            this.btnMain1.Text = "Main";
            this.btnMain1.UseVisualStyleBackColor = true;
            this.btnMain1.Click += new System.EventHandler(this.btnMain1_Click);
            // 
            // btnMain2
            // 
            this.btnMain2.Location = new System.Drawing.Point(12, 92);
            this.btnMain2.Name = "btnMain2";
            this.btnMain2.Size = new System.Drawing.Size(75, 23);
            this.btnMain2.TabIndex = 3;
            this.btnMain2.Text = "Main2";
            this.btnMain2.UseVisualStyleBackColor = true;
            this.btnMain2.Click += new System.EventHandler(this.btnMain2_Click);
            // 
            // btnMain3
            // 
            this.btnMain3.Location = new System.Drawing.Point(12, 133);
            this.btnMain3.Name = "btnMain3";
            this.btnMain3.Size = new System.Drawing.Size(75, 23);
            this.btnMain3.TabIndex = 4;
            this.btnMain3.Text = "Main3";
            this.btnMain3.UseVisualStyleBackColor = true;
            this.btnMain3.Click += new System.EventHandler(this.btnMain3_Click);
            // 
            // btn4Test
            // 
            this.btn4Test.Location = new System.Drawing.Point(11, 174);
            this.btn4Test.Name = "btn4Test";
            this.btn4Test.Size = new System.Drawing.Size(75, 23);
            this.btn4Test.TabIndex = 5;
            this.btn4Test.Text = "P4 Battle";
            this.btn4Test.UseVisualStyleBackColor = true;
            this.btn4Test.Click += new System.EventHandler(this.btnMain4_Click);
            // 
            // btn4Main
            // 
            this.btn4Main.Location = new System.Drawing.Point(12, 213);
            this.btn4Main.Name = "btn4Main";
            this.btn4Main.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn4Main.Size = new System.Drawing.Size(75, 23);
            this.btn4Main.TabIndex = 6;
            this.btn4Main.Text = "Main4";
            this.btn4Main.UseVisualStyleBackColor = true;
            this.btn4Main.Click += new System.EventHandler(this.btn4Main_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 430);
            this.Controls.Add(this.btn4Main);
            this.Controls.Add(this.btn4Test);
            this.Controls.Add(this.btnMain3);
            this.Controls.Add(this.btnMain2);
            this.Controls.Add(this.btnMain1);
            this.Controls.Add(this.txtConsola);
            this.Controls.Add(this.btnConsola);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnConsola;
        private TextBox txtConsola;
        private Button btnMain1;
        private Button btnMain2;
        private Button btnMain3;
        private Button btn4Test;
        private Button btn4Main;
    }
}