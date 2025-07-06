using StrangerGameTools.Management;

namespace StrangerGameTools.UI
{
    public class BasicMainMenuController : MenuControllerBase
    {
        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            GameStateManager.OnEnterMainMenu += HandleEnterMainMenu;
            GameStateManager.OnExitMainMenu += HandleExitMainMenu;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            GameStateManager.OnEnterMainMenu -= HandleEnterMainMenu;
            GameStateManager.OnExitMainMenu -= HandleExitMainMenu;
        }

        private void HandleEnterMainMenu() => ShowMenu();
        private void HandleExitMainMenu() => HideMenu();

        public void StartGame()
        {
            GameStateManager.StartGame();
        }
    }
}
