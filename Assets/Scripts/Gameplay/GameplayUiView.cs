﻿using System.Collections.Generic;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class GameplayUiView : BaseView
    {
        [SerializeField] private ImpressionWidget _impressionWidget = null;
        [SerializeField] private TimerWidget _timerWidget = null;
        [SerializeField] private MusicianWidget _musicianWidget = null;
        [SerializeField] private RectTransform _bubbleContainer = null;
        [SerializeField] private GameObject _bubblePrefab = null;
        [SerializeField] private GameObject _activeStateContainer = null;
        [SerializeField] private GameObject _gameInfo = null;

        private readonly List<BubbleWidget> _bubbles = new List<BubbleWidget>();

        public void Initialize(ImpressionModel impressionModel, Timer timer, List<MusicianModel> musicians)
        {
            _impressionWidget.Initialize(impressionModel);
            _timerWidget.Initialize(timer);
            _musicianWidget.Initialize(musicians);
        }

        public override void Tick(float deltaTime)
        {
            _bubbles.ForEach(bubble => bubble.Tick(deltaTime));
        }

        public BubbleWidget CreateBubble(string text, Transform pivotTransform)
        {
            GameObject bubble = Instantiate(_bubblePrefab, _bubbleContainer);
            var bubbleWidget = bubble.GetComponent<BubbleWidget>();
            bubbleWidget.Initialize(text, pivotTransform);
            _bubbles.Add(bubbleWidget);
            return bubbleWidget;
        }

        public void HideBubble(BubbleWidget bubbleWidget)
        {
            bubbleWidget.gameObject.SetActive(false);
            _bubbles.Remove(bubbleWidget);
        }

        
        public void ShowActiveState()
        {
            _activeStateContainer.SetActive(true);
        }

        public void HideActiveState()
        {
            _activeStateContainer.SetActive(false);
        }

        public void ShowGameInfo()
        {
            _gameInfo.SetActive(true);
        }

        public void HideGameInfo()
        {
            _gameInfo.SetActive(false);
        }

        public override void Dispose()
        {
            foreach (BubbleWidget bubbleWidget in _bubbles)
            {
                bubbleWidget.Dispose();
            }

            _bubbles.Clear();
        }
    }
}