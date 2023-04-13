using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, MOTHERBOARD, PROCESSOR, RAM, STORAGE_SSD, GRAPHICS_CARD, CPU_CASE, OS, MONITOR, CONFIRM_ORDER, RESET_ORDER
        }

        private State nCur = State.WELCOMING;
        public Order oOrder;    //Made this public from private so that I can access it to run tests

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
                    aMessages.Add($"1. {this.oOrder.motherBoardBrands[0]}");
                    aMessages.Add($"2. {this.oOrder.motherBoardBrands[1]}");
                    aMessages.Add($"3. {this.oOrder.motherBoardBrands[2]}");
                    aMessages.Add($"4. {this.oOrder.motherBoardBrands[3]}");
                    aMessages.Add("Please only reply with a number between 1 to 4.");
                    this.nCur = State.MOTHERBOARD;
                    break;
                case State.MOTHERBOARD:
                    if(int.TryParse(sInMessage, out int itemNumber) && itemNumber <= 4 && itemNumber > 0)   //THIS CONDITION WILL TRY TO PARSE THE RESPONSE OF USER TO SEE IF THEY PASSED A VALID RESPONSE WITH A NUMBER WITHIN THE RANGE OR NOT.
                    {
                        this.oOrder.Motherboard = this.oOrder.motherBoardBrands[int.Parse(sInMessage) - 1];
                        this.oOrder.Save();
                        aMessages.Add("Now, which processor would you like to install in your PC?");
                        aMessages.Add($"1. {this.oOrder.processorBrands[0]}");
                        aMessages.Add($"2. {this.oOrder.processorBrands[1]}");
                        aMessages.Add($"3. {this.oOrder.processorBrands[2]}");
                        aMessages.Add($"4. {this.oOrder.processorBrands[3]}");
                        aMessages.Add($"5. {this.oOrder.processorBrands[4]}");
                        aMessages.Add($"6. {this.oOrder.processorBrands[5]}");
                        aMessages.Add("Please only reply with a number between 1 to 6.");
                        this.nCur = State.PROCESSOR;
                        break;
                    }
                    else    //ELSE IT WILL PROMPT THE USER TO GIVE THE ANSWER AGAIN AND SET THE CURRENT STATE TO THIS STATE AGAIN
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.MOTHERBOARD;
                        break;
                    }

                case State.PROCESSOR:
                    if(int.TryParse(sInMessage, out itemNumber)  && itemNumber <= 6 && itemNumber > 0)
                    {
                        this.oOrder.Processor = this.oOrder.processorBrands[int.Parse(sInMessage) - 1];
                        this.oOrder.Save();
                        aMessages.Add("Now, which RAM would you like to install into this desktop and what is size of the RAM you would like ");
                        aMessages.Add($"1. {this.oOrder.ramBrands[0]}");
                        aMessages.Add($"2. {this.oOrder.ramBrands[1]}");
                        aMessages.Add($"3. {this.oOrder.ramBrands[2]}");
                        aMessages.Add($"4. {this.oOrder.ramBrands[3]}");
                        aMessages.Add("Please only reply with a number between 1 to 4.");
                        this.nCur = State.RAM;
                        break;
                    }
                    else
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.PROCESSOR;
                        break;
                    }
                case State.RAM:
                    if(int.TryParse(sInMessage, out itemNumber)  && itemNumber <= 4 && itemNumber > 0)
                    {
                        this.oOrder.RAM = this.oOrder.ramBrands[int.Parse(sInMessage) - 1];
                        this.oOrder.Save();
                        aMessages.Add("Which storage SSD would you like to fit into your PC? Mention the size as well.");
                        aMessages.Add($"1. {this.oOrder.storageSSDBrands[0]}");
                        aMessages.Add($"2. {this.oOrder.storageSSDBrands[1]}");
                        aMessages.Add($"3. {this.oOrder.storageSSDBrands[2]}");
                        aMessages.Add($"4. {this.oOrder.storageSSDBrands[3]}");
                        aMessages.Add("Please only reply with a number between 1 to 4.");
                        this.nCur = State.STORAGE_SSD;
                        break;
                    }
                    else
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.RAM;
                        break;
                    }

                case State.STORAGE_SSD:
                    if(int.TryParse(sInMessage, out itemNumber)  && itemNumber <= 4 && itemNumber > 0)
                    {
                        this.oOrder.Storage = this.oOrder.storageSSDBrands[int.Parse(sInMessage) - 1];
                        this.oOrder.Save();
                        aMessages.Add("Now which graphics card would you like to install in your desktop? Mention the graphics card size as well.");
                        aMessages.Add($"1. {this.oOrder.graphicsCardBrands[0]}");
                        aMessages.Add($"2. {this.oOrder.graphicsCardBrands[1]}");
                        aMessages.Add($"3. {this.oOrder.graphicsCardBrands[2]}");
                        aMessages.Add($"4. {this.oOrder.graphicsCardBrands[3]}");
                        aMessages.Add("Please only reply with a number between 1 to 4.");
                        this.nCur = State.GRAPHICS_CARD;
                        break;
                    }
                    else
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.STORAGE_SSD;
                        break;
                    }
                 
                case State.GRAPHICS_CARD:
                    if(int.TryParse(sInMessage, out itemNumber)  && itemNumber <= 4 && itemNumber > 0)
                    {
                        this.oOrder.Graphics = this.oOrder.graphicsCardBrands[int.Parse(sInMessage) - 1];
                        this.oOrder.Save();
                        aMessages.Add("Which CPU Case would you like to select for your desktop build?");
                        aMessages.Add($"1. {this.oOrder.cpuCaseBrands[0]}");
                        aMessages.Add($"2. {this.oOrder.cpuCaseBrands[1]}");
                        aMessages.Add($"3. {this.oOrder.cpuCaseBrands[2]}");
                        aMessages.Add($"4. {this.oOrder.cpuCaseBrands[3]}");
                        aMessages.Add("Please only reply with a number between 1 to 4.");
                        this.nCur = State.CPU_CASE;
                        break;
                    }
                    else
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.GRAPHICS_CARD;
                        break;
                    }
                  
                case State.CPU_CASE:
                    if(int.TryParse(sInMessage, out itemNumber)  && itemNumber <= 4 && itemNumber > 0)
                    {
                        this.oOrder.CPU_Case = this.oOrder.cpuCaseBrands[int.Parse(sInMessage) - 1];
                        this.oOrder.Save();
                        aMessages.Add("Which OS would you like to install in your desktop?");
                        aMessages.Add($"1. {this.oOrder.os[0]}");
                        aMessages.Add($"2. {this.oOrder.os[1]}");
                        aMessages.Add($"3. {this.oOrder.os[2]}");
                        aMessages.Add($"4. {this.oOrder.os[3]}");
                        aMessages.Add("Please only reply with a number between 1 to 4.");
                        this.nCur = State.OS;
                        break;
                    }
                    else
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.CPU_CASE;
                        break;
                    }
                  
                case State.OS:
                    if(int.TryParse(sInMessage, out itemNumber)  && itemNumber <= 4 && itemNumber > 0)
                    {
                        this.oOrder.OS = this.oOrder.os[int.Parse(sInMessage) - 1];
                        this.oOrder.Save();
                        aMessages.Add("Which monitor would you like to accompany with your cpu? Mention in details.");
                        aMessages.Add($"1. {this.oOrder.monitorBrands[0]}");
                        aMessages.Add($"2. {this.oOrder.monitorBrands[1]}");
                        aMessages.Add($"3. {this.oOrder.monitorBrands[2]}");
                        aMessages.Add($"4. {this.oOrder.monitorBrands[3]}");
                        aMessages.Add("Please only reply with a number between 1 to 4.");
                        this.nCur = State.MONITOR;
                        break;
                    }
                    else
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.OS;
                        break;
                    }
                 
                case State.MONITOR:
                    if(int.TryParse(sInMessage, out itemNumber)  && itemNumber <= 4 && itemNumber > 0)
                    {
                        this.oOrder.Monitor = this.oOrder.monitorBrands[int.Parse(sInMessage) - 1];
                        this.oOrder.Save();
                        aMessages.Add("Would you like to confirm your order? Type 'Y' or 'N' as your response.");
                        this.nCur = State.CONFIRM_ORDER;    //next state is confirm order state
                        break;
                    }
                    else
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.MONITOR;
                        break;
                    }
                  
                case State.CONFIRM_ORDER:
                    if(sInMessage.ToLower() == "y" || sInMessage.ToLower() == "n")
                    {
                        this.oOrder.ConfirmOrder = sInMessage;
                        this.oOrder.Save();
                        if(this.oOrder.ConfirmOrder.ToLower() == "y")
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
                            aMessages.Add("We will now reset the order, so you can use the chat again.");
                            this.nCur = State.RESET_ORDER;
                            break;
                        }
                        else if(this.oOrder.ConfirmOrder.ToLower() == "n")
                        {
                            aMessages.Add("You have chosen not to confirm your order. Thank you for using our chat service.");
                            aMessages.Add("Launch the chat service again, if you would like to build another desktop.");
                            aMessages.Add("We will now reset the order, so you can use the chat again.");
                            aMessages.Add("Say hello to begin the PC Building process again!");
                            this.nCur = State.RESET_ORDER;
                            break;
                        }
                        break;
                    }
                    else
                    {
                        aMessages.Add("Please enter a valid response as requested!!");
                        aMessages.Add("Try answering again!!");
                        this.nCur = State.CONFIRM_ORDER;
                        break;
                    }
                //RESETS/CREATES A NEW ORDER AND RESETS BACK TO THE WELCOME STATE.
                case State.RESET_ORDER:
                    this.oOrder = new Order();
                    this.oOrder.TokenNumber = GenerateRandomTokenNumber();
                    this.nCur = State.WELCOMING;
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
