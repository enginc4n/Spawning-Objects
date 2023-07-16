using Script.Runtime.Context.Game.Scripts.Enum;
using Script.Runtime.Context.Game.Scripts.Model;
using strange.extensions.mediation.impl;

namespace Script.Runtime.Context.Game.Scripts.View.ButtonManager
{
  public enum ButtonManagerEvent
  {
    Reset
  }

  public class ButtonManagerMediator : EventMediator
  {
    [Inject]
    public ButtonManagerView view { get; set; }

    [Inject]
    public IGameObjectModel gameObjectModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(ButtonManagerEvent.Reset, OnReset);

      dispatcher.AddListener(GameObjectEvent.ObjectReady, OnReady);
      dispatcher.AddListener(GameObjectEvent.ObjectCouldNotSpawned, OnReady);
    }

    private void OnReset()
    {
      gameObjectModel.ObjectCouldNotSpawned();
    }

    private void OnReady()
    {
      view.ToggleAllButtons();
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(ButtonManagerEvent.Reset, OnReset);

      dispatcher.AddListener(GameObjectEvent.ObjectReady, OnReady);
      dispatcher.RemoveListener(GameObjectEvent.ObjectCouldNotSpawned, OnReady);
    }
  }
}