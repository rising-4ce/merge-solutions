namespace MergeSolutions.UI
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newMergeRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMergePlanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openMergePlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMergePlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelOutputSolutionPath = new System.Windows.Forms.Label();
            this.textBoxOutputSolutionPath = new System.Windows.Forms.TextBox();
            this.buttonBrowseOutputSolutionPath = new System.Windows.Forms.Button();
            this.treeViewSolutions = new System.Windows.Forms.TreeView();
            this.labelSolutions = new System.Windows.Forms.Label();
            this.buttonAppendSolution = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMergeRuleToolStripMenuItem,
            this.runMergeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(920, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newMergeRuleToolStripMenuItem
            // 
            this.newMergeRuleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMergePlanToolStripMenuItem1,
            this.openMergePlanToolStripMenuItem,
            this.saveMergePlanToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.newMergeRuleToolStripMenuItem.Name = "newMergeRuleToolStripMenuItem";
            this.newMergeRuleToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.newMergeRuleToolStripMenuItem.Text = "Merge plan";
            // 
            // newMergePlanToolStripMenuItem1
            // 
            this.newMergePlanToolStripMenuItem1.Name = "newMergePlanToolStripMenuItem1";
            this.newMergePlanToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.newMergePlanToolStripMenuItem1.Text = "New";
            this.newMergePlanToolStripMenuItem1.Click += new System.EventHandler(this.newMergePlanToolStripMenuItem1_Click);
            // 
            // openMergePlanToolStripMenuItem
            // 
            this.openMergePlanToolStripMenuItem.Name = "openMergePlanToolStripMenuItem";
            this.openMergePlanToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openMergePlanToolStripMenuItem.Text = "Open";
            this.openMergePlanToolStripMenuItem.Click += new System.EventHandler(this.openMergePlanToolStripMenuItem_Click);
            // 
            // saveMergePlanToolStripMenuItem
            // 
            this.saveMergePlanToolStripMenuItem.Name = "saveMergePlanToolStripMenuItem";
            this.saveMergePlanToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveMergePlanToolStripMenuItem.Text = "Save As";
            this.saveMergePlanToolStripMenuItem.Click += new System.EventHandler(this.saveMergePlanToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // runMergeToolStripMenuItem
            // 
            this.runMergeToolStripMenuItem.Name = "runMergeToolStripMenuItem";
            this.runMergeToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.runMergeToolStripMenuItem.Text = "Run merge";
            this.runMergeToolStripMenuItem.Click += new System.EventHandler(this.runMergeToolStripMenuItem_Click);
            // 
            // labelOutputSolutionPath
            // 
            this.labelOutputSolutionPath.AutoSize = true;
            this.labelOutputSolutionPath.Location = new System.Drawing.Point(12, 34);
            this.labelOutputSolutionPath.Name = "labelOutputSolutionPath";
            this.labelOutputSolutionPath.Size = new System.Drawing.Size(118, 15);
            this.labelOutputSolutionPath.TabIndex = 1;
            this.labelOutputSolutionPath.Text = "Output solution path";
            // 
            // textBoxOutputSolutionPath
            // 
            this.textBoxOutputSolutionPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputSolutionPath.Location = new System.Drawing.Point(136, 31);
            this.textBoxOutputSolutionPath.Name = "textBoxOutputSolutionPath";
            this.textBoxOutputSolutionPath.Size = new System.Drawing.Size(691, 23);
            this.textBoxOutputSolutionPath.TabIndex = 2;
            // 
            // buttonBrowseOutputSolutionPath
            // 
            this.buttonBrowseOutputSolutionPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseOutputSolutionPath.Location = new System.Drawing.Point(833, 31);
            this.buttonBrowseOutputSolutionPath.Name = "buttonBrowseOutputSolutionPath";
            this.buttonBrowseOutputSolutionPath.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseOutputSolutionPath.TabIndex = 3;
            this.buttonBrowseOutputSolutionPath.Text = "Browse...";
            this.buttonBrowseOutputSolutionPath.UseVisualStyleBackColor = true;
            this.buttonBrowseOutputSolutionPath.Click += new System.EventHandler(this.buttonBrowseOutputSolutionPath_Click);
            // 
            // treeViewSolutions
            // 
            this.treeViewSolutions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSolutions.Location = new System.Drawing.Point(136, 60);
            this.treeViewSolutions.Name = "treeViewSolutions";
            this.treeViewSolutions.Size = new System.Drawing.Size(772, 361);
            this.treeViewSolutions.TabIndex = 4;
            // 
            // labelSolutions
            // 
            this.labelSolutions.AutoSize = true;
            this.labelSolutions.Location = new System.Drawing.Point(12, 60);
            this.labelSolutions.Name = "labelSolutions";
            this.labelSolutions.Size = new System.Drawing.Size(56, 15);
            this.labelSolutions.TabIndex = 5;
            this.labelSolutions.Text = "Solutions";
            // 
            // buttonAppendSolution
            // 
            this.buttonAppendSolution.Location = new System.Drawing.Point(12, 88);
            this.buttonAppendSolution.Name = "buttonAppendSolution";
            this.buttonAppendSolution.Size = new System.Drawing.Size(118, 23);
            this.buttonAppendSolution.TabIndex = 6;
            this.buttonAppendSolution.Text = "Append solution";
            this.buttonAppendSolution.UseVisualStyleBackColor = true;
            this.buttonAppendSolution.Click += new System.EventHandler(this.buttonAppendSolution_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 433);
            this.Controls.Add(this.buttonAppendSolution);
            this.Controls.Add(this.labelSolutions);
            this.Controls.Add(this.treeViewSolutions);
            this.Controls.Add(this.buttonBrowseOutputSolutionPath);
            this.Controls.Add(this.textBoxOutputSolutionPath);
            this.Controls.Add(this.labelOutputSolutionPath);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Merge Solutions UI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem newMergeRuleToolStripMenuItem;
        private ToolStripMenuItem newMergePlanToolStripMenuItem1;
        private ToolStripMenuItem openMergePlanToolStripMenuItem;
        private ToolStripMenuItem saveMergePlanToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem runMergeToolStripMenuItem;
        private Label labelOutputSolutionPath;
        private TextBox textBoxOutputSolutionPath;
        private Button buttonBrowseOutputSolutionPath;
        private TreeView treeViewSolutions;
        private Label labelSolutions;
        private Button buttonAppendSolution;
    }
}