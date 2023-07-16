using System.Collections.Generic;
using Script.Runtime.Context.Game.Scripts.Enum;
using Script.Runtime.Context.Game.Scripts.Model;
using Scripts.Runtime.Modules.Core.PromiseTool;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace Script.Runtime.Context.Game.Scripts.View.ObjectController
{
  public enum SpawnerEvent
  {
    SpawnClick
  }

  public class ObjectControllerMediator : EventMediator
  {
    [Inject]
    public ObjectControllerView view { get; set; }

    [Inject]
    public IGameObjectModel gameObjectModel { get; set; }

    private GameObject _prefab;

    private List<GameObject> _createdPrefabList = new();

    Vector2 minBounds, maxBounds;

    public override void OnRegister()
    {
      view.dispatcher.AddListener(SpawnerEvent.SpawnClick, OnSpawnClick);

      dispatcher.AddListener(GameObjectEvent.ObjectDestroyed, OnObjectDestroyed);
    }

    private void OnObjectDestroyed()
    {
      foreach (GameObject go in _createdPrefabList)
      {
        Destroy(go);
      }
    }

    private void OnSpawnClick()
    {
      LoadObject()
        .Then(() =>
        {
          SpawnObject().Then(() => { gameObjectModel.ObjectSpawned(); })
            .Catch(exception => { Debug.LogError("Exception" + exception.Message); });
        }).Catch(exception =>
        {
          Debug.LogError("Exception" + exception.Message);
          gameObjectModel.ObjectCouldNotSpawned();
        });
    }

    private IPromise LoadObject()
    {
      Promise promise = new();

      AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(gameObjectModel.GetAssetKey());
      asyncOperationHandle.Completed += handle =>
      {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
          GameObject prefab = handle.Result;
          AdjustPrefabPos(handle.Result);
          promise.Resolve();
        }
        else
        {
          promise.Reject(handle.OperationException);
        }
      };
      return promise;
    }

    private IPromise SpawnObject()
    {
      Promise promise = new();

      var asyncOperationHandle = Addressables.InstantiateAsync(gameObjectModel.GetAssetKey()
        , view.GetParentTransform());
      asyncOperationHandle.Completed += handle =>
      {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
          _prefab = handle.Result;
          _createdPrefabList.Add(_prefab);

          AdjustPrefab(_prefab);
          promise.Resolve();
        }
        else
          promise.Reject(handle.OperationException);
      };
      return promise;
    }

    private void AdjustPrefabPos(GameObject prefab)
    {
      InitBounds();
      prefab.transform.position = new Vector2(Random.Range(minBounds.x + 0.5f, maxBounds.x - 0.5f),
        Random.Range(minBounds.y + 0.5f, maxBounds.y - 0.5f));
    }

    void InitBounds()
    {
      Camera mainCamera = Camera.main;

      minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
      maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void AdjustPrefab(GameObject prefab)
    {
      prefab.name = gameObjectModel.GetPrefabName();

      SpriteRenderer prefabSpriteRenderer = prefab.GetComponent<SpriteRenderer>();
      prefabSpriteRenderer.color = view.GetRandomColorByIndex(GetRandomNumber());
    }

    public int GetRandomNumber()
    {
      return Random.Range(0, view.colorList.Count);
    }


    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(SpawnerEvent.SpawnClick, OnSpawnClick);

      dispatcher.RemoveListener(GameObjectEvent.ObjectDestroyed, OnObjectDestroyed);
    }
  }
}