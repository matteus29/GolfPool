using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Configuration;

namespace Golf.Utilities {
  public static class CacheHelper {
    private static Cache _cache;
    public static Cache Cache {
      get { return _cache ?? HttpContext.Current.Cache; }
      set { _cache = value; }
    }
    /// <summary>
    /// Returns the object from the cache and loads it with the delegate if it's not already in the cache
    /// </summary>
    /// <typeparam name="T">The type of the object to load</typeparam>
    /// <param name="key">The key to look for the object in cache</param>
    /// <param name="dataLoadDelegate">The delegate to use to load the cache-backed object</param>
    /// <param name="dependencies">Any cache dependencies that could invalidate this cached item</param>
    /// <returns>The cache-backed object</returns>
    public static T CacheGet<T>(string key, Func<T> dataLoadDelegate, CacheDependency dependencies = null) {
      return CacheGet(key, dataLoadDelegate, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default);
    }

    /// <summary>
    /// Returns the object from the cache and loads it with the delegate if it's not already in the cache
    /// </summary>
    /// <typeparam name="T">The type of the object to load</typeparam>
    /// <param name="key">The key to look for the object in cache</param>
    /// <param name="dataLoadDelegate">The delegate to use to load the cache-backed object</param>
    /// <param name="dependencies">Any cache dependencies that could invalidate this cached item</param>
    /// <param name="absoluteExpiration">The absoluted time this cache item will expire</param>
    /// <param name="slidingExpiration">The sliding amount of time until this cache item expires</param>
    /// <param name="priority">The priority of this item in the cache when evicting cache items due to memory constraints</param>
    /// <returns>The cache-backed object</returns>
    public static T CacheGet<T>(string key, Func<T> dataLoadDelegate, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority) {
      object cacheObject = Cache[key];
      if (cacheObject == null && dataLoadDelegate != null) {
        cacheObject = dataLoadDelegate();
        Cache.Add(key, cacheObject ?? DBNull.Value, dependencies, absoluteExpiration, slidingExpiration, priority, null);
      }
      else if (cacheObject is DBNull) {
        return default(T);
      }

      return (T)cacheObject;
    }

    /// <summary>
    /// Caches the set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="item">The item.</param>
    /// <param name="dependencies">The dependencies.</param>
    /// <returns></returns>
    public static T CacheSet<T>(string key, T item, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority) {
      Cache.Remove(key);
      Cache.Add(key, item, dependencies, absoluteExpiration, slidingExpiration, priority, null);
      return item;
    }

    /// <summary>
    /// Caches the set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="item">The item.</param>
    /// <param name="dependencies">The dependencies.</param>
    /// <returns></returns>
    public static T CacheSet<T>(string key, T item, CacheDependency dependencies = null) {
      Cache.Remove(key);
      Cache.Add(key, item, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
      return item;
    }
  }
}