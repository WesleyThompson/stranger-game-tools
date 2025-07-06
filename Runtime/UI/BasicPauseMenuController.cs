using StrangerGameTools.Management;

namespace StrangerGameTools.UI
{
    public class BasicPauseMenuController : MenuControllerBase
    {
        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            GameStateManager.OnEnterPause += HandleEnterPause;
            GameStateManager.OnExitPause += HandleExitPause;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            GameStateManager.OnEnterPause -= HandleEnterPause;
            GameStateManager.OnExitPause -= HandleExitPause;
        }

        private void HandleEnterPause() => ShowMenu();
        private void HandleExitPause() => HideMenu();

        public void ResumeGame()
        {
            GameStateManager.StartGame();
        }
    }
}
