using Models.Entities;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class ActionTreeViewModel
    {
        public ActionsDropdowns ActionTree { get; set; }
        public int ImageIndex { get; set; }
        public string NodeValue { get; set; }
    }
}
