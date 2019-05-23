using TankWorld.src.ressources.Commands;
using TankWorld.src.ressources.Events;
using TankWorld.src.ressources.Models;

namespace TankWorld.src.ressources.Panels
{
    public class MenuItem : IUpdate, IRender
    {
        MenuTextModel menuModel;
        MenuCommand command;

        //Constructors
        public MenuItem(MenuCommand command, string key, string text)
        {
            menuModel = new MenuTextModel(key, text);
            this.command = command;
        }

        //Accessors


        //Methods

        public void Action()
        {
            command.execute();
        }

        public void Render()
        {
            menuModel.Render();
        }

        public void Update()
        {
          /* empty */
        }

        public void SetPosition(int x, int y, int place)
        {
            menuModel.SetPosition(x, y, place);
        }

        public void ChangeStatus()
        {
            menuModel.ChangeStatus();
        }

    }
}
