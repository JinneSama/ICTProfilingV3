using Helpers.Tools.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Helpers.Tools
{
    public class CopyPaste
    {
        public static List<CopiedSpecs> PasteSpecs()
        {
            string clipboardText = string.Empty;
            if (Clipboard.ContainsText(TextDataFormat.Text))
                clipboardText = Clipboard.GetText(TextDataFormat.Text);

            var specs = new List<CopiedSpecs>();
            var splitByNewLine = clipboardText.Split('\n');
            var previousLine = splitByNewLine[0];

            foreach (var s in splitByNewLine)
                if (s.Contains("\t"))
                {
                    var splitByTab = s.Split('\t');
                    specs.Add(new CopiedSpecs
                    {
                        Specs = splitByTab[0],
                        Description = splitByTab[1]
                    });
                    previousLine = s;
                }
                else
                {
                    previousLine += s;
                }
            return specs;
        }
    }
}
