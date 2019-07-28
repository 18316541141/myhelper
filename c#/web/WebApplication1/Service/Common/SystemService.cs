using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    /// <summary>
    /// 绝大部分系统都用到的业务逻辑类
    /// </summary>
    public class SystemService
    {
        static List<NewsAlarm> _newsAlarmList;
        static SystemService()
        {
            _newsAlarmList = new List<NewsAlarm>();
            _newsAlarmList.Add(new NewsAlarm
            {
                createDate=DateTime.Now.AddDays(-1),
                menuId= "n11",
                readState=0,
                receive="asdasd",
                title="测试asdasd",
            });
            _newsAlarmList.Add(new NewsAlarm
            {
                createDate = DateTime.Now.AddDays(-2),
                menuId = "m11",
                readState = 0,
                receive = "asddasd",
                title = "测试vvss",
            });
            _newsAlarmList.Add(new NewsAlarm
            {
                createDate = DateTime.Now.AddDays(-2),
                menuId = "m12",
                readState = 0,
                receive = "assdffsdd",
                title = "测e4as试",
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
                currentPageIndex=1,
                startItemIndex=0,
                endItemIndex=2,
                pageDataList=_newsAlarmList,
                pageSize=20,
                totalItemCount=3,
                totalPageCount=1
            };
        }

        /// <summary>
        /// 加载地区树
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<AreaTree>> LoadAreaTree()
        {
            ProvinceRepository provinceRepository = new ProvinceRepository();
            CityRepository cityRepository = new CityRepository();
            DistrictRepository districtRepository = new DistrictRepository();
            TownRepository townRepository = new TownRepository();
            Dictionary<string, List<AreaTree>> areaTreeMap = new Dictionary<string, List<AreaTree>>();
            List<AreaTree> provinces = new List<AreaTree>();
            foreach (Province province in provinceRepository.ReadOnly.FindList())
            {
                provinces.Add(new AreaTree
                {
                    name=province.provinceName,
                    value=Convert.ToString(province.provinceID)
                });
            }
            areaTreeMap.Add("provinces", provinces);
            List<AreaTree> cities = new List<AreaTree>();
            foreach (City city in cityRepository.ReadOnly.FindList())
            {
                cities.Add(new AreaTree
                {
                    name = city.CityName,
                    value = Convert.ToString(city.CityID),
                    parentValue = Convert.ToString(city.ProvinceID)
                });
            }
            areaTreeMap.Add("cities", cities);
            List<AreaTree> counties = new List<AreaTree>();
            foreach (District district in districtRepository.ReadOnly.FindList())
            {
                counties.Add(new AreaTree
                {
                    name = district.DistrictName,
                    value = Convert.ToString(district.DistrictID),
                    parentValue = Convert.ToString(district.CityID)
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
                id = "m312",
                title = "统计图",
                url = "menus/testMenus1/charts.html",
                sortIndex = 3,
            };
            leftMenuList11.Add(leftMenuCharts);
            LeftMenu leftMenuNewAlarm = new LeftMenu
            {
                id="n11",
                title = "实时刷最新消息",
                url = "menus/testMenus1/testNewAlarm.html",
                sortIndex = 4,
            };
            leftMenuList11.Add(leftMenuNewAlarm);
            LeftMenu leftMenu111 = new LeftMenu
            {
                id="m101",
                title= "测试1-1",
                url= "menus/testMenus1/treeForm.html",
                sortIndex = 5,
            };
            leftMenuList11.Add(leftMenu111);

            LeftMenu leftMenu112 = new LeftMenu
            {
                id= "m12",
                title= "测试1-2",
                url= "menus/testMenus1/uploadImage.html",
                sortIndex = 6,
            };
            leftMenuList11.Add(leftMenu112);

            LeftMenu leftMenu13 = new LeftMenu
            {
                id="m13",
                title="测试1-3",
                url = "menus/testMenus1/bigImg.html",
                sortIndex = 7,
            };
            leftMenuList11.Add(leftMenu13);

            LeftMenu leftMenu14 = new LeftMenu
            {
                id = "m14",
                title = "测试1-4",
                url = "menus/testMenus1/uploadFiles.html",
                sortIndex = 8,
            };
            leftMenuList11.Add(leftMenu14);

            LeftMenu leftMenu15 = new LeftMenu
            {
                id = "m15",
                title = "测试1-5",
                url = "menus/testMenus1/areaSelect.html",
                sortIndex = 9,
            };
            leftMenuList11.Add(leftMenu15);

            LeftMenu leftMenu17 = new LeftMenu
            {
                id="m17",
                title = "测试1-7",
                url = "menus/testMenus1/pageTable2.html",
                sortIndex = 10,
            };
            leftMenuList11.Add(leftMenu17);

            LeftMenu leftMenu18 = new LeftMenu
            {
                id = "m18",
                title = "测试1-8",
                url = "menus/testMenus1/uploadExcel.html",
                sortIndex = 11,
            };
            leftMenuList11.Add(leftMenu18);
            LeftMenu leftMenu1 = new LeftMenu
            {
                id = "m1",
                title = "测试1",
                sortIndex = 0,
            };
            leftMenu1.leftMenus=leftMenuList11;
            leftMenuList.Add(leftMenu1);
            LeftMenu leftMenu2 = new LeftMenu
            {
                id="m2",
                title= "测试2",
                sortIndex=1,
            };

            leftMenuList.Add(leftMenu2);
            LeftMenu leftMenu3 = new LeftMenu
            {
                id= "m3",
                title= "测试3",
                sortIndex = 2,
            };

            leftMenuList.Add(leftMenu3);
            return leftMenuList;
        }
    }
}