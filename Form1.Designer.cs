namespace GraphicalProgammingLanguage
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
            this.CommandPanel = new System.Windows.Forms.TextBox();
            this.Run = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.CommandLine = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            this.Syntax = new System.Windows.Forms.Button();
            this.display = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // CommandPanel
            // 
            this.CommandPanel.Location = new System.Drawing.Point(30, 12);
            this.CommandPanel.Multiline = true;
            this.CommandPanel.Name = "CommandPanel";
            this.CommandPanel.Size = new System.Drawing.Size(368, 287);
            this.CommandPanel.TabIndex = 0;
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(30, 349);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 1;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.button1_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(111, 349);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 2;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            // 
            // CommandLine
            // 
            this.CommandLine.Location = new System.Drawing.Point(30, 313);
            this.CommandLine.Name = "CommandLine";
            this.CommandLine.Size = new System.Drawing.Size(368, 20);
            this.CommandLine.TabIndex = 3;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(607, 349);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(688, 349);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(75, 23);
            this.Load.TabIndex = 5;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Syntax
            // 
            this.Syntax.Location = new System.Drawing.Point(323, 349);
            this.Syntax.Name = "Syntax";
            this.Syntax.Size = new System.Drawing.Size(75, 23);
            this.Syntax.TabIndex = 6;
            this.Syntax.Text = "Syntax";
            this.Syntax.UseVisualStyleBackColor = true;
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(444, 12);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(332, 287);
            this.display.TabIndex = 7;
            this.display.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.display);
            this.Controls.Add(this.Syntax);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.CommandLine);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.CommandPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CommandPanel;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.TextBox CommandLine;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Load;
        private System.Windows.Forms.Button Syntax;
        private System.Windows.Forms.PictureBox display;
    }
}

