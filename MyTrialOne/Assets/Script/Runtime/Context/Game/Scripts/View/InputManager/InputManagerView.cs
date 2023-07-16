using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Runtime.Context.Game.Scripts.View.InputManager
{
  public class InputManagerView : EventView
  {
    [Header("Input Fields")]
    [SerializeField]
    private TMP_InputField prefabNameInputFieldField;

    [SerializeField]
    private TMP_InputField addressableInputFieldField;

    public string GetPrefabName()
    {
      return prefabNameInputFieldField.text;
    }

    public string GetAssetKey()
    {
      return addressableInputFieldField.text.ToLower();
    }

    public void ToggleInputFields()
    {
      prefabNameInputFieldField.interactable = !prefabNameInputFieldField.interactable;
      addressableInputFieldField.interactable = !addressableInputFieldField.interactable;
    }

    public void OnReadyClick()
    {
      dispatcher.Dispatch(InputManagerEvent.ObjectReadyClick);
    }
  }
}