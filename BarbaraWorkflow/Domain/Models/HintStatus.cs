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

        public string GetCurrentHint(int leftLines = 0, int rightLines = 0)
        {
            leftLines++;
            rightLines++;

            var midPart = $"【{content[cursor].Trim()}】";

            var leftParts = new List<string>();

            for (var i = cursor - 1; i >= 0; i--)
            {
                var part = content[i];
                leftParts.Add(part);
                if (part == "\n")
                {
                    leftLines--;
                }

                if (leftLines == 0)
                {
                    break;
                }
            }

            var rightParts = new List<string>();

            for (var i = cursor + 1; i < content.Count; i++)
            {
                var part = content[i];
                rightParts.Add(part);
                if (part == "\n")
                {
                    rightLines--;
                }

                if (rightLines == 0)
                {
                    break;
                }
            }

            var allParts = leftParts.AsEnumerable().Reverse().Append(midPart).Concat(rightParts).ToList();
            return string.Join("", allParts).Trim();
        }

        public bool TryBackward()
        {
            if (cursor > 0)
            {
                cursor--;
                if (content[cursor] == "\n" && cursor > 0)
                {
                    cursor--;
                }
                return true;
            }
            return false;
        }

        public bool TryForward()
        {
            if (cursor < content.Count - 1)
            {
                cursor++;
                if (content[cursor] == "\n" && cursor < content.Count - 1)
                {
                    cursor++;
                }

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
                foreach (var part in line.Split(" "))
                {
                    parts.Add(part + " ");
                }
                parts.Add("\n");
            }

            return new HintStatus(parts);
        }
    }
}
