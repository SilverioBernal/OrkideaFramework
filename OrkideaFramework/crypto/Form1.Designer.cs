namespace crypto
{
    partial class Form1
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
            this.chkHex = new System.Windows.Forms.CheckBox();
            this.chkEncrip = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.crypText1 = new System.Windows.Forms.TextBox();
            this.btnEncryp = new System.Windows.Forms.Button();
            this.btnDecryp = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.crypText2 = new System.Windows.Forms.TextBox();
            this.chkDecryp = new System.Windows.Forms.CheckBox();
            this.chkString = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkHex
            // 
            this.chkHex.AutoSize = true;
            this.chkHex.Location = new System.Drawing.Point(12, 12);
            this.chkHex.Name = "chkHex";
            this.chkHex.Size = new System.Drawing.Size(85, 17);
            this.chkHex.TabIndex = 0;
            this.chkHex.Text = "StringToHex";
            this.chkHex.UseVisualStyleBackColor = true;
            // 
            // chkEncrip
            // 
            this.chkEncrip.AutoSize = true;
            this.chkEncrip.Location = new System.Drawing.Point(116, 12);
            this.chkEncrip.Name = "chkEncrip";
            this.chkEncrip.Size = new System.Drawing.Size(62, 17);
            this.chkEncrip.TabIndex = 1;
            this.chkEncrip.Text = "Encrypt";
            this.chkEncrip.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1094, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Encrypted Text";
            // 
            // crypText1
            // 
            this.crypText1.Location = new System.Drawing.Point(12, 96);
            this.crypText1.Name = "crypText1";
            this.crypText1.Size = new System.Drawing.Size(1094, 20);
            this.crypText1.TabIndex = 4;
            // 
            // btnEncryp
            // 
            this.btnEncryp.Location = new System.Drawing.Point(1031, 122);
            this.btnEncryp.Name = "btnEncryp";
            this.btnEncryp.Size = new System.Drawing.Size(75, 23);
            this.btnEncryp.TabIndex = 6;
            this.btnEncryp.Text = "Encrypt";
            this.btnEncryp.UseVisualStyleBackColor = true;
            this.btnEncryp.Click += new System.EventHandler(this.btnEncryp_Click);
            // 
            // btnDecryp
            // 
            this.btnDecryp.Location = new System.Drawing.Point(1031, 297);
            this.btnDecryp.Name = "btnDecryp";
            this.btnDecryp.Size = new System.Drawing.Size(75, 23);
            this.btnDecryp.TabIndex = 12;
            this.btnDecryp.Text = "Decrypt";
            this.btnDecryp.UseVisualStyleBackColor = true;
            this.btnDecryp.Click += new System.EventHandler(this.btnDecryp_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Encrypted Text";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 271);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(1094, 20);
            this.textBox2.TabIndex = 10;
            // 
            // crypText2
            // 
            this.crypText2.Location = new System.Drawing.Point(12, 229);
            this.crypText2.Name = "crypText2";
            this.crypText2.Size = new System.Drawing.Size(1094, 20);
            this.crypText2.TabIndex = 9;
            // 
            // chkDecryp
            // 
            this.chkDecryp.AutoSize = true;
            this.chkDecryp.Location = new System.Drawing.Point(116, 187);
            this.chkDecryp.Name = "chkDecryp";
            this.chkDecryp.Size = new System.Drawing.Size(63, 17);
            this.chkDecryp.TabIndex = 8;
            this.chkDecryp.Text = "Decrypt";
            this.chkDecryp.UseVisualStyleBackColor = true;
            // 
            // chkString
            // 
            this.chkString.AutoSize = true;
            this.chkString.Location = new System.Drawing.Point(12, 187);
            this.chkString.Name = "chkString";
            this.chkString.Size = new System.Drawing.Size(85, 17);
            this.chkString.TabIndex = 7;
            this.chkString.Text = "HexToString";
            this.chkString.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 357);
            this.Controls.Add(this.btnDecryp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.crypText2);
            this.Controls.Add(this.chkDecryp);
            this.Controls.Add(this.chkString);
            this.Controls.Add(this.btnEncryp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.crypText1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chkEncrip);
            this.Controls.Add(this.chkHex);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkHex;
        private System.Windows.Forms.CheckBox chkEncrip;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox crypText1;
        private System.Windows.Forms.Button btnEncryp;
        private System.Windows.Forms.Button btnDecryp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox crypText2;
        private System.Windows.Forms.CheckBox chkDecryp;
        private System.Windows.Forms.CheckBox chkString;
    }
}

