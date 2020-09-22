using DynamicData.Data;
using System;
using System.Reflection;

namespace DynamicData.Models
{
    public class Common
    {
        private readonly DatabaseContext _context;

        public Common(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public static string GetGuidFromURL(string path)
        {
            string[] aPath = path.Split(new char[] { '/' }, StringSplitOptions.None);
            return aPath[aPath.Length - 1];
        }

        public static string[] getUpdateKey(string requestKey)
        {
            string[] keys = null;
            try
            {
                requestKey = requestKey.Replace("data[", "").TrimEnd(']');
                keys = requestKey.Split(new string[] { "][" }, StringSplitOptions.RemoveEmptyEntries);

            }
            catch (Exception ex)
            {

            }
            return keys;
        }
        public static void SetProperty(Object dynamicObject, string propertyName, object value)
        {
            Type type = dynamicObject.GetType();
            object result;
            result = type.InvokeMember(
                propertyName,
                BindingFlags.SetProperty |
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance,
                null,
                dynamicObject,
                new object[] { value });
        }

    }
}
