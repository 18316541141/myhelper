using CommonHelper.staticVar;
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
    /// 邮件发送者实体类
    /// </summary>
    public sealed class EmailSenderEntity
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
        /// 冷却时间，这里统一取60秒，冷却时间内不能发送邮件
        /// </summary>
        public int Cooldown { get { return 60; } }
    }

    /// <summary>
    /// 发送电子邮件的帮助类
    /// </summary>
    public static class EmailHelper
    {

        public static List<EmailSenderEntity> EmailSenderEntityList { set; get; }

        /// <summary>
        /// 随机下标
        /// </summary>
        static uint _randomIndex;

        static EmailHelper()
        {
            _randomIndex = (uint)new Random().Next();
            //电子邮件发送者的json配置。
            try
            {
                string emailSenderEntities = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.EmailSenderEntities"];
                if (!string.IsNullOrEmpty(emailSenderEntities))
                {
                    EmailSenderEntityList = new List<EmailSenderEntity>();
                    foreach (JObject jobject in JArray.Parse(emailSenderEntities))
                    {
                        EmailSenderEntityList.Add(new EmailSenderEntity
                        {
                            MailHost = Convert.ToString(jobject["MailHost"]),
                            MailUserName = Convert.ToString(jobject["MailUserName"]),
                            MailPassword = Convert.ToString(jobject["MailPassword"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取邮箱配置失败，失败原因：{ex.Message}，请添加邮箱配置或手动设置EmailSenderEntityList列表。");
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
            lock (EmailSenderEntityList)
            {
                foreach (EmailSenderEntity emailSenderEntity in EmailSenderEntityList)
                {
                    temp = (int)(emailSenderEntity.LastSenderTime.AddSeconds(emailSenderEntity.Cooldown) - DateTime.Now).TotalSeconds;
                    temp = temp < 0 ? 0 : temp;
                    min = temp < min ? temp : min;
                }
            }
            return min;
        }



        /// <summary>
        /// 马上发出邮件
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="accept">邮件的单个接收人，可以有多个（例如：1320579501@qq.com、）</param>
        /// <param name="attachmentPaths">附件，可有可无</param>
        /// <returns>如果有空闲的账号，则发送成功，返回true，如果没有空闲账号，返回false</returns>
        public static bool SendRightRow(string subject, string body, string accept, params string[] attachmentPaths) =>
            SendRightRow(subject, body, new string[] { accept }, attachmentPaths);

        /// <summary>
        /// 马上发出邮件
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="accepts">邮件接收人，可以有多个（例如：1320579501@qq.com、）</param>
        /// <param name="attachmentPaths">附件，可有可无</param>
        /// <returns>如果有空闲的账号，则发送成功，返回true，如果没有空闲账号，返回false</returns>
        public static bool SendRightRow(string subject, string body, string[] accepts, params string[] attachmentPaths)
        {
            if (accepts.Length == 0)
            {
                throw new Exception("收件人不能为空！");
            }
            if (accepts.Length > 20)
            {
                throw new Exception("收件人数量不能超过20人！");
            }
            EmailSenderEntity emailSenderEntity;
            int retryCount = 0;
            lock (EmailSenderEntityList)
            {
                do
                {
                    retryCount++;
                    emailSenderEntity = EmailSenderEntityList[(int)++_randomIndex % EmailSenderEntityList.Count];
                } while (emailSenderEntity.LastSenderTime.AddSeconds(emailSenderEntity.Cooldown) > DateTime.Now && retryCount < EmailSenderEntityList.Count);
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
                throw ex;
            }
        }
    }
}