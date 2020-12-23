using UnityEngine;
using UnityEngine.UI;


namespace SharikGame {
    public class GameOverChecker
    {
        private GameObject _overUI;
        private int _currentValue;
        private SliderController _slider;

        public GameOverChecker(GameObject ui, SliderController slider)
        {
            _slider = slider;
            _overUI = ui;
            slider.NewValue += AddValue;
            slider.Load += ChangeToSerializedValue;
        }

        public void GameEnd(bool isEnd, bool isWin)
        {
            Time.timeScale = isEnd ? 0.0f : 1.0f;
            _overUI.SetActive(isEnd);
            var message = isWin ? "Вы выиграли" : "Вы проиграли";
            _overUI.GetComponentInChildren<Text>().text = message;
        }

        public void AddValue()
        {
            _currentValue++;
            Debug.Log(_currentValue);
            if (_currentValue >= _slider.MaxValue)
                GameEnd(true, true);
        }

        public void ChangeToSerializedValue()
        {
            _currentValue = _slider.SerializedValue;
        }
    }
}
