using System.Collections.Generic;
using System.Linq;

namespace Message
{
    class Subject//subject
    {
        private OnlineState onlineState = OnlineState.Create();
        private OfflineState offlineState = OfflineState.Create();
        private InvisibleState invisibleState = InvisibleState.Create();
        private readonly List<Observer> _onlineObservers = new List<Observer>();
        private readonly List<Observer> _offlineObservers = new List<Observer>();
        private readonly Dictionary<Observer, List<string>> _queue = new Dictionary<Observer, List<string>>();
        public string Message { get; set; }

        public void SendMessage()
        {
            SendNotify(Message);
        }

        public void SendPersonalMessage(Observer senderObserver ,Observer receiverObserver)
        {
            SendNotifyToSomeone(Message, senderObserver, receiverObserver);
        }

        public void Attach(Observer observer)
        {
            _onlineObservers.Add(observer);
            observer.ChangeStatus(onlineState);
            Observer off=_offlineObservers.FirstOrDefault(x => x == observer);
            if (off!=null)
            {
                _offlineObservers.Remove(observer);
                GetPendingMessages(observer);
            }
        }

        public void Detach(Observer observer)
        {
            observer.ChangeStatus(offlineState);
            _offlineObservers.Add(observer);
            _onlineObservers.Remove(observer);
        }

        private void Notify(string message)
        {
            foreach (var observer in _onlineObservers)
            {
                observer.Update(message);
            }

            foreach (var observer in _offlineObservers)
            {
                int count = _queue.Count(x => x.Key == observer);
                if (count > 0)
                {
                    List<string> temporary = new List<string>();

                    var list=_queue.Where(x => x.Key == observer).Select(x => x.Value).First();
                    foreach (var value in list)
                    {
                        temporary.Add(value);
                    }
                    temporary.Add(message);
                    _queue.Remove(observer);
                    _queue.Add(observer, temporary);
                }
                else
                {
                    List<string> temporary = new List<string>();
                    temporary.Add(message);
                    _queue.Add(observer, temporary);
                }
            }
        }
        private void NotifyToSomeone(string message, Observer senderObserver, Observer receiverObserver)
        {
            var state = receiverObserver.GetState().ToString();
            string personalMessage = senderObserver.Name + " sent message to you: " + message;
            string myMessage = senderObserver.Name + ", your message sent to " + receiverObserver.Name + " " + message;
            senderObserver.Update(myMessage);
            if (state == "Online")
            {
                receiverObserver.Update(personalMessage);
            }
            else
            {
                int count = _queue.Count(x => x.Key == receiverObserver);
                if (count > 0)
                {
                    List<string> temporary = new List<string>();

                    var list = _queue.Where(x => x.Key == receiverObserver).Select(x => x.Value).First();
                    foreach (var value in list)
                    {
                        temporary.Add(value);
                    }
                    temporary.Add(personalMessage);
                    _queue.Remove(receiverObserver);
                    _queue.Add(receiverObserver, temporary);
                }
                else
                {
                    List<string> temporary = new List<string>();
                    temporary.Add(personalMessage);
                    _queue.Add(receiverObserver, temporary);
                }
            }
            
        }
        private void SendNotifyToSomeone(string message, Observer senderObserver,Observer receiverObserver)
        {
            NotifyToSomeone(message, senderObserver, receiverObserver);
        }
        private void SendNotify(string message)
        {
            Notify(message);
        }

        public void GetPendingMessages(Observer observer)
        {
            var list = _queue.Where(x => x.Key == observer).Select(x => x.Value).First();
            foreach (var value in list)
            {
                observer.Update(value);
            }

            _queue.Remove(observer);
        }
    }
}