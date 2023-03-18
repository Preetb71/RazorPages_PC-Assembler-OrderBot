using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, MOTHERBOARD, PROCESSOR, RAM, STORAGE_SSD, GRAPHICS_CARD, CPU_CASE, OS, MONITOR, CONFIRM_ORDER
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)  
        {
            this.oOrder = new Order();
            this.oOrder.TokenNumber = GenerateRandomTokenNumber();
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
                    this.oOrder.Save();
                    aMessages.Add("Now, which processor would you like to install in your PC? Be Specific.  " /*+ this.oOrder.Size + " Shawarama?"*/);
                    this.nCur = State.PROCESSOR;
                    break;
                case State.PROCESSOR:
                    this.oOrder.Processor = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Now, which RAM would you like to install into this desktop and what is size of the RAM you would like " /*+ this.oOrder.Size + " " + sProtein + " Shawarama?"*/);
                    this.nCur = State.RAM;
                    break;
                case State.RAM:
                    this.oOrder.RAM = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Which storage SSD would you like to fit into your PC? Mention the size as well.");
                    this.nCur = State.STORAGE_SSD;
                    break;
                case State.STORAGE_SSD:
                    this.oOrder.Storage = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Now which graphics card would you like to install in your desktop? Mention the graphics card size as well.");
                    this.nCur = State.GRAPHICS_CARD;
                    break;
                case State.GRAPHICS_CARD:
                    this.oOrder.Graphics = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Which CPU Case would you like to select for your desktop build?");
                    this.nCur = State.CPU_CASE;
                    break;
                case State.CPU_CASE:
                    this.oOrder.CPU_Case = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Which OS would you like to install in your desktop?");
                    this.nCur = State.OS;
                    break;
                case State.OS:
                    this.oOrder.OS = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Which monitor would you like to accompany with your cpu? Mention in details.");
                    this.nCur = State.MONITOR;
                    break;
                case State.MONITOR:
                    this.oOrder.Monitor = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Would you like to confirm your order? Type 'Y' or 'N' as your response.");
                    this.nCur = State.CONFIRM_ORDER;    //next state is confirm order state
                    break;
                case State.CONFIRM_ORDER:
                    this.oOrder.ConfirmOrder = sInMessage;
                    this.oOrder.Save();
                    if(this.oOrder.ConfirmOrder == "Y" || this.oOrder.ConfirmOrder == "y")
                    {
                        aMessages.Add("Your order is successfully confirmed!");
                        aMessages.Add("Here are your custom desktop specifications:");
                        aMessages.Add($"Token Number: {this.oOrder.TokenNumber}");
                        aMessages.Add($"Motherboard: {this.oOrder.Motherboard}");
                        aMessages.Add($"Processor: {this.oOrder.Processor}");
                        aMessages.Add($"RAM: {this.oOrder.RAM}");
                        aMessages.Add($"Storage SSD: {this.oOrder.Storage}");
                        aMessages.Add($"Graphics Card: {this.oOrder.Graphics}");
                        aMessages.Add($"CPU Case: {this.oOrder.CPU_Case}");
                        aMessages.Add($"OS: {this.oOrder.OS}");
                        aMessages.Add($"Monitor: {this.oOrder.Monitor}");
                        aMessages.Add("Keep the token number safe and show it to the physical store and finish your payment transaction there.");
                        aMessages.Add("Thank you for building a PC with us!");
                    }
                    else if(this.oOrder.ConfirmOrder == "N" || this.oOrder.ConfirmOrder == "n")
                    {
                        aMessages.Add("You have chosen not to confirm your order. Thank you for using our chat service.");
                        aMessages.Add("Launch the chat service again, if you would like to build another desktop.");
                    }
                    //PLANNING ON ADDING A LOOP HERE SO WHEN THE CURRENT TRANSACTION IS DONE USER CAN RESET THE SESSION AND BUILD A PC AGAIN.
                    // this.nCur = State.WELCOMING;    //set back to welcome state to order again
                    break;
            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

        //THIS FUNCTION WILL GENERATE A RANDOM ALPHANUMERIC STRING WHICH WILL ACT AS OUR TOKEN NUMBER AND WILL BE CALLED WHEN A NEW SESSION IS CREATED.
        public String GenerateRandomTokenNumber()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  
            var random = new Random();  
            var resultToken = new string(  
            Enumerable.Repeat(allChar , 8)  
            .Select(token => token[random.Next(token.Length)]).ToArray());   
            string authToken = resultToken.ToString();  
            return authToken;
        }

    }
}
