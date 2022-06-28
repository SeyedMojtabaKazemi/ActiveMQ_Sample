using Apache.NMS;
using Apache.NMS.Util;
using System.Text;


send();

static void send()
{

    while (true)
    {
        Console.Write("Enter Your Message: ");
        string? Message = Console.ReadLine();

        if (Message is null || Message?.ToLower() == "exit")
            break;

        IConnectionFactory connectionFactory = new NMSConnectionFactory("tcp://localhost:61616");
        IConnection connection = connectionFactory.CreateConnection();
        connection.Start();
        ISession session = connection.CreateSession();

        //============================ Various Destinations

        //IDestination destination = SessionUtil.GetDestination(session, "topic://MyTopic");
        //IDestination destination = SessionUtil.GetDestination(session, "queue://MyQueue");
        //IDestination destination = SessionUtil.GetTopic(session, "MyTopic");
        IDestination destination = SessionUtil.GetQueue(session, "MyQueue");

        //============================ Various Destinations

        IMessageProducer producer = session.CreateProducer(destination);
        IBytesMessage bytesMessage = session.CreateBytesMessage(Encoding.ASCII.GetBytes(Message));

        producer.Send(bytesMessage);
        session.Close();
        connection.Stop();
    }
}