using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Penll.Framework
{
    /// <summary>
    ///     Copyright (C) penll.com 版权所有。
    ///     文 件 名：String.Helper.cs
    ///     版	  本：ver 1.00.00
    ///     创建标识：2014-11-28 Created by  penll V1.00.00
    /// </summary>

    /// <summary>
    ///     字符串处理
    /// </summary>
    public static class StringHelper
    {
        public const string SqlKey =
            @"and|or|exec|execute|insert|select|delete|union|update|alter|create|drop|count|\*|chr|char|limit|asc|mid|'%|%'|substring|master|truncate|declare|xp_cmdshell|xp_|sp_|restore|backup|net+user|net+localgroup+administrators|and|or|exec|execute|insert|select|delete|union|update|alter|create|drop|count|\*|chr|char|limit|asc|mid|substring|master|truncate|declare|xp_cmdshell|xp_|sp_|restore|backup|net+user|net+localgroup+administrators";

        static StringHelper()
        {
            //todo 字符串构造函数
        }

        #region Sql防注入处理
        /// <summary>
        /// 判断字符串中是否有SQL攻击代码
        /// </summary>
        /// <param name="value"></param>
        /// <param name="containsSqlStr">SQL攻击代码过滤规则</param>
        /// <returns>true-包含；false-不包含</returns>
        public static bool CheckHasSql(this string value, string containsSqlStr = SqlKey)
        {
            if (value.IsNull())
            {
                return false;
            }

            var regex = new Regex(@"\b(" + containsSqlStr + @")\b", RegexOptions.IgnoreCase);

            return regex.IsMatch(value.ToLower());
        } 
        #endregion
        
        #region Html字符串处理
        /// <summary>
        /// 清除字符串中的html标签 包含script等
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isRemovePunctuation">是否移除换行符\n等</param>
        /// <returns></returns>
        public static string ClearHtmlTags(this string value, bool isRemovePunctuation = false)
        {
            if (value.IsNull())
            {
                return string.Empty;
            }
            value = Regex.Replace(value, @"<script[^>]*?>.*?</script>", " ", RegexOptions.IgnoreCase);//过滤script标签
            value = Regex.Replace(value, "&[^;]*;", " ", RegexOptions.IgnoreCase);//清除字符串中Html特殊符号 如&nbsp;空格
            value = Regex.Replace(value, @"<(.[^>]*)>", " ", RegexOptions.IgnoreCase);//清除字符串中Html标签 如<?>

            if (!isRemovePunctuation) return value;
            
            value = Regex.Replace(value, @"[\t\r\v\f\n]", " ", RegexOptions.IgnoreCase);
            
            return value;
        } 
	    #endregion

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}


