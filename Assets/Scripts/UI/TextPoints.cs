using UnityEngine;
using UnityEngine.UI;

namespace SharikGame
{
    public class TextPoints : IView
    {
        private Text _text;
        private int _currentPoint = 0;

        public TextPoints()
        {
            _text = GameObject.FindObjectOfType<Text>();
            _text.text = $"{_currentPoint} очков";
        }

        public int GetPoints
        {
            get
            {
                return _currentPoint;
            }
        }

        public void Display(int point)
        {
            _currentPoint += point;
            _text.text = $"{_currentPoint} очков";
        }
    }
}
