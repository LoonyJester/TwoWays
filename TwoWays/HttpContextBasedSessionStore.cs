using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwoWays
{
    public class HttpContextBasedSessionStore : SessionDataServiceBase
    {
        public override object Get(string key)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
                return null;

            var data = HttpContext.Current.Session[key];
            if (data == null)
                return null;

            if (!(data is KeyValuePair<object, DateTime>))
                return data;

            var item = (KeyValuePair<object, DateTime>)data;
            if (item.Value >= DateTime.Now)
                return item.Key;
            data = null;
            Put(key, null);

            return null;
        }

        public override void Put(string key, object data, int expirationTimeout = 0)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
                return;

            if (data == null)
            {
                if (HttpContext.Current.Session[key] != null)
                    HttpContext.Current.Session.Remove(key);
                return;
            }

            HttpContext.Current.Session[key] =
                expirationTimeout == 0
                    ? data
                    : new KeyValuePair<object, DateTime>(data, DateTime.Now.AddSeconds(expirationTimeout));
        }

        public override void Clear(string[] sessionKeyPrefixes)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
                return;
            if (sessionKeyPrefixes == null)
            {
                HttpContext.Current.Session.Clear();
            }
            else
            {
                var removingKeys = new List<string>();
                foreach (var s in sessionKeyPrefixes.Where(s => !string.IsNullOrEmpty(s)))
                {
                    removingKeys.AddRange(from object key in HttpContext.Current.Session.Keys
                                          where key.ToString().StartsWith(s)
                                          select key.ToString());
                }
                foreach (var key in removingKeys)
                    HttpContext.Current.Session[key] = null;
            }
        }

        public override void Abandon()
        {
            Clear(null);
            HttpContext.Current.Session.Abandon();
        }
    }
}
