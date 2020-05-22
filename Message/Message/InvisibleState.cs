using System;

namespace Message
{
    class InvisibleState : IState
    {
        private static InvisibleState invisibleState;
        private InvisibleState()
        {

        }

        public static InvisibleState Create()
        {
            if (invisibleState == null)
            {
                invisibleState = new InvisibleState();
            }

            return invisibleState;
        }
        public void DoAction(string name, Context context)
        {
            if (context.GetState().ToString() == this.ToString())
            {
                Console.WriteLine("You are already Invisible");
            }
            else
            {
                Console.WriteLine(name + " state : Invisible");
                context.SetState(this);
            }

        }

        public override string ToString()
        {
            return "Offline";
        }
    }
}