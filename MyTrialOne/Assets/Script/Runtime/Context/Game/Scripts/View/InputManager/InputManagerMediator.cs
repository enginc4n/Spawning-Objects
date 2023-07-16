using Script.Runtime.Context.Game.Scripts.Enum;
using Script.Runtime.Context.Game.Scripts.Model;
using strange.extensions.mediation.impl;

namespace Script.Runtime.Context.Game.Scripts.View.InputManager
{
  public enum InputManagerEvent
  {
    ObjectReadyClick
  }

  public class InputManagerMediator : EventMediator
  {
    [Inject]
    public InputManagerView view { get; set; }

    [Inject]
    public IGameObjectModel gameObjectsModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(InputManagerEvent.ObjectReadyClick, OnSetInput);

      dispatcher.AddListener(GameObjectEvent.ObjectReady, OnReady);
      dispatcher.AddListener(GameObjectEvent.ObjectCouldNotSpawned, OnReady);
    }

    private void OnSetInput()
    {
      gameObjectsModel.SetPrefab(view.GetPrefabName(), view.GetAssetKey());
    }

    private void OnReady()
    {
      view.ToggleInputFields();
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(InputManagerEvent.ObjectReadyClick, OnSetInput);

      dispatcher.RemoveListener(GameObjectEvent.ObjectReady, OnReady);
      dispatcher.RemoveListener(GameObjectEvent.ObjectCouldNotSpawned, OnReady);
    }
  }
}