using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace STalkServer.Lib
{
    public class Function
    {
        /// <summary>
        /// 创建随机SessionID
        /// </summary>
        /// <returns></returns>
        public static string CreateSessionID()
        {
            RandomNumberGenerator RNG = RandomNumberGenerator.Create();
            byte[] buf = new byte[4];
            RNG.GetBytes(buf);

            return HexToString(buf);
        }

        /// <summary>
        /// hex 转 字符
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static string HexToString(byte[] buf)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in buf)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
