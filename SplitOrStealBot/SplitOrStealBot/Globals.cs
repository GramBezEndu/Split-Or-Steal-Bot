using SplitOrStealBot.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitOrStealBot
{
    public static class Globals
    {
        public static System.Random Random = new System.Random();
        public static GameController GameController;
        //Tier 10 win window
        public static WinController WinController;
        public static ChatNetworkController ChatNetworkController;
        public static QueueController QueueController;
        public static MainScreen MainScreen;
        public static float TimeRunning = 0f;
    }
}
