namespace WinformAppCleanupCSharpClass;

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
            this.btnProcess = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtInputFolder = new System.Windows.Forms.TextBox();
            this.btnProcessFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(14, 855);
            this.btnProcess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(86, 31);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "button1";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(14, 36);
            this.txtInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(630, 312);
            this.txtInput.TabIndex = 1;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(14, 456);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(630, 312);
            this.txtOutput.TabIndex = 2;
            // 
            // txtInputFolder
            // 
            this.txtInputFolder.Location = new System.Drawing.Point(825, 402);
            this.txtInputFolder.Name = "txtInputFolder";
            this.txtInputFolder.Size = new System.Drawing.Size(125, 27);
            this.txtInputFolder.TabIndex = 3;
            // 
            // btnProcessFolder
            // 
            this.btnProcessFolder.Location = new System.Drawing.Point(825, 454);
            this.btnProcessFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnProcessFolder.Name = "btnProcessFolder";
            this.btnProcessFolder.Size = new System.Drawing.Size(171, 31);
            this.btnProcessFolder.TabIndex = 4;
            this.btnProcessFolder.Text = "processFolder";
            this.btnProcessFolder.UseVisualStyleBackColor = true;
            this.btnProcessFolder.Click += new System.EventHandler(this.btnProcessFolder_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 901);
            this.Controls.Add(this.btnProcessFolder);
            this.Controls.Add(this.txtInputFolder);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnProcess);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button btnProcess;
    private TextBox txtInput;
    private TextBox txtOutput;
    private TextBox txtInputFolder;
    private Button btnProcessFolder;
}
