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
            this.CommandLine = new System.Windows.Forms.TextBox();
            this.Load = new System.Windows.Forms.Button();
            this.display = new System.Windows.Forms.PictureBox();
            this.Clear = new System.Windows.Forms.Button();
            this.Syntax = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // CommandPanel
            // 
            this.CommandPanel.Location = new System.Drawing.Point(40, 15);
            this.CommandPanel.Margin = new System.Windows.Forms.Padding(4);
            this.CommandPanel.Multiline = true;
            this.CommandPanel.Name = "CommandPanel";
            this.CommandPanel.Size = new System.Drawing.Size(489, 352);
            this.CommandPanel.TabIndex = 0;
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(40, 430);
            this.Run.Margin = new System.Windows.Forms.Padding(4);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(100, 28);
            this.Run.TabIndex = 1;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.runButton);
            // 
            // CommandLine
            // 
            this.CommandLine.Location = new System.Drawing.Point(40, 385);
            this.CommandLine.Margin = new System.Windows.Forms.Padding(4);
            this.CommandLine.Name = "CommandLine";
            this.CommandLine.Size = new System.Drawing.Size(489, 22);
            this.CommandLine.TabIndex = 3;
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(917, 430);
            this.Load.Margin = new System.Windows.Forms.Padding(4);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(100, 28);
            this.Load.TabIndex = 5;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.loadButton);
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.display.Location = new System.Drawing.Point(592, 15);
            this.display.Margin = new System.Windows.Forms.Padding(4);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(443, 353);
            this.display.TabIndex = 7;
            this.display.TabStop = false;
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(147, 430);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(104, 28);
            this.Clear.TabIndex = 8;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.clearButton);
            // 
            // Syntax
            // 
            this.Syntax.Location = new System.Drawing.Point(415, 430);
            this.Syntax.Name = "Syntax";
            this.Syntax.Size = new System.Drawing.Size(103, 28);
            this.Syntax.TabIndex = 9;
            this.Syntax.Text = "Syntax";
            this.Syntax.UseVisualStyleBackColor = true;
            this.Syntax.Click += new System.EventHandler(this.syntaxButton);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(802, 430);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(97, 28);
            this.Save.TabIndex = 10;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.saveButton);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Syntax);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.display);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.CommandLine);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.CommandPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CommandPanel;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.TextBox CommandLine;
        private new System.Windows.Forms.Button Load;
        private System.Windows.Forms.PictureBox display;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Syntax;
        private System.Windows.Forms.Button Save;
    }
}

