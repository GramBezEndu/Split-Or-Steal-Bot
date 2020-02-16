using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SplitOrStealBot.Features
{
    public class CashOutTier10 : Feature
    {
        public override void DrawMenu()
        {
            Enabled = GUILayout.Toggle(Enabled, "Cash out Tier 10");
            //throw new NotImplementedException();
        }

        public override void Init()
        {
            Globals.WinController.OnStartWin.AddListener(CloseWinWindow);
        }

        private void CloseWinWindow()
        {
            if (Enabled)
            {
                Globals.WinController.closeWinWindow();
            }
        }
    }
}
