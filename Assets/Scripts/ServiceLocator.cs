using System;
using System.Collections.Generic;


namespace SharikGame
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();

        public static void SetDependency(object obj)
        {
            var type = obj.GetType();
            if (_dictionary.ContainsKey(type))
                throw new ArgumentException("В СервисЛокаторе уже есть компонент с таким типом.");
            _dictionary.Add(type, obj);
        }

        public static bool IsHas<T>()
        {
            return _dictionary.ContainsKey(typeof(T));
        }

        public static bool IsHas(Type type)
        {
            return _dictionary.ContainsKey(type);
        }

        public static T GetDependency<T>()
        {
            var type = typeof(T);
            if (!_dictionary.ContainsKey(type))
                throw new ArgumentException("В СервисЛокаторе нет компонента с таким типом");
            return (T)_dictionary[type];
        }

        public static void RemoveDependency<T>()
        {
            var type = typeof(T);
            if (!_dictionary.ContainsKey(type)) return;
            _dictionary.Remove(type);
        }

        public static void RemoveDependency(object obj)
        {
            if (!_dictionary.ContainsValue(obj)) return;
            _dictionary.Remove(obj.GetType());
        }

        public static void Dispose()
        {
            _dictionary.Clear();
        }
    }
}
