using Script.Runtime.Context.Game.Scripts.Enum;
using Script.Runtime.Context.Game.Scripts.Model;
using strange.extensions.mediation.impl;

namespace Script.Runtime.Context.Game.Scripts.View.MainHud
{
  public class MainHudMediator : EventMediator
  {
    [Inject]
    public MainHudView view { get; set; }

    [Inject]
    public IGameObjectModel gameObjectModel { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(GameObjectEvent.ObjectReady, OnReady);
      dispatcher.AddListener(GameObjectEvent.ObjectNotReady, OnNotReady);
      dispatcher.AddListener(GameObjectEvent.ObjectSpawned, OnSpawned);
      dispatcher.AddListener(GameObjectEvent.ObjectCouldNotSpawned, ObjectCouldNotSpawned);
    }

    private void ObjectCouldNotSpawned()
    {
      view.SetStatusLabel("Check your input fields!");
    }

    private void OnSpawned()
    {
      view.SetStatusLabel("Object is spawned! Total Count: " + gameObjectModel.GetCount());
    }

    private void OnNotReady()
    {
      view.SetStatusLabel("Object is not ready!");
    }

    private void OnReady()
    {
      view.SetStatusLabel("Object is ready to go!");
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameObjectEvent.ObjectReady, OnReady);
      dispatcher.RemoveListener(GameObjectEvent.ObjectNotReady, OnNotReady);
      dispatcher.RemoveListener(GameObjectEvent.ObjectSpawned, OnSpawned);
      dispatcher.RemoveListener(GameObjectEvent.ObjectCouldNotSpawned, ObjectCouldNotSpawned);
    }
  }
}
