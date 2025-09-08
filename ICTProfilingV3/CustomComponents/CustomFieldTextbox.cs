using DevExpress.XtraEditors;
using System.ComponentModel;

namespace ICTProfilingV3.CustomComponents
{
    public class CustomFieldTextbox : TextEdit
    {
        private string _fieldName;

        [Category("Custom")]
        [Description("Text that will appear when the TextBox is empty.")]
        public string FieldName
        {
            get { return _fieldName; }
            set
            {
                _fieldName = value;
                this.Invalidate();
            }
        }
    }
}
