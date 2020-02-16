using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SplitOrStealBot.Features
{
    public class EarlySplit : Feature
    {
        public override void DrawMenu()
        {
            var labelRage = new GUIStyle(GUI.skin.label);
            labelRage.normal.textColor = Color.red;

            GUILayout.Label("Rage", labelRage);
            Enabled = GUILayout.Toggle(Enabled, "Early split");
        }

        public override void Init()
        {
            Globals.GameController.EarlySplit.AddListener(SplitEarly);
        }

        private void SplitEarly()
        {
            if (Enabled)
            {
                if (!Globals.GameController.hasSetAction)
                {
                    Globals.GameController.StartCoroutine(Globals.GameController.sendAction("share", Globals.GameController.gameToken));
                }
                else if (!Globals.GameController.hasLockedIn)
                {
                    Globals.GameController.StartCoroutine(Globals.GameController.lockIn(Globals.GameController.gameToken));
                }
            }
        }
    }
}
