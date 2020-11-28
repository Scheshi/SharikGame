using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SharikGame
{
    public class SliderPoints : IView
    {
        private Slider _slider;

        public SliderPoints(int maxPoint)
        {
            _slider = GameObject.FindObjectOfType<Slider>();
            _slider.maxValue = maxPoint;
            _slider.value = 0;
        }


        public void Display(int point)
        {
            _slider.value += point;
        }
    }
}