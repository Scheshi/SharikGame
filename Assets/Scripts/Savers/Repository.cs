using System.Collections.Generic;
using UnityEngine;
using System;


namespace SharikGame {
    public class Repository
    {
        #region Fields

        public event Action Saved;
        public event Action<IData> Loaded;
        private List<IData> _datas = new List<IData>();
        private readonly string _folderPath = Application.dataPath + "/dataSaves/";
        private ISaver _saver;

        #endregion


        #region Contructors

        public Repository()
        {
            _saver = new XMLSerializer();
        }

        #endregion


        #region Methods

        public void AddDataToList(IData item)
        {
            _datas.Add(item);
            Saved += item.FromSave; 
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
            for(int i = 0; i<_datas.Count; i++)
            {
                Debug.Log($"Загружено на {(i / _datas.Count) * 100}%");
                if(ServiceLocator.IsHas(_datas[i].GetType())) 
                    ServiceLocator.RemoveDependency(_datas.Count);

                if (_datas[i] is IUpdatable) ControllersUpdater.RemoveUpdate((IUpdatable)_datas[i]);

                var data = _datas[i];
                _saver.Load(ref data, _folderPath);
                data.FromLoad();
            }
        }

        #endregion
    }
}
