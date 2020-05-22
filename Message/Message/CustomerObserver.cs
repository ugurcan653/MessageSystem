using System;

namespace Message
{
    class CustomerObserver : Observer
    {
        private Context _context = new Context();
        private string Name { get; set; }

        public CustomerObserver(string name):base(name)
        {
            Name = name;
        }

        public override IState GetState()
        {
            return _context.GetState();
        }
        public override void ChangeStatus(IState state)
        {
            state.DoAction(Name,_context);
        }

        public override void Update(string message)
        {
            Console.WriteLine(Name +" " + GetState() + ", his message: " + message);
        }
    }
}