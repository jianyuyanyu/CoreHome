﻿using System;
using System.IO;

namespace coreHome.DatabaseOperator
{
    public static class ErrorLogManager
    {
        private static readonly string path = "C:\\Server";
        private static readonly string errorLog = path + "\\CXTK_ErrorLog.txt";

        /// <summary>
        /// 记录异常信息
        /// </summary>
        public static void SetErrorLog(Exception ex)
        {
            if (!Directory.Exists(errorLog))
                Directory.CreateDirectory(errorLog);
            if (!File.Exists(errorLog))
                File.Create(errorLog);

            using (StreamWriter sw = File.AppendText(errorLog))
            {
                sw.WriteLine(ex.ToString());
            }
        }
    }
}
