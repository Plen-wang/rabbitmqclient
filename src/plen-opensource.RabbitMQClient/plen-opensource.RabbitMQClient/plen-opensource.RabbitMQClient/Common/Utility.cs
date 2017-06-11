namespace SCM.RabbitMQClient.Common
{
    /// <summary>
    /// 静态辅助方法。
    /// </summary>
    public static class Utility
    {
        #region 判断相关

        /// <summary>
        /// 对象是否等于NULL。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// 对象是否不等于NULL。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        /// <summary>
        /// bool类型的值是否为false。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFalse(this bool value)
        {
            return value == false;
        }

        /// <summary>
        /// 是否小于等于0。
        /// </summary>
        /// <returns></returns>
        public static bool IsLessOrEqual0(this int value)
        {
            return value <= 0;
        }

        /// <summary>
        /// 是否不等于0。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotEqual0(this int value)
        {
            return value != 0;
        }

        /// <summary>
        /// 判断String是NULL或Empty。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        #endregion
    }
}