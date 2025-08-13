using Models.Entities;

namespace ICTProfilingV3.DataTransferModels
{
    public class ActionTreeDTM
    {
        public ActionsDropdowns ActionTree { get; set; }
        public int ImageIndex { get; set; }
        public string NodeValue { get; set; }
    }
}
