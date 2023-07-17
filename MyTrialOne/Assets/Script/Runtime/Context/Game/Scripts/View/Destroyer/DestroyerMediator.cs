using System;
using Script.Runtime.Context.Game.Scripts.Model;
using Scripts.Runtime.Modules.Core.PromiseTool;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Script.Runtime.Context.Game.Scripts.View.Destroyer
{
  public enum DestroyerEvent
  {
    Destroy
  }

  public class DestroyerMediator : EventMediator
  {
    [Inject]
    public DestroyerView view { get; set; }

    [Inject]
    public IGameObjectModel gameObjectModel { get; set; }

    private ParticleSystem _particleSystem;

    public override void OnRegister()
    {
      view.dispatcher.AddListener(DestroyerEvent.Destroy, OnDestroyer);
    }

    private void OnDestroyer()
    {
      CreateExplosion().Then((() =>
      {
        Destroy(_particleSystem.gameObject, _particleSystem.main.duration);
        gameObjectModel.ObjectDestroyed();
        view.DestroyObject();
      })).Catch(exception =>
      {
        Debug.LogError("Exception" + exception.Message);
      });
    }

    IPromise CreateExplosion()
    {
      Promise promise = new();
      AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.InstantiateAsync("explosion");
      asyncOperationHandle.Completed += handle =>
      {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
          _particleSystem = handle.Result.GetComponent<ParticleSystem>();
          promise.Resolve();
        }
        else
        {
          promise.Reject(handle.OperationException);
        }
      };
      return promise;
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(DestroyerEvent.Destroy, OnDestroyer);
    }
  }
}
