using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.ObjectController
{
  public class ObjectControllerView : EventView
  {
    [SerializeField]
    private Transform parentTransform;

    public List<Color> colorList;

    public Color GetRandomColorByIndex(int index)
    {
      return colorList[index];
    }

    public Transform GetParentTransform()
    {
      return parentTransform;
    }

    public void OnSpawnClick()
    {
      dispatcher.Dispatch(SpawnerEvent.SpawnClick);
    }
  }
}