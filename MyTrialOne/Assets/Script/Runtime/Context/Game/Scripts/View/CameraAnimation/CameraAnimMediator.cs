using System.Collections;
using Script.Runtime.Context.Game.Scripts.Enum;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.CameraAnimation
{
  public class CameraAnimMediator : EventMediator
  {
    [Inject]
    public CameraAnimView view { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(GameObjectEvent.ObjectDestroyed, OnObjectDestroyed);
    }

    private void OnObjectDestroyed()
    {
      StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
      Vector3 initialPosition = transform.position;
      float time = 0;
      while (time < view.shakeDuartion)
      {
        transform.position = (Vector3)Random.insideUnitCircle * view.shakeMagnitude;
        time += Time.deltaTime;
        yield return new WaitForEndOfFrame();
      }

      transform.position = initialPosition;
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameObjectEvent.ObjectDestroyed, OnObjectDestroyed);
    }
  }
}