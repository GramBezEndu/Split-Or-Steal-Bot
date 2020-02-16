using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitOrStealBot.Features
{
    public interface IFeature
    {
        bool Enabled { get; set; }
        void Init();
        void DrawMenu();
    }
}
