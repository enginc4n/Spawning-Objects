using Script.Runtime.Context.Game.Scripts.Model;
using Script.Runtime.Context.Game.Scripts.View.ButtonManager;
using Script.Runtime.Context.Game.Scripts.View.CameraAnimation;
using Script.Runtime.Context.Game.Scripts.View.Destroyer;
using Script.Runtime.Context.Game.Scripts.View.InputManager;
using Script.Runtime.Context.Game.Scripts.View.MainHud;
using Script.Runtime.Context.Game.Scripts.View.ObjectController;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Config
{
  public class GameContext : MVCSContext
  {
    public GameContext(MonoBehaviour view) : base(view)
    {
    }

    public GameContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
      base.mapBindings();

      injectionBinder.Bind<IGameObjectModel>().To<GameObjectModel>().ToSingleton();

      mediationBinder.Bind<InputManagerView>().To<InputManagerMediator>();
      mediationBinder.Bind<ButtonManagerView>().To<ButtonManagerMediator>();
      mediationBinder.Bind<MainHudView>().To<MainHudMediator>();
      mediationBinder.Bind<ObjectControllerView>().To<ObjectControllerMediator>();
      mediationBinder.Bind<DestroyerView>().To<DestroyerMediator>();
      mediationBinder.Bind<CameraAnimView>().To<CameraAnimMediator>();
    }
  }
}