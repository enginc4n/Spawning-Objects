using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.MainHud
{
  public class MainHudView : EventView
  {
    [Header("Text Labels")]
    [SerializeField]
    private TextMeshProUGUI statusLabel;

    public void SetStatusLabel(string text)
    {
      statusLabel.text = "Status: "+ text;
    }
  }
}