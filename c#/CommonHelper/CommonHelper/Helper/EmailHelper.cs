using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 这是延迟发送的委托，该委托只有在真正要发送邮件时才会被调用
    /// </summary>
    public delegate void SendLazyDelegate();

    /// <summary>
    /// 邮件发送者实体类
    /// </summary>
    public class EmailSenderEntity
    {
        /// <summary>
        /// 负责发送邮件的邮件服务器地址
        /// </summary>
        public string MailHost { set; get; }

        /// <summary>
        /// 负责发送邮件的用户名
        /// </summary>
        public string MailUserName { set; get; }

        /// <summary>
        /// 负责发送邮件的用户密码
        /// </summary>
        public string MailPassword { set; get; }

        /// <summary>
        /// 最近一次发送时间
        /// </summary>
        public DateTime LastSenderTime { set; get; }

        /// <summary>
        /// 冷却时间，这里统一取60秒，冷却时间内不发送邮件由其他用户发送
        /// </summary>
        public int Cooldown { get { return 60; } }
    }

    public interface TryAgain
    {
        bool CheckTry(Exception ex);
    }

    public class DefaultTryAgain : TryAgain
    {
        public bool CheckTry(Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// 发送电子邮件的帮助类
    /// </summary>
    public class EmailHelper
    {

        static DefaultTryAgain _defaultTryAgain;

        static List<EmailSenderEntity> _emailSenderEntityList;

        /// <summary>
        /// 随机下标
        /// </summary>
        static uint _randomIndex;

        static EmailHelper()
        {
            _defaultTryAgain = new DefaultTryAgain();
            _randomIndex = (uint)new Random().Next();
            string emailSenderEntities = ConfigurationManager.AppSettings["EmailSenderEntities"];
            if (!string.IsNullOrEmpty(emailSenderEntities))
            {
                _emailSenderEntityList = new List<EmailSenderEntity>();
                foreach (JObject jobject in JArray.Parse(emailSenderEntities))
                {
                    _emailSenderEntityList.Add(new EmailSenderEntity
                    {
                        MailHost = Convert.ToString(jobject["MailHost"]),
                        MailUserName = Convert.ToString(jobject["MailUserName"]),
                        MailPassword = Convert.ToString(jobject["MailPassword"])
                    });
                }
            }
            //SendLazyThread();
        }

        /// <summary>
        /// 当前账号池里面的剩余冷却时间最短的账号，单位：秒
        /// </summary>
        /// <returns></returns>
        public static int MinCooldown()
        {
            int min = int.MaxValue;
            int temp;
            lock (_emailSenderEntityList)
            {
                foreach (EmailSenderEntity emailSenderEntity in _emailSenderEntityList)
                {
                    temp = (int)(emailSenderEntity.LastSenderTime.AddSeconds(emailSenderEntity.Cooldown) - DateTime.Now).TotalSeconds;
                    temp = temp < 0 ? 0 : temp;
                    min=temp < min ? temp : min;
                }
            }
            return min;
        }



        /// <summary>
        /// 马上发出邮件
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="accept">邮件的单个接收人</param>
        /// <param name="attachmentPaths">附件，可有可无</param>
        /// <returns>如果有空闲的账号，则发送成功，返回true，如果没有空闲账号，返回false</returns>
        public static bool SendRightRow(string subject, string body, string accept, params string[] attachmentPaths)=>
            SendRightRow(subject, body, new string[] { accept },attachmentPaths);

        /// <summary>
        /// 马上发出邮件
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="accepts">邮件接收人，可以有多个</param>
        /// <param name="attachmentPaths">附件，可有可无</param>
        /// <returns>如果有空闲的账号，则发送成功，返回true，如果没有空闲账号，返回false</returns>
        public static bool SendRightRow(string subject, string body,string[] accepts,params string[] attachmentPaths)
        {
            if (accepts.Length == 0)
            {
                throw new Exception("收件人不能为空！");
            }
            if (accepts.Length > 20)
            {
                throw new Exception("收件人数量不能超过20人！");
            }
            TryAgain:
            EmailSenderEntity emailSenderEntity;
            int retryCount = 0;
            lock (_emailSenderEntityList)
            {
                do
                {
                    retryCount++;
                    emailSenderEntity = _emailSenderEntityList[(int)++_randomIndex % _emailSenderEntityList.Count];
                } while (emailSenderEntity.LastSenderTime.AddSeconds(emailSenderEntity.Cooldown) > DateTime.Now && retryCount < _emailSenderEntityList.Count);
            }
            if (emailSenderEntity.LastSenderTime.AddSeconds(emailSenderEntity.Cooldown) > DateTime.Now)
            {
                return false;
            }

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(emailSenderEntity.MailUserName),
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = body,
                BodyEncoding = Encoding.UTF8
            };
            Attachment attachment;
            foreach (string attachmentPath in attachmentPaths)
            {
                attachment = new Attachment(attachmentPath);
                attachment.ContentDisposition.CreationDate = File.GetCreationTime(attachmentPath);
                attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(attachmentPath);
                attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(attachmentPath);
                mailMessage.Attachments.Add(attachment);
            }
            for (int i = 0, len = accepts.Length; i < len; i++)
            {
                mailMessage.To.Add(accepts[i]);
            }
            try
            {
                emailSenderEntity.LastSenderTime = DateTime.Now;
                new SmtpClient
                {
                    Credentials = new NetworkCredential(emailSenderEntity.MailUserName, emailSenderEntity.MailPassword),
                    Host = emailSenderEntity.MailHost,
                }.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                if (_defaultTryAgain.CheckTry(ex))
                    goto TryAgain;
                else
                    throw ex;
            }
        }
    }
}