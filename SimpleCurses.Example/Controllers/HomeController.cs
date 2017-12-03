using SimpleCurses.Interfaces;
using SimpleCurses.Views;

namespace SimpleCurses.Example.Controllers
{
    public class HomeController: IController
    {
        private IRenderable menuView = null;
        private IRenderable currentView = null;

        public HomeController()
        {
            menuView = new MenuView(new [] {"Show a Message Box", "Ask me Some Questions"});
            menuView.Finished += HandleSelection;

            currentView = menuView;
        }

        private void HandleSelection(IRenderable sender, object selection)
        {
            switch ((int) selection)
            {
                case 0:
                    currentView = new MessageBoxView("This is a sample message box view!");
                    currentView.Finished += (renderable, response) => { currentView = menuView; };
                    break;
                case 1:
                    currentView = new MultipleQuestionsView(new [] {"What is your name?", "Where do you live?"});
                    currentView.Finished += (renderable, response) => { currentView = menuView; };
                    break;
            }
        }
        
        public IRenderable CurrentView()
        {
            return currentView;
        }
    }
}