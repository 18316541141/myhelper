﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    /// <summary>
    /// 绝大部分系统都用到的业务逻辑类
    /// </summary>
    public class SystemService
    {

        /// <summary>
        /// 加载地区树
        /// </summary>
        /// <returns></returns>
        public List<AreaTree> LoadAreaTree()
        {
            ProvinceRepository provinceRepository = new ProvinceRepository();
            CityRepository cityRepository = new CityRepository();
            DistrictRepository districtRepository = new DistrictRepository();
            TownRepository townRepository = new TownRepository();
            List<AreaTree> areaTreeList = new List<AreaTree>();
            foreach (Province province in provinceRepository.FindList())
            {
                areaTreeList.Add(new AreaTree
                {
                    name=province.ProvinceName,
                    value=province.ProvinceID,
                    type=0
                });
            }
            foreach (City city in cityRepository.FindList())
            {
                areaTreeList.Add(new AreaTree
                {
                    name = city.CityName,
                    value = city.CityID,
                    parentValue = city.ProvinceID,
                    type = 1
                });
            }
            foreach (District district in districtRepository.FindList())
            {
                areaTreeList.Add(new AreaTree
                {
                    name = district.DistrictName,
                    value = district.DistrictID,
                    parentValue = district.CityID,
                    type = 2
                });
            }
            //foreach (Town town in townRepository.FindList())
            //{
            //    areaTreeList.Add(new AreaTree
            //    {
            //        name = town.TownName,
            //        value = town.TownID,
            //        parentValue = town.DistrictID,
            //        type = 3
            //    });
            //}
            return areaTreeList;
        }

        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        /// <returns></returns>
        public List<LeftMenu> LoadLeftMenus()
        {
            List<LeftMenu> leftMenuList = new List<LeftMenu>();
            LeftMenu leftMenu1 = new LeftMenu
            {
                id = "m1",
                title = "测试1"
            };
            List<LeftMenu> leftMenuList11 = new List<LeftMenu>();
            LeftMenu leftMenu111 = new LeftMenu
            {
                id="m11",
                title= "测试1-1",
                url= "menus/testMenus1/treeForm.html"
            };
            leftMenuList11.Add(leftMenu111);

            LeftMenu leftMenu112 = new LeftMenu
            {
                id= "m12",
                title= "测试1-2",
                url= "menus/testMenus1/uploadImage.html"
            };
            leftMenuList11.Add(leftMenu112);

            LeftMenu leftMenu13 = new LeftMenu
            {
                id="m13",
                title="测试1-3",
                url = "menus/testMenus1/bigImg.html"
            };
            leftMenuList11.Add(leftMenu13);

            LeftMenu leftMenu14 = new LeftMenu
            {
                id = "m14",
                title = "测试1-4",
                url = "menus/testMenus1/uploadFiles.html"
            };
            leftMenuList11.Add(leftMenu14);

            LeftMenu leftMenu15 = new LeftMenu
            {
                id = "m15",
                title = "测试1-5",
                url = "menus/testMenus1/areaSelect.html"
            };
            leftMenuList11.Add(leftMenu15);

            leftMenu1.leftMenus=leftMenuList11;
            leftMenuList.Add(leftMenu1);
            LeftMenu leftMenu2 = new LeftMenu
            {
                id="m2",
                title= "测试2"
            };

            leftMenuList.Add(leftMenu2);
            LeftMenu leftMenu3 = new LeftMenu
            {
                id= "m3",
                title= "测试3"
            };

            leftMenuList.Add(leftMenu3);
            return leftMenuList;
        }
    }
}