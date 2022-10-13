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
            this.treeViewSolutions = new System.Windows.Forms.TreeView();
            this.buttonAppendSolution = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonOpenPlan = new System.Windows.Forms.Button();
            this.buttonRunMerge = new System.Windows.Forms.Button();
            this.buttonSaveMergePlan = new System.Windows.Forms.Button();
            this.labelMergePlanPath = new System.Windows.Forms.Label();
            this.labelMergedSolutionPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeViewSolutions
            // 
            this.treeViewSolutions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSolutions.Location = new System.Drawing.Point(161, 41);
            this.treeViewSolutions.Name = "treeViewSolutions";
            this.treeViewSolutions.Size = new System.Drawing.Size(611, 445);
            this.treeViewSolutions.TabIndex = 4;
            this.treeViewSolutions.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewSolutions_AfterLabelEdit);
            this.treeViewSolutions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSolutions_AfterCheck);
            // 
            // buttonAppendSolution
            // 
            this.buttonAppendSolution.Location = new System.Drawing.Point(11, 41);
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
            this.buttonRunMerge.Location = new System.Drawing.Point(12, 526);
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
            this.buttonSaveMergePlan.Location = new System.Drawing.Point(12, 496);
            this.buttonSaveMergePlan.Name = "buttonSaveMergePlan";
            this.buttonSaveMergePlan.Size = new System.Drawing.Size(142, 23);
            this.buttonSaveMergePlan.TabIndex = 10;
            this.buttonSaveMergePlan.Text = "Save merge plan";
            this.buttonSaveMergePlan.UseVisualStyleBackColor = true;
            this.buttonSaveMergePlan.Click += new System.EventHandler(this.buttonSaveMergePlan_Click);
            // 
            // labelMergePlanPath
            // 
            this.labelMergePlanPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMergePlanPath.AutoSize = true;
            this.labelMergePlanPath.Location = new System.Drawing.Point(161, 500);
            this.labelMergePlanPath.Name = "labelMergePlanPath";
            this.labelMergePlanPath.Size = new System.Drawing.Size(113, 15);
            this.labelMergePlanPath.TabIndex = 11;
            this.labelMergePlanPath.Text = "labelMergePlanPath";
            // 
            // labelMergedSolutionPath
            // 
            this.labelMergedSolutionPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMergedSolutionPath.AutoSize = true;
            this.labelMergedSolutionPath.Location = new System.Drawing.Point(161, 530);
            this.labelMergedSolutionPath.Name = "labelMergedSolutionPath";
            this.labelMergedSolutionPath.Size = new System.Drawing.Size(141, 15);
            this.labelMergedSolutionPath.TabIndex = 12;
            this.labelMergedSolutionPath.Text = "labelMergedSolutionPath";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.labelMergedSolutionPath);
            this.Controls.Add(this.labelMergePlanPath);
            this.Controls.Add(this.buttonSaveMergePlan);
            this.Controls.Add(this.buttonRunMerge);
            this.Controls.Add(this.buttonOpenPlan);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonAppendSolution);
            this.Controls.Add(this.treeViewSolutions);
            this.MinimumSize = new System.Drawing.Size(800, 377);
            this.Name = "MainForm";
            this.Text = "Merge Solutions UI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TreeView treeViewSolutions;
        private Button buttonAppendSolution;
        private Button buttonReset;
        private Button buttonOpenPlan;
        private Button buttonRunMerge;
        private Button buttonSaveMergePlan;
        private Label labelMergePlanPath;
        private Label labelMergedSolutionPath;
    }
}