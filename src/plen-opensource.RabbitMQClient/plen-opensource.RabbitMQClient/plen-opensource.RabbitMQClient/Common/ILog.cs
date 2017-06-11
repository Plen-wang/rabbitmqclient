using System;

namespace SCM.RabbitMQClient.Common
{
    /// <summary>
    /// 内部使用的LOG接口。
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 写入异常信息。
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="exception">Exception.</param>
        void WriteException(string title, Exception exception);

        /// <summary>
        /// 写入提示信息。
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        void WriteInfo(string title, string message);
    }
}