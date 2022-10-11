using MergeSolutions.Core.Models;

namespace MergeSolutions.UI
{
    internal class SolutionTreeNode : TreeNode
    {
        public SolutionTreeNode(SolutionEntity solutionEntity)
        {
            SolutionEntity = solutionEntity;
            NodeName = solutionEntity.NodeName ?? "";
        }

        public string NodeName
        {
            get => SolutionEntity.NodeName ?? "";
            set
            {
                SolutionEntity.NodeName = value;
                Text = SolutionNodeText(value, SolutionEntity.RelativePath);
            }
        }

        public SolutionEntity SolutionEntity { get; }

        private static string SolutionNodeText(string nodeName, string? relativePath)
        {
            return $"{nodeName} ({relativePath})";
        }
    }
}