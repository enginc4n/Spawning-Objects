using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.Destroyer
{
  public class DestroyerView : EventView
  {
    public void DestroyObject()
    {
      Destroy(gameObject);
    }

    public void OnDestroyClick()
    {
      dispatcher.Dispatch(DestroyerEvent.Destroy);
    }
  }
}