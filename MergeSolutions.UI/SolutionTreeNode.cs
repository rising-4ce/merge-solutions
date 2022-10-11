namespace MergeSolutions.UI
{
    internal class SolutionTreeNode : TreeNode
    {
        private string _nodeName = null!;

        public SolutionTreeNode(string nodeName, string relativePath)
        {
            RelativePath = relativePath;
            //Init RelativePath before NodeName
            NodeName = nodeName;
        }

        public string NodeName
        {
            get => _nodeName;
            set
            {
                _nodeName = value;
                Text = SolutionNodeText(_nodeName, RelativePath);
            }
        }

        public string RelativePath { get; }

        private static string SolutionNodeText(string nodeName, string relativePath)
        {
            return $"{nodeName} ({relativePath})";
        }
    }
}