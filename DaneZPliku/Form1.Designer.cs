namespace DaneZPlikuOkienko
{
    partial class DaneZPliku
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnWybierzSystemDecyzyjny = new System.Windows.Forms.Button();
            this.tbSciezkaDoSystemuDecyzyjnego = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbSystemDecyzyjny = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnWybierzSystemDecyzyjny
            // 
            this.btnWybierzSystemDecyzyjny.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWybierzSystemDecyzyjny.Location = new System.Drawing.Point(1036, 10);
            this.btnWybierzSystemDecyzyjny.Margin = new System.Windows.Forms.Padding(2);
            this.btnWybierzSystemDecyzyjny.Name = "btnWybierzSystemDecyzyjny";
            this.btnWybierzSystemDecyzyjny.Size = new System.Drawing.Size(32, 19);
            this.btnWybierzSystemDecyzyjny.TabIndex = 0;
            this.btnWybierzSystemDecyzyjny.Text = "...";
            this.btnWybierzSystemDecyzyjny.UseVisualStyleBackColor = true;
            this.btnWybierzSystemDecyzyjny.Click += new System.EventHandler(this.btnWybierzSystemDecyzyjny_Click);
            // 
            // tbSciezkaDoSystemuDecyzyjnego
            // 
            this.tbSciezkaDoSystemuDecyzyjnego.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSciezkaDoSystemuDecyzyjnego.Location = new System.Drawing.Point(176, 10);
            this.tbSciezkaDoSystemuDecyzyjnego.Margin = new System.Windows.Forms.Padding(2);
            this.tbSciezkaDoSystemuDecyzyjnego.Name = "tbSciezkaDoSystemuDecyzyjnego";
            this.tbSciezkaDoSystemuDecyzyjnego.ReadOnly = true;
            this.tbSciezkaDoSystemuDecyzyjnego.Size = new System.Drawing.Size(858, 20);
            this.tbSciezkaDoSystemuDecyzyjnego.TabIndex = 1;
            this.tbSciezkaDoSystemuDecyzyjnego.Click += new System.EventHandler(this.btnWybierzSystemDecyzyjny_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ścieżka do systemu decyzyjnego";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStart.Location = new System.Drawing.Point(486, 422);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(104, 35);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Pracuj";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbSystemDecyzyjny
            // 
            this.tbSystemDecyzyjny.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSystemDecyzyjny.Location = new System.Drawing.Point(9, 51);
            this.tbSystemDecyzyjny.Margin = new System.Windows.Forms.Padding(2);
            this.tbSystemDecyzyjny.Multiline = true;
            this.tbSystemDecyzyjny.Name = "tbSystemDecyzyjny";
            this.tbSystemDecyzyjny.Size = new System.Drawing.Size(320, 408);
            this.tbSystemDecyzyjny.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(334, 51);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(731, 366);
            this.textBox1.TabIndex = 10;
            // 
            // DaneZPliku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 470);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tbSystemDecyzyjny);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSciezkaDoSystemuDecyzyjnego);
            this.Controls.Add(this.btnWybierzSystemDecyzyjny);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(529, 413);
            this.Name = "DaneZPliku";
            this.Text = "Drzewo Decyzyjne";

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWybierzSystemDecyzyjny;
        private System.Windows.Forms.TextBox tbSciezkaDoSystemuDecyzyjnego;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbSystemDecyzyjny;
        private System.Windows.Forms.TextBox textBox1;
    }
}

