using CommonHelper.Helper.EFDbContext;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Configuration;
namespace WebApplication1
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public sealed class MyDbContext : BaseDbContext
    {
        public MyDbContext() : base(new SqlConnection(WebConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString), true) { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}