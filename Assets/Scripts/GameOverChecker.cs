using UnityEngine;
using UnityEngine.UI;


namespace SharikGame {
    public class GameOverChecker
    {
        private GameObject _overUI;
        private int _currentValue;

        public GameOverChecker(GameObject ui)
        {
            _overUI = ui;
        }

        public void GameEnd(bool isEnd, bool isWin)
        {
            Time.timeScale = isEnd ? 0.0f : 1.0f;
            _overUI.SetActive(isEnd);
            var message = isWin ? "Вы выйграли" : "Вы проиграли";
            _overUI.GetComponentInChildren<Text>().text = message;
        }

        public void AddValue()
        {
            _currentValue++;
            Debug.Log(_currentValue);
            if (_currentValue >= ServiceLocator.GetDepencity<SliderController>().MaxValue)
                GameEnd(true, true);
    }
    }
}
