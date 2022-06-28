using Apache.NMS;
using Apache.NMS.Util;
using System.Text;


recieve();

static void recieve()
{
    IConnectionFactory connectionFactory = new NMSConnectionFactory("tcp://localhost:61616");
    IConnection connection = connectionFactory.CreateConnection();
    connection.Start();
    ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);

    //============================ Various Destinations

    //IDestination destination = SessionUtil.GetDestination(session, "topic://MyTopic");
    //IDestination destination = SessionUtil.GetDestination(session, "queue://MyQueue");
    //IDestination destination = SessionUtil.GetTopic(session, "MyTopic");
    IDestination destination = SessionUtil.GetQueue(session, "MyQueue");

    //============================ Various Destinations

    IMessageConsumer consumer = session.CreateConsumer(destination);

    consumer.Listener += Consumer_Listener;
    Console.Read();

}

static void Consumer_Listener(IMessage message)
{
    try
    {
        IBytesMessage? bytesMessage = message as IBytesMessage;
        Console.WriteLine("Message received: {0}", Encoding.ASCII.GetString(bytesMessage.Content));
    }
    catch (Exception ex)
    { }
}