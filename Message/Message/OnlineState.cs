using System;

namespace Message
{
    class OnlineState : IState
    {
        private static OnlineState onlineState;
        private OnlineState()
        {
            
        }

        public static OnlineState Create()
        {
            if (onlineState==null)
            {
                onlineState=new OnlineState();
            }

            return onlineState;
        }
        public void DoAction(string name, Context context)
        {
            if (context.GetState()!=null && context.GetState().ToString() == this.ToString())
            {
                Console.WriteLine("You are already online");
            }
            else
            { 
                Console.WriteLine(name+" state : Online");
                context.SetState(this); 
            }
            
        }

        public override string ToString()
        {
            return "Online";
        }
    }
}