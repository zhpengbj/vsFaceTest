using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
namespace MSMQ
{
    /// <summary>
    /// 该类实现从消息对列中发送和接收消息的主要功能 
    /// </summary>
    public class DTcmsQueue : IDisposable
    {

        //指定消息队列事务的类型，Automatic 枚举值允许发送外部事务和从处部事务接收
        protected MessageQueueTransactionType transactionType = MessageQueueTransactionType.Automatic;
        protected MessageQueue queue;
        protected TimeSpan timeout;
        //实现构造函数
        public DTcmsQueue(string queuePath, int timeoutSeconds)
        {
            Createqueue(queuePath);
            queue = new MessageQueue(queuePath);
            timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeoutSeconds));

            //设置当应用程序向消息对列发送消息时默认情况下使用的消息属性值
            queue.DefaultPropertiesToSend.AttachSenderId = false;
            queue.DefaultPropertiesToSend.UseAuthentication = false;
            queue.DefaultPropertiesToSend.UseEncryption = false;
            queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;
            queue.DefaultPropertiesToSend.UseJournalQueue = false;
        }

        /// <summary>
        /// 继承类将从自身的Receive方法中调用以下方法，该方法用于实现消息接收
        /// </summary>
        public virtual object Receive()
        {
            try
            {
                using (Message message = queue.Receive(timeout, transactionType))
                    return message;
            }
            catch (MessageQueueException mqex)
            {
                if (mqex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                    throw new TimeoutException();

                throw;
            }
        }

        /// <summary>
        /// 继承类将从自身的Send方法中调用以下方法，该方法用于实现消息发送
        /// </summary>
        public virtual void Send(object msg)
        {
            queue.Send(msg, transactionType);
        }

        /// <summary>
        /// 通过Create方法创建使用指定路径的新消息队列
        /// </summary>
        /// <param name="queuePath"></param>
        public static void Createqueue(string queuePath)
        {
            try
            {
                if (!MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Create(queuePath, true);  //创建事务性的专用消息队列
                    //logger.Debug("创建队列成功！");
                }
            }
            catch (MessageQueueException e)
            {
                //logger.Error(e.Message);
            }
        }

        #region 实现 IDisposable 接口成员
        public void Dispose()
        {
            queue.Dispose();
        }
        #endregion
    }

    /// <summary>
    /// 枚举,操作类型是增加还是删除
    /// </summary>
    public enum JobType { Add, Remove }
    /// <summary>
    /// 任务类,包括任务的Id ,操作的类型
    /// </summary>
    [Serializable]
    public class IndexJob
    {
        public int Id { get; set; }
        public JobType JobType { get; set; }
        public string context { get; set; }
    }
}
