using System;
using UnityEngine.UI;


namespace SharikGame {
    public class ButtonReloaderView
    {
        private Button _button;
        private ButtonReloaderController _controller;

        public ButtonReloaderView(Button button)
        {
            _button = button;
            _controller = new ButtonReloaderController();
            _button.onClick.AddListener(_controller.Reload);
        }
    }
}
