using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, MOTHERBOARD, PROCESSOR, RAM, STORAGE_SSD, GRAPHICS_CARD, CPU_CASE, OS, MONITOR
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            // this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to PC Assemblers. Lets build your future PC!");
                    aMessages.Add("For starters, which motherboard would you like to install for your desktop?");
                    this.nCur = State.MOTHERBOARD;
                    break;
                case State.MOTHERBOARD:
                    this.oOrder.Motherboard = sInMessage;
                    // this.oOrder.Save();
                    aMessages.Add("Now, which processor would you like to install in your PC? Be Specific.  " /*+ this.oOrder.Size + " Shawarama?"*/);
                    this.nCur = State.PROCESSOR;
                    break;
                case State.PROCESSOR:
                    this.oOrder.Processor = sInMessage;
                    aMessages.Add("Now, which RAM would you like to install into this desktop and what is size of the RAM you would like " /*+ this.oOrder.Size + " " + sProtein + " Shawarama?"*/);
                    this.nCur = State.RAM;
                    break;
                case State.RAM:
                    this.oOrder.RAM = sInMessage;
                    aMessages.Add("Which storage SSD would you like to fit into your PC? Mention the size as well.");
                    this.nCur = State.STORAGE_SSD;
                    break;
                case State.STORAGE_SSD:
                    this.oOrder.Storage = sInMessage;
                    aMessages.Add("Now which graphics card would you like to install in your desktop? Mention the graphics card size as well.");
                    this.nCur = State.GRAPHICS_CARD;
                    break;

            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
