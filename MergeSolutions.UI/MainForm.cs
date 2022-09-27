using System.Text.Json;
using MergeSolutions.Core;
using MergeSolutions.Core.Models;
using MergeSolutions.Core.Services;

// ReSharper disable LocalizableElement

namespace MergeSolutions.UI
{
    public partial class MainForm : Form
    {
        public const string MergeSolutionsPlanFilter =
            "MergeSolutions Plan (.msp1)|*.msp1|All files (*.*)|*.*";

        public const string SolutionFilter =
            "Visual Studio Solution (.sln)|*.sln|All files (*.*)|*.*";

        public const string AppName = "Merge Solutions UI";

        private readonly IMergeSolutionsService _mergeSolutionsService;

        private readonly ISolutionService _solutionService;

        private MergePlan _mergePlan = new MergePlan();

        public MainForm(IMergeSolutionsService mergeSolutionsService, ISolutionService solutionService, IStartup startup)
        {
            _mergeSolutionsService = mergeSolutionsService;
            _solutionService = solutionService;
            InitializeComponent();
            if (startup.PlanPath != null)
            {
                LoadMergePlan(startup.PlanPath);
            }
        }

        private void buttonAppendSolution_Click(object sender, EventArgs e)
        {
            InTryCatch(() =>
            {
                UiToPlan();
                using var openFileDialog = new OpenFileDialog
                {
                    Filter = SolutionFilter,
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var stream = openFileDialog.OpenFile();
                    using (stream)
                    {
                        var solutionName = Path.GetFileNameWithoutExtension(openFileDialog.SafeFileName);
                        _mergePlan.Solutions[solutionName] = openFileDialog.FileName;
                        PlanToUi();
                    }
                }
            });
        }

        private void buttonBrowseOutputSolutionPath_Click(object sender, EventArgs e)
        {
            InTryCatch(() =>
            {
                UiToPlan();
                using var saveFileDialog = new SaveFileDialog
                {
                    Filter = SolutionFilter,
                    FilterIndex = 1,
                    FileName = _mergePlan.PlanName,
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _mergePlan.OutputSolutionPath = saveFileDialog.FileName;
                    PlanToUi();
                }
            });
        }

        private void buttonOpenPlan_Click(object sender, EventArgs e)
        {
            InTryCatch(() =>
            {
                using var openFileDialog = new OpenFileDialog
                {
                    Filter = MergeSolutionsPlanFilter,
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = openFileDialog.FileName;
                    LoadMergePlan(fileName);
                }
            });
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            InTryCatch(() =>
            {
                _mergePlan = new MergePlan();
                PlanToUi();
            });
        }

        private void buttonRunMerge_Click(object sender, EventArgs e)
        {
            InTryCatch(() =>
            {
                UiToPlan();
                _mergeSolutionsService.MergeSolutions(_mergePlan);
                PlanToUi();
                MessageBox.Show("Merged solution is created.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void buttonSaveMergePlan_Click(object sender, EventArgs e)
        {
            InTryCatch(() =>
            {
                UiToPlan();
                using var saveFileDialog = new SaveFileDialog
                {
                    Filter = MergeSolutionsPlanFilter,
                    FilterIndex = 1,
                    FileName = _mergePlan.PlanName,
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var stream = saveFileDialog.OpenFile();
                    using (stream)
                    {
                        JsonSerializer.Serialize(stream, _mergePlan, new JsonSerializerOptions
                        {
                            WriteIndented = true
                        });
                        _mergePlan.PlanName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                    }

                    PlanToUi();
                }
            });
        }

        private void InTryCatch(Action action, string? message = null, string? caption = null)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Program.ShowExceptionMessage(message ?? "Operation failed", e, caption ?? "Operation failed");
            }
        }

        private void LoadMergePlan(string fileName)
        {
            var content = File.ReadAllText(fileName);
            var mergePlan = JsonSerializer.Deserialize<MergePlan>(content) ?? new MergePlan();
            mergePlan.PlanName = Path.GetFileNameWithoutExtension(fileName);
            _mergePlan = mergePlan;
            PlanToUi();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PlanToUi();
        }

        private void PlanToUi()
        {
            Text = $"{AppName} : {_mergePlan.PlanName}";
            textBoxOutputSolutionPath.Text = _mergePlan.OutputSolutionPath;
            treeViewSolutions.Nodes.Clear();
            treeViewSolutions.CheckBoxes = true;
            foreach (var solution in _mergePlan.Solutions.OrderBy(s => s.Key))
            {
                var solutionInfo = _solutionService.ParseSolution(solution.Value);
                var solutionNode = new TreeNode(solution.Key)
                {
                    Checked = true
                };

                foreach (var project in solutionInfo.Projects.OfType<Project>().OrderBy(p => p.Name))
                {
                    var projectNode = new TreeNode(project.Name)
                    {
                        Checked = !_mergePlan.IsExcluded(project.SolutionName, project.Guid),
                        Tag = project.Guid
                    };

                    solutionNode.Nodes.Add(projectNode);
                }

                treeViewSolutions.Nodes.Add(solutionNode);
                solutionNode.Expand();
            }
        }

        private void treeViewSolutions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode nodeNode in e.Node!.Nodes)
            {
                nodeNode.Checked = e.Node.Checked;
            }
        }

        private void treeViewSolutions_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (sender is TreeView treeView)
                {
                    var selectedNode = treeView.SelectedNode;
                    // Allow root level nodes removing only
                    if (selectedNode.Parent != null)
                    {
                        return;
                    }

                    treeView.Nodes.Remove(selectedNode);
                }
            }
        }

        private void UiToPlan()
        {
            _mergePlan.OutputSolutionPath = textBoxOutputSolutionPath.Text;
            var excludedProjects = new List<KeyValuePair<string, string>>();
            foreach (TreeNode solutionNode in treeViewSolutions.Nodes)
            {
                foreach (TreeNode projectNode in solutionNode.Nodes)
                {
                    if (!projectNode.Checked)
                    {
                        excludedProjects.Add(new KeyValuePair<string, string>(solutionNode.Text, (string)projectNode.Tag));
                    }
                }
            }

            _mergePlan.ExcludedProjects = excludedProjects.ToArray();
        }
    }
}