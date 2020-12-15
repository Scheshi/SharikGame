using System;
using UnityEngine.UI;


namespace SharikGame {

    [Serializable]
    public class SliderController : IData
    {
        #region Fields

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
            _checker = ServiceLocator.GetDependency<GameOverChecker>();
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

            _checker.AddValue();
        }

        public void FromSave()
        {
            SerializedValue = _currentValue;
        }

        public void FromLoad()
        {
            _currentValue = SerializedValue - 1;
            ServiceLocator.GetDependency<GameOverChecker>().ChangeToSerializedValue();
            ChangeValue();

        }

        #endregion
    }
}
