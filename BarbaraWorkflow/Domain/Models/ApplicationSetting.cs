using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbaraWorkflow.Domain.Models
{
    public record ApplicationSetting
    {
        public MainLabelStyle MainLabelStyle = new MainLabelStyle();
    }

    public record MainLabelStyle(
        string FontFamily = "华文中宋",
        int FontSize = 30,
        bool IsBold = false,
        string Color = ""
    );
}
