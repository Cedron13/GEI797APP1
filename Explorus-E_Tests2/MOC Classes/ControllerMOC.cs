using ExplorusE.Controllers.States;
using ExplorusE.Models;
using ExplorusE.Observer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace Tests
{
    internal class ControllerMOC : ExplorusE.Controllers.IControllerModel
    {
        private int gemCounter = 0;
        public void EngineRenderEvent()
        {

        }

        public void EngineUpdateEvent(double lag)
        {

        }

        public void EngineProcessInputEvent()
        {

        }
        public void ModelCloseEvent()
        {

        }
        public int[,] GetLabyrinth()
        {
            return new int[,] { { 0 } };
        }

        public bool IsPaused { get; set; }
        public bool IsDeadOnce { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsDeadTwice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void InitGame()
        {

        }
        public void EndGameReached()
        {

        }

        public GameModel GetGameModel()
        {
            return null;
        }

        public Sprite GetPlayer()
        {
            return null;
        }
        public void SetGemCounter(int i)
        {
            gemCounter = i;
        }

        public IState GetState()
        {
            return null;
        }

        public int GetTransitionTime()
        {
            return 0;
        }

        public void ExitPause()
        {

        }

        public int NewLevel()
        {
            return 0;
        }

        public void SetIsInvincible(bool b)
        {
            
        }

        public void SetInvincibleTimer(double time)
        {
            
        }

        public void SetFlashPlayer(bool b)
        {
            
        }

        public bool GetFlashPlayer()
        {
            return false;
        }

        public void SetFlashToxic(bool b)
        {
            
        }

        public bool GetFlashToxic()
        {
            return false;
        }

        public void SetIsFlashingToxic(bool b)
        {
            
        }

        public bool GetWaitLoadBubble()
        {
            return false;
        }

        public void WaitForNewBubble()
        {
            
        }

        public void SetFlashToxicTimer(double b)
        {
            
        }

        public void AddSubscriber(IResizeEventSubscriber newBubble)
        {
            
        }

        public void IsDying()
        {
           
        }

        public void ShowFPS(float f)
        {
            
        }

        public void ChangeFpsDisplay()
        {
            
        }

        public void KillApp()
        {
           
        }

        public PauseMenu GetPauseMenu()
        {
            return null;
        }

        public void LaunchMenu()
        {
            
        }

        public HelpMenu GetHelpMenu()
        {
            return null;
        }

        public void LaunchHelp()
        {
            
        }

        public void NewGame()
        {
            
        }

        public bool GetTempDead()
        {
            throw new NotImplementedException();
        }
    }
}
