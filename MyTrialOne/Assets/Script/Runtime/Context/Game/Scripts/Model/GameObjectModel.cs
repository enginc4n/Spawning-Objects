using System.Collections.Generic;
using Script.Runtime.Context.Game.Scripts.Enum;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Model
{
  public class GameObjectModel : IGameObjectModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private string _prefabName;
    private string _assetKey;
    private int _count;

    private List<GameObject> _createdPrefabList;

    [PostConstruct]
    public void OnPostConstruct()
    {
      _createdPrefabList = new();
    }

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
      _count++;
      dispatcher.Dispatch(GameObjectEvent.ObjectSpawned);
    }

    public void ObjectCouldNotSpawned()
    {
      dispatcher.Dispatch(GameObjectEvent.ObjectCouldNotSpawned);
    }

    public void ObjectDestroyed()
    {
      DestroyAllGameObjects();
      dispatcher.Dispatch(GameObjectEvent.ObjectDestroyed);
    }

    public int GetCount()
    {
      return _count;
    }

    public void AddGameObjectToTheList(GameObject prefab)
    {
      _createdPrefabList.Add(prefab);
    }

    void DestroyAllGameObjects()
    {
      foreach (var prefab in _createdPrefabList)
      {
        Object.Destroy(prefab);
      }

      _createdPrefabList.Clear();
    }
  }
}
