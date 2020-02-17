using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using SplitOrStealBot.Features;

namespace SplitOrStealBot
{
    public class CheatManager : MonoBehaviour
    {
        public List<IFeature> features = new List<IFeature>();
        //Window
        public Rect WindowRect = new Rect(10, 10, 200, 40);
        public bool ShowOptions = false;

        public bool autoBackToMenu = false;
        public bool autoAcceptLobby = true;
        public bool autoSearchLobby = true;
        public bool autoCashOutTier10 = false;
        public bool autoCloseFailedToAccept = false;
        public bool autoEarlySplit = false;

        //public bool autoEarlyLockIn = false;

        public bool autoJoinLobbyMessage = false;
        public List<string> msg = new List<string>() 
        {
            "Hi. Just split.",
            "Hello! Please split.",
        };
        public bool[] msgClick;

        private bool skinChanger = false;
        private bool displaySkinList = false;
        private Characters selectedCharacter;
        string[] characterNames;
        int[] characterValues;
        bool[] selectedCharacterCheckboxes;

        public bool this[int i]
        {
            get { return selectedCharacterCheckboxes[i]; }
            set 
            {
                if(value != selectedCharacterCheckboxes[i])
                {
                    //Toggle OFF all
                    for(int j=0;j<selectedCharacterCheckboxes.Length;j++)
                    {
                        selectedCharacterCheckboxes[j] = false;
                    }
                    //Toggle ON one selected
                    selectedCharacterCheckboxes[i] = value;
                    selectedCharacter = (Characters)characterValues[i];
                }
            }
        }

        public GUIStyle labelRage;
        public GUIStyle windowStyle;
        public Vector2 scrollPosition;

        /// <summary>
        /// Do stuff here once for initialization
        /// </summary>
        public void Start()
        {
            characterNames = Enum.GetNames(typeof(Characters));
            characterValues = (int[])Enum.GetValues(typeof(Characters));
            selectedCharacterCheckboxes = new bool[characterNames.Length];
            //First skin from list selected by default
            selectedCharacterCheckboxes[0] = true;
            selectedCharacter = (Characters)characterValues[0];

            Globals.MainScreen = FindObjectOfType<MainScreen>();
            Globals.WinController = FindObjectOfType<WinController>();
            Globals.GameController = FindObjectOfType<GameController>();
            Globals.GameController.SkinChanger.AddListener(ChangeSkin);
            Globals.ChatNetworkController = FindObjectOfType<ChatNetworkController>();
            Globals.QueueController = FindObjectOfType<QueueController>();

            features.Add(new ReturnToMainMenu());
            features.Add(new AcceptLobby());
            features.Add(new SearchLobby());
            features.Add(new CashOutTier10());
            features.Add(new CloseFailedToAcceptWindow());
            features.Add(new EarlySplit());
            features.Add(new InvitationMessage());
            features.Add(new SkinChanger(selectedCharacter));
        }

        //private void JoinQueue()
        //{
        //    if(autoSearchLobby)
        //    {
        //        if(mainScreen.isActiveAndEnabled)
        //        {
        //            //Join queue if time < 0 s.
        //            if(queueController.AutoJoinQueueInitialized && queueController.AutoJoinQueueTime < 0f)
        //            {
        //                queueController.AutoJoinQueueTime = random.Next(6, 10);
        //                queueController.AutoJoinQueueInitialized = false;
        //                queueController.button_join_queue();
        //            }
        //            //Countdown if was initialized
        //            if (queueController.AutoJoinQueueInitialized)
        //                queueController.AutoJoinQueueTime -= Time.deltaTime;
        //            //Or initialize
        //            else
        //                queueController.AutoJoinQueueInitialized = true;
        //        }
        //    }
        //}

        //private void AcceptQueue()
        //{
        //    if (autoAcceptLobby)
        //    {
        //        if (queueController.timeToAcceptQueueInvite <= 7f && !queueController.AutoAcceptQueueInitialized)
        //        {
        //            queueController.button_accept_invite();
        //            queueController.AutoAcceptQueueInitialized = true;
        //        }
        //    }
        //}

        //private void LockInEarly()
        //{
        //    if(autoEarlyLockIn)
        //    {
        //        if(!gameController.hasSetAction)
        //        {
        //            gameController.StartCoroutine(gameController.sendAction("send", gameController.gameToken));
        //        }
        //    }
        //}

        private void ChangeSkin()
        {
            if(skinChanger)
            {
                if (Globals.GameController.player_position_id == 1)
                {
                    Globals.GameController.player1GO.GetComponent<PlayerModelController>().setPlayer((int)selectedCharacter, 1);
                    Globals.GameController.player2GO.GetComponent<PlayerModelController>().setPlayer(Globals.GameController.gameModel.player_2_model_id - 1, 1);
                }
                else
                {
                    Globals.GameController.player1GO.GetComponent<PlayerModelController>().setPlayer(Globals.GameController.gameModel.player_1_model_id - 1, 1);
                    Globals.GameController.player2GO.GetComponent<PlayerModelController>().setPlayer((int)selectedCharacter, 1);
                }
            }
        }

        public void Update()
        {
            features[0].Enabled = autoBackToMenu;
            features[1].Enabled = autoAcceptLobby;
            features[2].Enabled = autoSearchLobby;
            features[3].Enabled = autoCashOutTier10;
            features[4].Enabled = autoCloseFailedToAccept;
            features[5].Enabled = autoEarlySplit;
            features[6].Enabled = autoJoinLobbyMessage;
            features[7].Enabled = skinChanger;

            if (features[6] is InvitationMessage invitationMessage)
                invitationMessage.messages = this.msg;

            if (features[7] is SkinChanger s)
                s.SelectedCharacter = selectedCharacter;

            //Add message
            if (msgClick[0] == true)
            {
                msg.Add("Hello!");
                msgClick[0] = false;
            }
            //Remove message
            for(int i=1;i<msgClick.Length;i++)
            {
                if (msgClick[i] == true)
                {
                    msg.RemoveAt(i);
                    msgClick[i] = false;
                }
            }
        }

        public void OnEnable()
        {
        }
        public void OnGUI()
        {
            labelRage = new GUIStyle(GUI.skin.label);
            labelRage.normal.textColor = Color.red;

            WindowRect = GUILayout.Window(0, WindowRect, DoMyWindow, "Split or Steal Bot", GUILayout.Width(200), GUILayout.MaxHeight(40));
        }
        public void DoMyWindow(int windowID)
        {
            GUILayout.BeginVertical();
            ShowOptions = GUILayout.Toggle(ShowOptions, "Show options");
            if(ShowOptions)
            {
                GUILayout.Label("Bot settings");
                DrawReturnToMenu();

                GUI.enabled = false;
                autoAcceptLobby = GUILayout.Toggle(autoAcceptLobby, "Accept lobby");
                autoSearchLobby = GUILayout.Toggle(autoSearchLobby, "Search lobby");
                GUI.enabled = true;

                autoCashOutTier10 = GUILayout.Toggle(autoCashOutTier10, "Cash out Tier 10");
                autoCloseFailedToAccept = GUILayout.Toggle(autoCloseFailedToAccept, "Close 'Failed to accept' window");
                GUILayout.Label("Rage", labelRage);
                autoEarlySplit = GUILayout.Toggle(autoEarlySplit, "Early split");
                DrawChatMessages();
                DrawSkins();

            }
            GUILayout.EndVertical();
            // Make a very long rect that is 20 pixels tall.
            // This will make the window be resizable by the top
            // title bar - no matter how wide it gets.
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        private void DrawChatMessages()
        {
            GUILayout.Label("Chat Messages");
            autoJoinLobbyMessage = GUILayout.Toggle(autoJoinLobbyMessage, "Message on lobby join (random one)");
            msgClick = new bool[msg.Count];
            for (int i = 0; i < msg.Count; i++)
            {
                GUILayout.BeginHorizontal();
                msg[i] = GUILayout.TextField(msg[i]);
                if (i == 0)
                {
                    msgClick[0] = GUILayout.Button("+", GUILayout.MaxWidth(25));
                }
                else
                {
                    msgClick[i] = GUILayout.Button("-", GUILayout.MaxWidth(25));
                }
                GUILayout.EndHorizontal();
            }
        }

        private void DrawSkins()
        {
            GUILayout.Label("Skins");
            skinChanger = GUILayout.Toggle(skinChanger, "Skin changer");
            displaySkinList = GUILayout.Toggle(displaySkinList, "Display skin list");
            if (displaySkinList)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MinHeight(140));
                for (int i = 0; i < Enum.GetNames(typeof(Characters)).Length; i++)
                {
                    this[i] = GUILayout.Toggle(this[i], String.Format("{0} [{1}]",
                        characterNames[i],
                        characterValues[i]
                        ));
                }
                GUILayout.EndScrollView();
            }
        }

        private void DrawReturnToMenu()
        {
            autoBackToMenu = GUILayout.Toggle(autoBackToMenu, "Back to main menu on match end");
        }
    }
}