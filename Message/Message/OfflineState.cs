using System;

namespace Message
{
    class OfflineState : IState
    {
        private static OfflineState offlineState;
        private OfflineState()
        {

        }

        public static OfflineState Create()
        {
            if (offlineState == null)
            {
                offlineState = new OfflineState();
            }

            return offlineState;
        }
        public void DoAction(string name, Context context)
        {
            if (context.GetState().ToString() == this.ToString())
            {
                Console.WriteLine("You are already offline");
            }
            else
            {
                Console.WriteLine(name + " state : Offline");
                context.SetState(this);
            }

        }

        public override string ToString()
        {
            return "Offline";
        }
    }
}