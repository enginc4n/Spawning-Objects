using Script.Runtime.Context.Game.Scripts.Enum;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace Script.Runtime.Context.Game.Scripts.Model
{
  public class GameObjectModel : IGameObjectModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private string _prefabName;
    private string _assetKey;


    public void SetPrefab(string prefabName, string assetKey)
    {
      _prefabName = prefabName;
      _assetKey = assetKey;

      if (_prefabName != "" && _assetKey != "")
        dispatcher.Dispatch(GameObjectEvent.ObjectReady);
      else
        dispatcher.Dispatch(GameObjectEvent.ObjectNotReady);
    }

    public string GetPrefabName()
    {
      return _prefabName;
    }

    public string GetAssetKey()
    {
      return _assetKey;
    }

    public void ObjectSpawned()
    {
      dispatcher.Dispatch(GameObjectEvent.ObjectSpawned);
    }

    public void ObjectCouldNotSpawned()
    {
      dispatcher.Dispatch(GameObjectEvent.ObjectCouldNotSpawned);
    }

    public void ObjectDestroyed()
    {
      dispatcher.Dispatch(GameObjectEvent.ObjectDestroyed);
    }
  }
}