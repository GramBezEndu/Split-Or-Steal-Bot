using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SplitOrStealBot.Features
{
    public class CloseFailedToAcceptWindow : Feature
    {
        public override void DrawMenu()
        {
            //throw new NotImplementedException();
            Enabled = GUILayout.Toggle(Enabled, "Close 'Failed to accept' window");
        }

        public override void Init()
        {
            //throw new NotImplementedException();
        }
    }
}
