using Script.Runtime.Context.Game.Scripts.Enum;
using Script.Runtime.Context.Game.Scripts.Model;
using Scripts.Runtime.Modules.Core.PromiseTool;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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

    public override void OnRegister()
    {
      view.dispatcher.AddListener(SpawnerEvent.SpawnClick, OnSpawnClick);
    }

    private void OnSpawnClick()
    {
      LoadObject()
        .Then(() =>
        {
          SpawnObject().Then(() =>
            {
              gameObjectModel.ObjectSpawned();
            })
            .Catch(exception =>
            {
              Debug.LogError("Exception" + exception.Message);
            });
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
          gameObjectModel.AddGameObjectToTheList(handle.Result);
          AdjustPrefab(handle.Result);
          promise.Resolve();
        }
        else
          promise.Reject(handle.OperationException);
      };
      return promise;
    }

    private void AdjustPrefabPos(GameObject prefab)
    {
      Vector2 minBounds, maxBounds;

      Camera mainCamera = Camera.main;
      minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
      maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

      prefab.transform.position = new Vector2(Random.Range(minBounds.x + 0.5f, maxBounds.x - 0.5f),
        Random.Range(minBounds.y + 0.5f, maxBounds.y - 0.5f));
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
    }
  }
}
