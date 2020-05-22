namespace Message
{
    interface IState
    {
        void DoAction(string name, Context context);
    }
}