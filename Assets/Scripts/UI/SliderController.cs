using System;
using UnityEngine.UI;


namespace SharikGame {
    public class SliderController
    {

        public event Action ChangeValueEvent;
        private Slider _slider;
        private int _currentValue;
        public SliderController(Slider slider)
        {
            _slider = slider;
            _currentValue = 0;
            _slider.value = _currentValue;
            ChangeValueEvent += ServiceLocator.GetDepencity<GameOverChecker>().AddValue;
        }

        public float MaxValue
        {
            get
            {
                return _slider.maxValue;
            }
        }

        public void ChangeValue()
        {
            _currentValue++;
            _slider.value = _currentValue;

            ChangeValueEvent?.Invoke();
    }
    }
}
