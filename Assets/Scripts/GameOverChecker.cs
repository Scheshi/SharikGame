﻿using UnityEngine;
using System;


namespace SharikGame {
    public static class GameOverChecker
    {
        public static event Action EndGame;
        private static GameObject _ui;

        public static void Initialize(GameObject ui)
        {
            _ui = ui;
        }

        public static void GameOver(bool isActive)
        {
            Time.timeScale = isActive ? 0.0f : 1.0f;
            _ui.SetActive(isActive);
            if(isActive)
            EndGame?.Invoke();
        }
    }
}