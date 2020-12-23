using System;
using UnityEngine.UI;


namespace SharikGame {

    [Serializable]
    public class SliderController : IData
    {
        #region Fields

        public event Action NewValue;
        public event Action Load;


        public int SerializedValue;
        [NonSerialized]private GameOverChecker _checker;
        [NonSerialized]private Slider _slider;
        [NonSerialized]private int _currentValue;

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

            NewValue.Invoke();
        }

        public void FromSave()
        {
            SerializedValue = _currentValue;
        }

        public void FromLoad()
        {
            _currentValue = SerializedValue - 1;
            Load.Invoke();
            ChangeValue();

        }

        #endregion
    }
}
