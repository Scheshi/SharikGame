using System.Collections.Generic;
using UnityEngine;
using System;


namespace SharikGame
{
    public class ControllersUpdater : MonoBehaviour
    {
        #region Fields

        private static List<IFixedUpdatable> _fixedUpdates = new List<IFixedUpdatable>();
        private static List<IFrameUpdatable> _frameUpdates = new List<IFrameUpdatable>();
        private static List<ILateUpdatable> _lateUpdates = new List<ILateUpdatable>();

        #endregion


        #region UnityMethods

        public void Update()
        {
            for(int i = 0; i<_frameUpdates.Count; i++)
            {
                _frameUpdates[i].UpdateTick();
            }
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdates.Count; i++)
            {
                _fixedUpdates[i].FixedUpdateTick();
            }
        }

        public void LateUpdate()
        {
            for (int i = 0; i < _lateUpdates.Count; i++)
            {
                _lateUpdates[i].LateUpdateTick();
            }
        }

        #endregion


        #region Methods

        public static void AddUpdate(IUpdatable update)
        {
            if (update is IFixedUpdatable) _fixedUpdates.Add((IFixedUpdatable)update);
            if (update is IFrameUpdatable) _frameUpdates.Add((IFrameUpdatable)update);
            if (update is ILateUpdatable) _lateUpdates.Add((ILateUpdatable)update);
        }

        public static void RemoveUpdate(IUpdatable update)
        {

            if (update is IFixedUpdatable) _fixedUpdates.Remove((IFixedUpdatable)update);
            if (update is IFrameUpdatable) _frameUpdates.Remove((IFrameUpdatable)update);
            if (update is ILateUpdatable) _lateUpdates.Remove((ILateUpdatable)update);
        }

        public static void Dispose()
        {
            _fixedUpdates.Clear();
            _frameUpdates.Clear();
            _lateUpdates.Clear();
        }

        #endregion
    }
}
