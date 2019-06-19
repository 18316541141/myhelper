using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 各种浏览器driver
    /// </summary>
    public enum WebDriverEnum
    {
        //谷歌浏览器
        ChromeDriver,
        //火狐浏览器
        FirefoxDriver,
        //欧朋浏览器
        OperaDriver,
    }

    /// <summary>
    /// 浏览器操作帮助类
    /// </summary>
    public static class SeleniumHelper
    {
        /// <summary>
        /// 去寻找一个存在的元素，如果超时也找不到，则抛出异常。
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">查找条件</param>
        /// <param name="seconds">等待时间秒</param>
        /// <returns></returns>
        public static IWebElement FindElementWhenExists(this IWebDriver webDriver, By by, int seconds)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 60));
            return webDriverWait.Until(ExpectedConditions.ElementExists(by));
        }

        /// <summary>
        /// 尝试去寻找一个存在的元素，如果超时也找不到，则返回null。
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">查找条件</param>
        /// <param name="seconds">等待时间秒</param>
        /// <returns></returns>
        public static IWebElement TryFindElementWhenExists(this IWebDriver webDriver, By by, int seconds)
        {
            try
            {
                return webDriver.FindElementWhenExists(by, seconds);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 去寻找一个可见元素，如果超时也找不到，则抛出异常。
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">查找条件</param>
        /// <param name="seconds">等待时间秒</param>
        /// <returns></returns>
        public static IWebElement FindElementWhenVisible(this IWebDriver webDriver, By by, int seconds)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 60));
            return webDriverWait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        /// <summary>
        /// 尝试去寻找一个可见元素，如果超时也找不到，则返回null。
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">查找条件</param>
        /// <param name="seconds">等待时间秒</param>
        /// <returns></returns>
        public static IWebElement TryFindElementWhenVisible(this IWebDriver webDriver, By by,int seconds)
        {
            try
            {
                return webDriver.FindElementWhenVisible(by, seconds);
            }catch(Exception ex)
            {
                return null;
            }
        }



        /// <summary>
        /// 尝试去寻找元素，如果找不到则不抛出异常，返回null
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">查找条件</param>
        /// <returns>如果找到返回元素，找不到返回null</returns>
        public static IWebElement TryFindElement(this IWebDriver webDriver, By by)
        {
            try
            {
                return webDriver.FindElement(by);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 当元素标签存在时删除，如果超过等待秒数也没有完成删除操作时抛出异常
        /// </summary>
        /// <param name="webDriver">浏览器</param>
        /// <param name="webElement">删除的元素</param>
        /// <param name="seconds">等待秒数</param>
        public static void DelElementWhenExists(this IWebDriver webDriver, By by,int seconds)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webDriver, new TimeSpan(0, 0, seconds));
            webDriver.DelElement(webDriverWait.Until(ExpectedConditions.ElementExists(by)));
        }

        /// <summary>
        /// 点击元素，当元素是可点击时才去点击
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">元素的搜索条件</param>
        /// <param name="seconds">等待时间</param>
        public static void ClickElementWhenClickable(this IWebDriver webDriver, By by, int seconds)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webDriver, new TimeSpan(0, 0, seconds));
            webDriverWait.Until(ExpectedConditions.ElementToBeClickable(by)).Click();
        }

        /// <summary>
        /// 点击元素，当元素的动画播放完毕后点击
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">元素的搜索条件</param>
        /// <param name="millisecond">动画等待时间</param>
        public static void ClickElementAfterAnimate(this IWebDriver webDriver, By by, int millisecond)
        {
            IWebElement webElement = webDriver.TryFindElement(by);
            if (webElement != null)
            {
                Thread.Sleep(millisecond);
                webElement.Click();
            }
        }

        /// <summary>
        /// 删除元素标签
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="webElement">要删除的元素</param>
        public static void DelElement(this IWebDriver webDriver,IWebElement webElement)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            jsExecutor.ExecuteScript("arguments[0].remove();", webElement);
        }

        /// <summary>
        /// 删除元素标签
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">要删除元素的搜索条件</param>
        public static void DelElement(this IWebDriver webDriver, By by) =>
            webDriver.DelElement(webDriver.FindElement(by));

        /// <summary>
        /// 删除单个或多个元素标签
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="bys">要删除元素的搜索条件</param>
        public static void DelElement(this IWebDriver webDriver, params By[] bys) {
            IWebElement[] webElements = new IWebElement[bys.Length];
            for (int i = 0, len = bys.Length; i < len; i++)
                webElements[i] = webDriver.FindElement(bys[i]);
            webDriver.DelElement(webElements);
        }

        /// <summary>
        /// 删除单个或多个元素标签
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="webElement">要删除的元素</param>
        public static void DelElement(this IWebDriver webDriver,params IWebElement[] webElements)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            foreach(IWebElement webElement in webElements)
                jsExecutor.ExecuteScript("arguments[0].remove();", webElement);
        }

        /// <summary>
        /// 删除多个元素标签
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">要删除元素的搜索条件</param>
        public static void DelElements(this IWebDriver webDriver, By by) =>
            webDriver.DelElements(webDriver.FindElements(by));

        /// <summary>
        /// 删除多个元素标签
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="webElement">要删除的元素</param>
        public static void DelElements(this IWebDriver webDriver, IList<IWebElement> webElementList)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            foreach(IWebElement ele in webElementList)
                jsExecutor.ExecuteScript("arguments[0].remove();", ele);
        }

        /// <summary>
        /// 把指定元素追加到另外一个元素的末尾
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="parent">追加后的父元素</param>
        /// <param name="target">追加元素</param>
        public static void AppendElement(this IWebDriver webDriver,IWebElement parent, IWebElement target)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            jsExecutor.ExecuteScript("arguments[0].appendChild(arguments[1]);", parent, target);
        }

        /// <summary>
        /// 把指定元素追加到另外一个元素的末尾
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="by">追加的父元素搜索条件</param>
        /// <param name="tagName">指定元素的标签名称</param>
        /// <param name="innerHtml">指定元素标签的html内容</param>
        public static void AppendElement(this IWebDriver webDriver, By by, string tagName, string innerHtml="")
            =>webDriver.AppendElement(webDriver.FindElement(by), tagName, innerHtml);

        /// <summary>
        /// 把指定元素追加到另外一个元素的末尾
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="parent">追加的父元素</param>
        /// <param name="tagName">指定元素的标签名称</param>
        /// <param name="innerHtml">指定元素标签的html内容</param>
        public static void AppendElement(this IWebDriver webDriver, IWebElement parent,string tagName,string innerHtml)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            jsExecutor.ExecuteScript(
                @"var style=document.createElement(arguments[1]);
                style.innerHTML=arguments[2];
                arguments[0].appendChild(style);", parent, tagName, innerHtml);
        }

        /// <summary>
        /// 设置元素的属性
        /// </summary>
        /// <param name="webDriver">浏览器driver</param>
        /// <param name="target">设置的目标元素</param>
        /// <param name="key">属性的key</param>
        /// <param name="value">属性的值</param>
        public static void SetAttribute(this IWebDriver webDriver, IWebElement target,string key,string value)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1],arguments[2])", target, key, value);
        }
    }
}
