using System;
using UnityEngine.UI;


namespace SharikGame {

    [Serializable]
    public class SliderController : IData
    {
        #region Fields

        public event Action ChangeValueEvent;
        private Slider _slider;
        private int _currentValue;
        
        public int SerializedValue { get
            {
                return _currentValue;
            }
            set
            {
                return;
            }
        }

        #endregion


        #region Constructors

        public SliderController()
        {
            return;
        }

        public SliderController(Slider slider)
        {
            _slider = slider;
            _currentValue = 0;
            _slider.value = _currentValue;
            ChangeValueEvent += ServiceLocator.GetDependency<GameOverChecker>().AddValue;
        }

        #endregion


        #region Properties
        public float MaxValue
        {
            get
            {
                return _slider.maxValue;
            }
        }

        #endregion


        #region Methods

        public void ChangeValue()
        {
            _currentValue++;
            _slider.value = _currentValue;

            ChangeValueEvent?.Invoke();
        }

        public void FromSave()
        {
            SerializedValue = _currentValue;
        }

        public void FromLoad()
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
