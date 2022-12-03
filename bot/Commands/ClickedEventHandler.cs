using DSharpPlus;
using DSharpPlus.EventArgs;
using System.ComponentModel.Design;

namespace bot.Commands
{
    public class ClickedEventHandler
    {
        ComponentInteractionCreateEventArgs e;
        public ClickedEventHandler(ComponentInteractionCreateEventArgs args)
        {
            args = e;

            switch (e.Id)
            {
                case "button":
                    //code
                    break;
                default:

                    break;
            }
                    
        }
    }
}
