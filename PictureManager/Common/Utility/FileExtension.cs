using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.Utility
{
    public class FileExtension
    {
        public static bool IsImage(string fileUrl)
        {
            bool result = false;//下载结果

            WebResponse response = null;
            try
            {
                WebRequest req = WebRequest.Create(fileUrl);

                response = req.GetResponse();
                if (response.ContentType.StartsWith("image"))
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }
    }
}
