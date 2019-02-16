using System;
using System.Configuration;
using System.Messaging;

namespace MSMQ
{
    /// <summary>
    /// 该类实现从消息队列中发送和接收订单消息
    /// </summary>
    public class OrderJob : DTcmsQueue
    {
        // 获取配置文件中有关消息队列路径的参数
        private static readonly string queuePath = @".\Private$\zhptest";
        //ConfigurationManager.AppSettings["OrderQueuePath"];
        private static int queueTimeout = 20;
        //实现构造函数
        public OrderJob()
            : base(queuePath, queueTimeout)
        {
            // 设置消息的序列化采用二进制方式 
            queue.Formatter = new BinaryMessageFormatter();
        }

        /// <summary>
        /// 调用PetShopQueue基类方法，实现从消息队列中接收订单消息
        /// </summary>
        /// <returns>订单对象 OrderInfo</returns>
        public new IndexJob Receive()
        {
            // 指定消息队列事务的类型，Automatic枚举值允许发送发部事务和从外部事务接收
            base.transactionType = MessageQueueTransactionType.Automatic;
            return (IndexJob)((Message)base.Receive()).Body;
        }
        //该方法实现从消息队列中接收订单消息
        public IndexJob Receive(int timeout)
        {
            base.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return Receive();
        }

        /// <summary>
        /// 调用PetShopQueue基类方法，实现从消息队列中发送订单消息
        /// </summary>
        /// <param name="orderMessage">订单对象 OrderInfo</param>
        public void Send(IndexJob orderMessage)
        {
            // 指定消息队列事务的类型，Single枚举值用于单个内部事务的事务类型
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }
    }
}