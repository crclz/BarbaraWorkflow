using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbaraWorkflow.Domain.Models
{
    public class HintStatus
    {
        // 换行需要按照
        private List<string> content { get; set; }

        private int cursor { get; set; } = 0;

        public HintStatus(IEnumerable<string> content)
        {
            this.content = content.ToList();
        }

        public string GetCurrentHint()
        {
            var hint = content[cursor];
            if (hint.Trim() == "")
            {
                return "<空行>";
            }

            return hint;
        }

        public bool TryBackward()
        {
            if (cursor > 0)
            {
                cursor--;
            }
            return false;
        }

        public bool TryForward()
        {
            if (cursor < content.Count - 1)
            {
                cursor++;
                return true;
            }
            return false;
        }


        public static HintStatus CreateFromText(string s)
        {
            var parts = new List<string>();

            s = s.Replace("\r", "");

            foreach (var line in s.Split("\n"))
            {
                parts.Add(line);
            }

            return new HintStatus(parts);
        }
    }
}
