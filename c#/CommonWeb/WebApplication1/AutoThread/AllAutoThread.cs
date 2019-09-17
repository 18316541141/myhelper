using CommonHelper.EFMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WebApplication1.Repository;

namespace WebApplication1.AutoThread
{
    /// <summary>
    /// 所有的定时线程
    /// </summary>
    public class AllAutoThread
    {
        HeartbeatEntityRepository heartbeatEntityRepository { set; get; }

        public AllAutoThread(HeartbeatEntityRepository heartbeatEntityRepository)
        {
            this.heartbeatEntityRepository = heartbeatEntityRepository;
        }

        public void Start()
        {
            Heartbeat();
        }

        /// <summary>
        /// 心跳监测定时线程。
        /// </summary>
        private void Heartbeat()
        {
            //每隔10分钟检查心跳监测表，如果发现有超过10分钟没有心跳的，则马上报警
            new Thread(() => {
                while (true)
                {
                    DateTime temp = DateTime.Now.AddMinutes(-10);
                    try
                    {
                        foreach (HeartbeatEntity heartbeatEntity in heartbeatEntityRepository.FindList(a => a.LastHeartbeatTime < temp))
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    Thread.Sleep(600000);
                }
            }).Start();
        }
    }
}