using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SplitOrStealBot.Features
{
    public class Quit : Feature
    {
        public int QuitAfter = 1;
        public override void DrawMenu()
        {
            //throw new NotImplementedException();
        }

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override void Update()
        {
            Globals.TimeRunning += Time.deltaTime;
            if(Enabled)
            {
                if(TimeSpan.FromSeconds(Globals.TimeRunning).Hours >= QuitAfter)
                {
                    Application.Quit();
                }
            }
        }
    }
}
