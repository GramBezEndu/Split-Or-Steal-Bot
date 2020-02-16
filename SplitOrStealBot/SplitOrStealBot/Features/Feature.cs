using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitOrStealBot.Features
{
    public abstract class Feature : IFeature
    {
        public Feature()
        {
            Init();
        }

        public bool Enabled { get; set; }

        public abstract void DrawMenu();

        public abstract void Init();
    }
}
