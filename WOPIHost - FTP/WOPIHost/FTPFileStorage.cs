﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Net;

namespace WOPIHost
{
    public class FTPFileStorage : IFileStorage
    {
        public string serverPath = ConfigurationManager.AppSettings["FileServiceUrl"];
        private string username = ConfigurationManager.AppSettings["FileServiceUserName"];
        private string password = ConfigurationManager.AppSettings["FileServiceUserPassword"];

        public long GetFileSize(string name)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(serverPath + name);
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                long size = response.ContentLength;
                response.Close();

                return size;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public DateTime? GetLastModifiedTime(string name)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(serverPath + name);
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                request.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                DateTime lastModified = response.LastModified;
                response.Close();

                return lastModified;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Stream GetFile(string name)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(serverPath + name);
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                return responseStream;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int UploadFile(string name, Stream stream)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(serverPath + name);
                request.Credentials = new NetworkCredential(username, password);
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;

                using (Stream requestStream = request.GetRequestStream())
                {
                    stream.CopyTo(requestStream);
                }
            }
            catch (Exception)
            {
                return -1;
            }

            return 0;
        }

        public List<string> GetFileNames()
        {
            List<string> result = new List<string>();
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(serverPath);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.UseBinary = true;
            request.UsePassive = true;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string str;
            while ((str = reader.ReadLine()) != null)
            {
                result.Add(str);
            }

            reader.Close();
            response.Close();
            return result;
        }
    }
}