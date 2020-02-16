using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SplitOrStealBot.Features
{
    public class InvitationMessage : Feature
    {
        public List<string> messages = new List<string>() 
        { 
            "Hi. Just split.", 
            "Hello! Please split.", 
            "I'm splitting",
            "Split",
            "Split :)"
        };
        public override void DrawMenu()
        {
            GUILayout.Label("Chat Messages");
            Enabled = GUILayout.Toggle(Enabled, "Message on lobby join (random one)");
            for (int i = 0; i < messages.Count; i++)
            {
                GUILayout.BeginHorizontal();
                messages[i] = GUILayout.TextField(messages[i]);
                if (i == 0)
                {
                    GUILayout.Button("+", GUILayout.MaxWidth(25));
                }
                else
                {
                    GUILayout.Button("-", GUILayout.MaxWidth(25));
                }
                GUILayout.EndHorizontal();
            }
        }

        public override void Init()
        {
            Globals.ChatNetworkController.SendInvitationMessage.AddListener(SendInvMessage);
        }

        private void SendInvMessage()
        {
            if (Enabled)
            {
                if (!Globals.ChatNetworkController.invitationMessageSent)
                {
                    Globals.ChatNetworkController.invitationMessageSent = true;
                    Globals.ChatNetworkController.StartCoroutine(Globals.ChatNetworkController.sendChat(Globals.ChatNetworkController.gameToken, messages[Globals.Random.Next(0, messages.Count)]));
                }
            }
        }
    }
}
