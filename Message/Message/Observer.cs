namespace Message
{
    abstract class Observer
    {
        private Context _context = new Context();
        public string Name { get; set; }

        public Observer(string name)
        {
            Name = name;
        }

        public abstract IState GetState();
        public abstract void Update(string message);
        public abstract void ChangeStatus(IState state);
    }
}