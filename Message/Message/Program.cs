using System;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    class Program
    {
        static void Main(string[] args)
        {

            Subject subject = new Subject();

            CustomerObserver ugur = new CustomerObserver("Uğur");
            CustomerObserver utku = new CustomerObserver("Can");
            subject.Attach(ugur);
            subject.Attach(utku);



            subject.Message = "Herkese1";
            subject.Detach(utku);
            subject.SendMessage();
            subject.Message = "Herkese2";
            subject.SendMessage();
            subject.Attach(utku);
            subject.Detach(ugur);
            subject.Message = "Herkese3";
            subject.SendMessage();
            subject.Message = "Kişiye özel";
            subject.SendPersonalMessage(utku,ugur);
            subject.Attach(ugur);
            Console.ReadLine();
        }
    }
}
