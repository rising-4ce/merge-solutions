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
            this.textBoxOutputSolutionPath = new System.Windows.Forms.TextBox();
            this.buttonBrowseOutputSolutionPath = new System.Windows.Forms.Button();
            this.treeViewSolutions = new System.Windows.Forms.TreeView();
            this.buttonAppendSolution = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonOpenPlan = new System.Windows.Forms.Button();
            this.buttonRunMerge = new System.Windows.Forms.Button();
            this.buttonSaveMergePlan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxOutputSolutionPath
            // 
            this.textBoxOutputSolutionPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputSolutionPath.Location = new System.Drawing.Point(161, 42);
            this.textBoxOutputSolutionPath.Name = "textBoxOutputSolutionPath";
            this.textBoxOutputSolutionPath.Size = new System.Drawing.Size(711, 23);
            this.textBoxOutputSolutionPath.TabIndex = 2;
            // 
            // buttonBrowseOutputSolutionPath
            // 
            this.buttonBrowseOutputSolutionPath.Location = new System.Drawing.Point(12, 41);
            this.buttonBrowseOutputSolutionPath.Name = "buttonBrowseOutputSolutionPath";
            this.buttonBrowseOutputSolutionPath.Size = new System.Drawing.Size(143, 23);
            this.buttonBrowseOutputSolutionPath.TabIndex = 3;
            this.buttonBrowseOutputSolutionPath.Text = "Merged solution path...";
            this.buttonBrowseOutputSolutionPath.UseVisualStyleBackColor = true;
            this.buttonBrowseOutputSolutionPath.Click += new System.EventHandler(this.buttonBrowseOutputSolutionPath_Click);
            // 
            // treeViewSolutions
            // 
            this.treeViewSolutions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSolutions.Location = new System.Drawing.Point(161, 71);
            this.treeViewSolutions.Name = "treeViewSolutions";
            this.treeViewSolutions.Size = new System.Drawing.Size(711, 399);
            this.treeViewSolutions.TabIndex = 4;
            this.treeViewSolutions.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewSolutions_AfterLabelEdit);
            this.treeViewSolutions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSolutions_AfterCheck);
            this.treeViewSolutions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeViewSolutions_KeyUp);
            // 
            // buttonAppendSolution
            // 
            this.buttonAppendSolution.Location = new System.Drawing.Point(12, 71);
            this.buttonAppendSolution.Name = "buttonAppendSolution";
            this.buttonAppendSolution.Size = new System.Drawing.Size(143, 23);
            this.buttonAppendSolution.TabIndex = 6;
            this.buttonAppendSolution.Text = "Append solution";
            this.buttonAppendSolution.UseVisualStyleBackColor = true;
            this.buttonAppendSolution.Click += new System.EventHandler(this.buttonAppendSolution_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(12, 12);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(143, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Reset merge plan";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonOpenPlan
            // 
            this.buttonOpenPlan.Location = new System.Drawing.Point(161, 12);
            this.buttonOpenPlan.Name = "buttonOpenPlan";
            this.buttonOpenPlan.Size = new System.Drawing.Size(143, 23);
            this.buttonOpenPlan.TabIndex = 8;
            this.buttonOpenPlan.Text = "Open merge plan";
            this.buttonOpenPlan.UseVisualStyleBackColor = true;
            this.buttonOpenPlan.Click += new System.EventHandler(this.buttonOpenPlan_Click);
            // 
            // buttonRunMerge
            // 
            this.buttonRunMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRunMerge.Location = new System.Drawing.Point(161, 476);
            this.buttonRunMerge.Name = "buttonRunMerge";
            this.buttonRunMerge.Size = new System.Drawing.Size(143, 23);
            this.buttonRunMerge.TabIndex = 9;
            this.buttonRunMerge.Text = "Create merged solution";
            this.buttonRunMerge.UseVisualStyleBackColor = true;
            this.buttonRunMerge.Click += new System.EventHandler(this.buttonRunMerge_Click);
            // 
            // buttonSaveMergePlan
            // 
            this.buttonSaveMergePlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveMergePlan.Location = new System.Drawing.Point(12, 476);
            this.buttonSaveMergePlan.Name = "buttonSaveMergePlan";
            this.buttonSaveMergePlan.Size = new System.Drawing.Size(142, 23);
            this.buttonSaveMergePlan.TabIndex = 10;
            this.buttonSaveMergePlan.Text = "Save merge plan";
            this.buttonSaveMergePlan.UseVisualStyleBackColor = true;
            this.buttonSaveMergePlan.Click += new System.EventHandler(this.buttonSaveMergePlan_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.buttonSaveMergePlan);
            this.Controls.Add(this.buttonRunMerge);
            this.Controls.Add(this.buttonOpenPlan);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonAppendSolution);
            this.Controls.Add(this.treeViewSolutions);
            this.Controls.Add(this.buttonBrowseOutputSolutionPath);
            this.Controls.Add(this.textBoxOutputSolutionPath);
            this.MinimumSize = new System.Drawing.Size(800, 377);
            this.Name = "MainForm";
            this.Text = "Merge Solutions UI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox textBoxOutputSolutionPath;
        private Button buttonBrowseOutputSolutionPath;
        private TreeView treeViewSolutions;
        private Button buttonAppendSolution;
        private Button buttonReset;
        private Button buttonOpenPlan;
        private Button buttonRunMerge;
        private Button buttonSaveMergePlan;
    }
}