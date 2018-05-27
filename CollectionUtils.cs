using System;
using System.Collections.Generic;
using System.Linq;

namespace iMotions.Collections
{
    public class CollectionUtils
    {
        /// <summary>
        ///     Gets, or inSerts and gets and item from a list based on the matching function.
        ///     If no item in the list satisfies the matching function, then it will use the
        ///     object supplier function to provide a value which it will Add (insert) into
        ///     the list and return that object.
        /// </summary>
        /// <param name="list">The list to search</param>
        /// <param name="matchFunc">The matching function to determine if an element already exists in the list</param>
        /// <param name="objectSupplier">The supplier of a new object function to insert into the list and return, if no
        /// object exists that matches the matchFunc</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The existing item matching the matchFunc Func, or if no such item exists,
        /// the object supplied by the objectSupplier Func</returns>
        public static T GetSertListEntry<T>(List<T> list, Predicate<T> matchFunc, Func<T> objectSupplier)
        {
            int indexOfExisting = list.FindIndex(matchFunc);
            if (indexOfExisting > -1)
            {
                return list[indexOfExisting];
            }
            
            T obj = objectSupplier();
            list.Add(obj);

            return obj;
        }

        /// <summary>
        ///     Find the first object in list that matches the matching function, and apply the mapping function on
        ///     that object.
        ///     If the list is null, or empty, or no elements return true for the matching function, then return the
        ///     object supplied by the default object supplier function.
        /// </summary>
        /// <param name="list">The list of objects on which to apply the match first and map functions</param>
        /// <param name="matchFunc">The matching function to find first matching object in list.</param>
        /// <param name="functionToApplyToFoundObject">Mapping function to apply</param>
        /// <param name="defaultObjectSupplier">Supplier function to provide a default value</param>
        /// <typeparam name="T">Type of objects in the list</typeparam>
        /// <typeparam name="S">Type of the result of the mapping function</typeparam>
        /// <returns>The mapped object, or the object supplied by the default object supplier if no elements in list
        /// or no objects in the provided list that matches the matching function</returns>        
        public static S MapFirstOrDefault<T, S>(
            List<T> list, 
            Predicate<T> matchFunc, 
            Func<T, S> functionToApplyToFoundObject,
            Func<S> defaultObjectSupplier)
        {
            if (list == null) { return defaultObjectSupplier(); }
            
            int indexOfFirstMatch = list.FindIndex(matchFunc);
            if (indexOfFirstMatch > -1)
            {
                return functionToApplyToFoundObject(list[indexOfFirstMatch]);
            }

            return defaultObjectSupplier();
        }

        /// <summary>
        ///     Map the objects of the incoming list with the mapping function and return the resulting list.
        ///     If the list is null, or empty, then return the default list suppliers function result.
        /// </summary>
        /// <param name="list">The list to map with mapping function</param>
        /// <param name="mapFunc">The matching function to apply to every object in the list.</param>
        /// <param name="defaultObjectSupplier">Supplier function to provide a default value</param>
        /// <typeparam name="T">Type of objects in the list</typeparam>
        /// <typeparam name="S">Type of the result of the mapping function</typeparam>
        /// <returns>A new list of where each object of supplied list has been mapped with mapping function, or the
        /// default list suppliers result if incoming list is empty or null</returns> 
        public static List<S> MapOrDefault<T, S>( 
            List<T> list, 
            Func<T, S> mapFunc,
            Func<List<S>> defaultObjectSupplier)
        {
            if (list == null || list.Count.Equals(0)) return defaultObjectSupplier();

            return list.Select(mapFunc).ToList();
        }

        /// <summary>
        ///     Order the objects of a list by the order function
        ///     If the list is null, or empty, then return the default list suppliers function result.
        /// </summary>
        /// <param name="list">The list to order with ordering function</param>
        /// <param name="orderActionSupplier">The ordering function by which to order the list.</param>
        /// <param name="defaultListSupplier">Supplier function to provide a default value</param>
        /// <typeparam name="T">Type of objects in the list</typeparam>
        /// <typeparam name="S">Type of key on which to order the list</typeparam>
        /// <returns>A list with same objects ordered by the ordering function, or the
        /// default list suppliers result if incoming list is empty or null</returns> 
        public static List<T> OrderOrDefault<T, S>(
            List<T> list, 
            Func<T, S> orderActionSupplier,
            Func<List<T>> defaultListSupplier)
        {
            if (list == null || list.Count.Equals(0)) return defaultListSupplier();

            return list.OrderBy(orderActionSupplier).ToList();
        }
        
        /// <summary>
        ///     Gets or inserts and gets an entry in a Dictionary with the key. The value will be a new TValue(), and 
        ///     therefore, must have an empty constructor.
        /// </summary>
        /// <param name="key">The key of the entry to Get or Insert</param>
        /// <param name="dictionary">The Dictionary object on which to Get or Insert</param>
        /// <typeparam name="TKey">The type of the dictionary key value</typeparam>
        /// <typeparam name="TValue">the type of the dictionary value</typeparam>
        /// <returns>The existing entry in the dictionary for the key, or if key does not exist, 
        /// the new TValue object which was added to the dictionary with the provided key.</returns>
        public static TValue GetSertDictionaryEntry<TKey, TValue>(TKey key, Dictionary<TKey, TValue> dictionary) where TValue : new()
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new TValue());
            }

            return dictionary[key];
        }

        /// <summary>
        ///     Gets or inserts and gets an entry in a Dictionary with the key. If new entry the value will be the
        ///     one provided by the no argument lamda.
        /// </summary>
        /// <param name="key">The key of the entry to Get or Insert</param>
        /// <param name="dictionary">The Dictionary object on which to Get or Insert</param>
        /// <param name="objectSupplier">No argument Lamda function that will provide an object to be Added to 
        /// Dictionary if one does not already exist</param>
        /// <typeparam name="TKey">The type of the dictionary key</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value</typeparam>
        /// <returns></returns>
        public static TValue GetSertDictionaryEntry<TKey, TValue>(TKey key, Dictionary<TKey, TValue> dictionary, Func<TValue> objectSupplier)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, objectSupplier());
            }

            return dictionary[key];
        }
    }
}
