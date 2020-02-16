using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SplitOrStealBot.Features
{
    public class ReturnToMainMenu : Feature
    {
        public override void DrawMenu()
        {
            Enabled = GUILayout.Toggle(Enabled, "Back to main menu on match end");
        }

        public override void Init()
        {
            Globals.GameController.OnFinishEndScreen.AddListener(CloseEndScreenWin);
        }

        private void CloseEndScreenWin()
        {
            if (Enabled)
            {
                Globals.GameController.button_main_menu();
            }
        }
    }
}
