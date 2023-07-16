using strange.extensions.context.impl;

namespace Script.Runtime.Context.Game.Scripts.Config
{
  public class GameBootstrap : ContextView
  {
    private void Awake()
    {
      context = new GameContext(this);
    }
  }
}