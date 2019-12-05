using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CommonWeb.ModelBinder
{
    /// <summary>
    /// 对原有的DefaultModelBinder进行代理，解决提交对象数据时，
    /// 空串属性会转为null的现象
    /// </summary>
    public class MyDefaultModelBinder : DefaultModelBinder
    {
        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            if(propertyDescriptor.PropertyType == typeof(string))
            {
                base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value == null ? string.Empty : value);
            }
            else
            {
                base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
            }
        }
    }
}