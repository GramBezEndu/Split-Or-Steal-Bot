using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SplitOrStealBot
{
    public class Loader
    {
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<CheatManager>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        private static GameObject Load;
    }
}