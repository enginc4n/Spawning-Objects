using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.Context.Game.Scripts.View.ButtonManager
{
  public class ButtonManagerView : EventView
  {
    [Header("Buttons")]
    [SerializeField]
    private Button readyButton;

    [SerializeField]
    private Button spawnButton;

    [SerializeField]
    private Button resetButton;

    public void ToggleAllButtons()
    {
      ToggleButton(readyButton);
      ToggleButton(spawnButton);
      ToggleButton(resetButton);
    }

    private void ToggleButton(Button button)
    {
      button.interactable = !button.interactable;
    }

    public void OnResetClick()
    {
      dispatcher.Dispatch(ButtonManagerEvent.Reset);
    }
  }
}