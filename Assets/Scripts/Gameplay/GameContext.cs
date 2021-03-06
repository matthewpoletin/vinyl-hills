﻿namespace KnowCrow.AT.KeepItAlive
{
    public class GameContext : LifecycleItem
    {
        private GameState _currentState = null;

        public GameplayController GameplayController { get; private set; }
        public GameplayUiView UiView { get; private set; }
        public EnvironmentView EnvironmentView { get; }
        public GameplayModel Model { get; private set; }

        public Timer Timer { get; private set; }

        public GameContext(GameplayController gameplayController, GameplayUiView uiView, EnvironmentView environmentView, GameplayModel model)
        {
            GameplayController = gameplayController;
            UiView = uiView;
            EnvironmentView = environmentView;
            Model = model;

            Timer = new Timer(gameplayController.GameParams.SessionDurationSec);
            Timer.Pause();
        }

        public void ChangeState(GameState state)
        {
            _currentState?.Dispose();

            _currentState = state;
            _currentState.SetContext(this);
            _currentState.Initialize();
        }

        public void TogglePause()
        {
            _currentState.TogglePauseAction();
        }

        public void GameFinished(GameStateChangeReason reason)
        {
            _currentState.FinishGameAction(reason);
        }

        public override void Tick(float deltaTime)
        {
            _currentState.Tick(deltaTime);
            Timer.Tick(deltaTime);
        }

        public override void Dispose()
        {
            _currentState.Dispose();
        }
    }
}