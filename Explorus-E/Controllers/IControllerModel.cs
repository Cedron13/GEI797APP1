using ExplorusE.Constants;
using ExplorusE.Controllers.States;
using ExplorusE.Models;
using ExplorusE.Observer;
using System.Collections.Generic;


/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Controllers
{
    internal interface IControllerModel
    {
        
        void EngineRenderEvent();

        void EngineUpdateEvent(double lag);

        void EngineProcessInputEvent();

        int[,] GetLabyrinth();

        void SetIsInvincible(bool b);

        void SetInvincibleTimer(double time);

        bool IsPaused { get; set; }
        bool IsDeadOnce { get; set; }
        bool IsDeadTwice { get; set; }

        void SetFlashPlayer(bool b);
        bool GetFlashPlayer();
        void SetFlashToxic(bool b);
        bool GetFlashToxic();
        void SetIsFlashingToxic(bool b);
        bool GetWaitLoadBubble();

        void WaitForNewBubble();

        void InitGame();
        void SetFlashToxicTimer(double b);
        void EndGameReached();

        void ModelCloseEvent();
        

        GameModel GetGameModel();

        Sprite GetPlayer();
        void SetGemCounter(int i);

        IState GetState();

        int GetTransitionTime();

        void ExitPause();

        int NewLevel();
        void AddSubscriber(IResizeEventSubscriber newBubble);

        void IsDying();



        void ShowFPS(float f);
        void ChangeFpsDisplay();

        void KillApp();
        PauseMenu GetPauseMenu();
        void LaunchMenu();
        HelpMenu GetHelpMenu();
        void LaunchHelp();
        
    }
}
