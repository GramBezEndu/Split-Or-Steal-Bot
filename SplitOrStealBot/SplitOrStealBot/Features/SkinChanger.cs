using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitOrStealBot.Features
{
    public class SkinChanger : Feature
    {
        public Characters SelectedCharacter;
        public SkinChanger(Characters selectedCharacter)
        {
            SelectedCharacter = selectedCharacter;
        }
        public override void DrawMenu()
        {
            //throw new NotImplementedException();
        }

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        private void ChangeSkin()
        {
            if (Enabled)
            {
                if (Globals.GameController.player_position_id == 1)
                {
                    Globals.GameController.player1GO.GetComponent<PlayerModelController>().setPlayer((int)SelectedCharacter, 1);
                    Globals.GameController.player2GO.GetComponent<PlayerModelController>().setPlayer(Globals.GameController.gameModel.player_2_model_id - 1, 1);
                }
                else
                {
                    Globals.GameController.player1GO.GetComponent<PlayerModelController>().setPlayer(Globals.GameController.gameModel.player_1_model_id - 1, 1);
                    Globals.GameController.player2GO.GetComponent<PlayerModelController>().setPlayer((int)SelectedCharacter, 1);
                }
            }
        }
    }
}
