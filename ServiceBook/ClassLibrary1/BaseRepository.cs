using ServiceBook.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public abstract class BaseRepository<T> : IDisposable, IRepository<T> where T : class
    {
        ServiceBookContext dbContext = null;

        protected ServiceBookContext DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = new ServiceBookContext();
                }

                return dbContext;
            }
        }

        #region IRepository<T> Members

        /// <summary>
        /// 	Gets the enumerator of an object by type
        /// </summary>
        /// <param name = "type">The object type</param>
        /// <returns>The object enumerator</returns>
        public IEnumerator Get(Type type)
        {
            return DbContext.Database.SqlQuery(type, "", null).GetEnumerator();
        }

        public bool Lazy
        {
            get { return DbContext.Configuration.LazyLoadingEnabled; }
            set { DbContext.Configuration.LazyLoadingEnabled = value; }
        }

        public int GetItemsCount()
        {
            return DbContext.Set<T>().Count();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Delete(T item)
        {
            DbContext.Set<T>().Remove(item);
        }
        #endregion

        #region Sorting/Filtering

        /// <summary>
        /// 	Filters a collection after the given filters
        /// </summary>
        /// <typeparam name = "T">The type of the elements in the collection</typeparam>
        /// <param name = "filtersKeyValuePairs">a collection of filterColumn - filterValue pair</param>
        /// <param name = "collection">The collection to be filtered</param>
        /// <returns>The filtered collection</returns>
        internal static IQueryable<T> Filter(List<KeyValuePair<string, string>> filtersKeyValuePairs,
                                             IQueryable<T> collection)
        {
            //filters were applied
            if (filtersKeyValuePairs.Count > 0)
            {
                foreach (var filter in filtersKeyValuePairs)
                {
                    collection = collection.Where(DAL.Filter.From<T>(filter.Key, filter.Value));
                }
            }

            return collection;
        }

        internal static IEnumerable<T> Filter(List<KeyValuePair<string, string>> filtersKeyValuePairs,
                                              IEnumerable<T> collection)
        {
            //filters were applied
            if (filtersKeyValuePairs.Count > 0)
            {
                foreach (var filter in filtersKeyValuePairs)
                {
                    collection = collection.Where(DAL.Filter.From<T>(filter.Key, filter.Value).Compile());
                }
            }

            return collection;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            dbContext = null;
        }

        #endregion
    }
}
