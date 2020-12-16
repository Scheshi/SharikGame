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

        public static T GetDependency<T>()
        {
            var type = typeof(T);
            if (!_dictionary.ContainsKey(type))
                throw new ArgumentException("В СервисЛокаторе нет компонента с таким типом");
            return (T)_dictionary[type];
        }

        public static void Dispose()
        {
            _dictionary.Clear();
        }
    }
}
