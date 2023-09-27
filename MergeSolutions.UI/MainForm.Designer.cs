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
            treeViewSolutions = new TreeView();
            buttonAppendSolution = new Button();
            buttonReset = new Button();
            buttonOpenPlan = new Button();
            buttonRunMerge = new Button();
            buttonSaveMergePlan = new Button();
            labelMergePlanPath = new Label();
            labelMergedSolutionPath = new Label();
            SuspendLayout();
            // 
            // treeViewSolutions
            // 
            treeViewSolutions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeViewSolutions.Location = new Point(161, 41);
            treeViewSolutions.Name = "treeViewSolutions";
            treeViewSolutions.Size = new Size(611, 445);
            treeViewSolutions.TabIndex = 4;
            treeViewSolutions.AfterLabelEdit += treeViewSolutions_AfterLabelEdit;
            treeViewSolutions.AfterCheck += treeViewSolutions_AfterCheck;
            // 
            // buttonAppendSolution
            // 
            buttonAppendSolution.Location = new Point(11, 41);
            buttonAppendSolution.Name = "buttonAppendSolution";
            buttonAppendSolution.Size = new Size(143, 23);
            buttonAppendSolution.TabIndex = 6;
            buttonAppendSolution.Text = "Append solution";
            buttonAppendSolution.UseVisualStyleBackColor = true;
            buttonAppendSolution.Click += buttonAppendSolution_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(12, 12);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(143, 23);
            buttonReset.TabIndex = 7;
            buttonReset.Text = "Reset merge plan";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // buttonOpenPlan
            // 
            buttonOpenPlan.Location = new Point(161, 12);
            buttonOpenPlan.Name = "buttonOpenPlan";
            buttonOpenPlan.Size = new Size(143, 23);
            buttonOpenPlan.TabIndex = 8;
            buttonOpenPlan.Text = "Open merge plan";
            buttonOpenPlan.UseVisualStyleBackColor = true;
            buttonOpenPlan.Click += buttonOpenPlan_Click;
            // 
            // buttonRunMerge
            // 
            buttonRunMerge.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonRunMerge.Location = new Point(12, 526);
            buttonRunMerge.Name = "buttonRunMerge";
            buttonRunMerge.Size = new Size(143, 23);
            buttonRunMerge.TabIndex = 9;
            buttonRunMerge.Text = "Create merged solution";
            buttonRunMerge.UseVisualStyleBackColor = true;
            buttonRunMerge.Click += buttonRunMerge_Click;
            // 
            // buttonSaveMergePlan
            // 
            buttonSaveMergePlan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonSaveMergePlan.Location = new Point(12, 496);
            buttonSaveMergePlan.Name = "buttonSaveMergePlan";
            buttonSaveMergePlan.Size = new Size(142, 23);
            buttonSaveMergePlan.TabIndex = 10;
            buttonSaveMergePlan.Text = "Save merge plan";
            buttonSaveMergePlan.UseVisualStyleBackColor = true;
            buttonSaveMergePlan.Click += buttonSaveMergePlan_Click;
            // 
            // labelMergePlanPath
            // 
            labelMergePlanPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelMergePlanPath.AutoSize = true;
            labelMergePlanPath.Location = new Point(161, 500);
            labelMergePlanPath.Name = "labelMergePlanPath";
            labelMergePlanPath.Size = new Size(113, 15);
            labelMergePlanPath.TabIndex = 11;
            labelMergePlanPath.Text = "labelMergePlanPath";
            // 
            // labelMergedSolutionPath
            // 
            labelMergedSolutionPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelMergedSolutionPath.AutoSize = true;
            labelMergedSolutionPath.Location = new Point(161, 530);
            labelMergedSolutionPath.Name = "labelMergedSolutionPath";
            labelMergedSolutionPath.Size = new Size(141, 15);
            labelMergedSolutionPath.TabIndex = 12;
            labelMergedSolutionPath.Text = "labelMergedSolutionPath";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(labelMergedSolutionPath);
            Controls.Add(labelMergePlanPath);
            Controls.Add(buttonSaveMergePlan);
            Controls.Add(buttonRunMerge);
            Controls.Add(buttonOpenPlan);
            Controls.Add(buttonReset);
            Controls.Add(buttonAppendSolution);
            Controls.Add(treeViewSolutions);
            MinimumSize = new Size(800, 377);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Merge Solutions UI";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
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