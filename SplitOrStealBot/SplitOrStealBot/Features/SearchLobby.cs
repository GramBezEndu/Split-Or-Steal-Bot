using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SplitOrStealBot.Features
{
    public class SearchLobby : Feature
    {
        public SearchLobby()
        {
            Enabled = true;
        }
        public override void DrawMenu()
        {
            GUI.enabled = false;
            Enabled = GUILayout.Toggle(Enabled, "Search lobby");
            GUI.enabled = true;
            //throw new NotImplementedException();
        }

        public override void Init()
        {
            //throw new NotImplementedException();
        }
    }
}
