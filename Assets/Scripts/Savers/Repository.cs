using System.Collections.Generic;
using UnityEngine;
using System;


namespace SharikGame {
    public class Repository
    {
        #region Fields

        public event Action Saved;
        public event Action Loaded;
        private List<IData> _datas = new List<IData>();
        private readonly string _folderPath = Application.dataPath + "/dataSaves/";
        private ISaver _saver;

        #endregion


        #region Contructors

        public Repository(SerializerEnum serializer)
        {
            switch (serializer)
            {
                case SerializerEnum.XML:
                    _saver = new XMLSerializer();
                    break;
                case SerializerEnum.Binary:
                    _saver = new BinarySerializator();
                    break;
            }
            
        }

        #endregion


        #region Methods

        public void AddDataToList(IData item)
        {
            Debug.Log("Добавление " + item.GetType().Name + " в список");
            _datas.Add(item);
            Saved += item.FromSave;
            Loaded += item.FromLoad;
        }

        public void RemoveDataFromList(Type type)
        {
            Debug.Log(type.Name);
            IData data;
            if (
                (data = _datas.Find(x => x.GetType() == type && x != null)) 
                == null) 
                return;
            _datas.Remove(data);
            Debug.Log("Удаление" + type + " из списка");
        }

        public void RemoveDataFromList(int index)
        {
            _datas.RemoveAt(index);
        }

        public void Save()
        {
            Saved.Invoke();
            for(int i = 0; i < _datas.Count; i++)
            {
                _saver.Save(_datas[i], _folderPath);
            }
        }

        public void Load()
        {
            Debug.Log("Общее кол-во объектов в списке: " + _datas.Count);
            foreach(var data in _datas)
            {
                Debug.Log($"Загружается {data.GetType().Name}\n");

                if (ServiceLocator.IsHas(data.GetType()) && data.GetType() != typeof(PlayerSaveData)) 
                    ServiceLocator.RemoveDependency(data);

                if (data is IUpdatable) ControllersUpdater.RemoveUpdate((IUpdatable)data);
                var item = data;
                _saver.Load(ref item, _folderPath);
            }
            Loaded.Invoke();
        }

        #endregion
    }
}
