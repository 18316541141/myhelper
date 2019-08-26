using CommonHelper.Helper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.Repository;
using WebApplication1.Service.Common;

namespace WebApplication1.Service
{
    /// <summary>
    /// 绝大部分系统都用到的业务逻辑类
    /// </summary>
    public class SystemService: BaseService
    {
        public ProvinceRepository ProvinceRepository { set; get; }

        public CityRepository CityRepository { set; get; }

        public DistrictRepository DistrictRepository { set; get; }

        public TownRepository TownRepository { set; get; }

        static List<NewsAlarm> _newsAlarmList;
        static SystemService()
        {
            _newsAlarmList = new List<NewsAlarm>();
            _newsAlarmList.Add(new NewsAlarm
            {
                CreateDate=DateTime.Now.AddDays(-1),
                MenuId= "n11",
                ReadState=0,
                Receive="asdasd",
                Title="测试asdasd",
            });
            _newsAlarmList.Add(new NewsAlarm
            {
                CreateDate = DateTime.Now.AddDays(-2),
                MenuId = "m11",
                ReadState = 0,
                Receive = "asddasd",
                Title = "测试vvss",
            });
            _newsAlarmList.Add(new NewsAlarm
            {
                CreateDate = DateTime.Now.AddDays(-2),
                MenuId = "m12",
                ReadState = 0,
                Receive = "assdffsdd",
                Title = "测e4as试",
            });
        }

        /// <summary>
        /// 追加最新消息
        /// </summary>
        /// <param name="newsAlarm"></param>
        public void AddNewsAlarm(NewsAlarm newsAlarm)
        {
            _newsAlarmList.Add(newsAlarm);
        }

        /// <summary>
        /// 批量追加最新消息
        /// </summary>
        /// <param name="newsAlarmList"></param>
        public void AddNewsAlarm(List<NewsAlarm> newsAlarmList)
        {
            _newsAlarmList.AddRange(newsAlarmList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MyPagedList<NewsAlarm> LoadNewsAlarm()
        {
            return new MyPagedList<NewsAlarm>
            {
                CurrentPageIndex=1,
                StartItemIndex=0,
                EndItemIndex=2,
                PageDataList=_newsAlarmList,
                PageSize=20,
                TotalItemCount=3,
                TotalPageCount=1
            };
        }

        /// <summary>
        /// 加载地区树
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<AreaTree>> LoadAreaTree()
        {
            Dictionary<string, List<AreaTree>> areaTreeMap = new Dictionary<string, List<AreaTree>>();
            List<AreaTree> provinces = new List<AreaTree>();
            foreach (Province province in ProvinceRepository.ReadOnly.FindList())
            {
                provinces.Add(new AreaTree
                {
                    Name=province.provinceName,
                    Value=Convert.ToString(province.provinceID)
                });
            }
            areaTreeMap.Add("provinces", provinces);
            List<AreaTree> cities = new List<AreaTree>();
            foreach (City city in CityRepository.ReadOnly.FindList())
            {
                cities.Add(new AreaTree
                {
                    Name = city.CityName,
                    Value = Convert.ToString(city.CityID),
                    ParentValue = Convert.ToString(city.ProvinceID)
                });
            }
            areaTreeMap.Add("cities", cities);
            List<AreaTree> counties = new List<AreaTree>();
            foreach (District district in DistrictRepository.ReadOnly.FindList())
            {
                counties.Add(new AreaTree
                {
                    Name = district.DistrictName,
                    Value = Convert.ToString(district.DistrictID),
                    ParentValue = Convert.ToString(district.CityID)
                });
            }
            areaTreeMap.Add("counties", counties);
            //foreach (Town town in townRepository.FindList())
            //{
            //    areaTreeList.Add(new AreaTree
            //    {
            //        name = town.TownName,
            //        value = town.TownID,
            //        parentValue = town.DistrictID
            //    });
            //}
            areaTreeMap.Add("towns",new List<AreaTree>());
            return areaTreeMap;
        }

        /// <summary>
        /// 根据key查询签名密钥
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string FindSecretByKey(string key)
        {
            return "sdgsdfsdfsdfsdf";
        }

        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        /// <returns></returns>
        public List<LeftMenu> LoadLeftMenus()
        {
            List<LeftMenu> leftMenuList = new List<LeftMenu>();
            List<LeftMenu> leftMenuList11 = new List<LeftMenu>();
            LeftMenu leftMenuCharts = new LeftMenu
            {
                Id = "m312",
                Title = "统计图",
                Url = "menus/testMenus1/charts.html",
                SortIndex = 3,
            };
            leftMenuList11.Add(leftMenuCharts);
            LeftMenu leftMenuNewAlarm = new LeftMenu
            {
                Id="n11",
                Title = "实时刷最新消息",
                Url = "menus/testMenus1/testNewAlarm.html",
                SortIndex = 4,
            };
            leftMenuList11.Add(leftMenuNewAlarm);
            LeftMenu leftMenu111 = new LeftMenu
            {
                Id="m101",
                Title= "测试1-1",
                Url= "menus/testMenus1/treeForm.html",
                SortIndex = 5,
            };
            leftMenuList11.Add(leftMenu111);

            LeftMenu leftMenu112 = new LeftMenu
            {
                Id= "m12",
                Title= "测试1-2",
                Url= "menus/testMenus1/uploadImage.html",
                SortIndex = 6,
            };
            leftMenuList11.Add(leftMenu112);

            LeftMenu leftMenu13 = new LeftMenu
            {
                Id="m13",
                Title="测试1-3",
                Url = "menus/testMenus1/bigImg.html",
                SortIndex = 7,
            };
            leftMenuList11.Add(leftMenu13);

            LeftMenu leftMenu14 = new LeftMenu
            {
                Id = "m14",
                Title = "测试1-4",
                Url = "menus/testMenus1/uploadFiles.html",
                SortIndex = 8,
            };
            leftMenuList11.Add(leftMenu14);

            LeftMenu leftMenu15 = new LeftMenu
            {
                Id = "m15",
                Title = "测试1-5",
                Url = "menus/testMenus1/areaSelect.html",
                SortIndex = 9,
            };
            leftMenuList11.Add(leftMenu15);

            LeftMenu leftMenu17 = new LeftMenu
            {
                Id="m17",
                Title = "测试1-7",
                Url = "menus/testMenus1/pageTable2.html",
                SortIndex = 10,
            };
            leftMenuList11.Add(leftMenu17);

            LeftMenu leftMenu18 = new LeftMenu
            {
                Id = "m18",
                Title = "测试1-8",
                Url = "menus/testMenus1/uploadExcel.html",
                SortIndex = 11,
            };
            leftMenuList11.Add(leftMenu18);
            LeftMenu leftMenu1 = new LeftMenu
            {
                Id = "m1",
                Title = "测试1",
                SortIndex = 0,
            };
            leftMenu1.LeftMenus=leftMenuList11;
            leftMenuList.Add(leftMenu1);
            LeftMenu leftMenu2 = new LeftMenu
            {
                Id="m2",
                Title= "测试2",
                SortIndex=1,
            };

            leftMenuList.Add(leftMenu2);
            LeftMenu leftMenu3 = new LeftMenu
            {
                Id= "m3",
                Title= "测试3",
                SortIndex = 2,
            };

            leftMenuList.Add(leftMenu3);
            return leftMenuList;
        }
    }
}