namespace Script.Runtime.Context.Game.Scripts.Model
{
  public interface IGameObjectModel
  {
    public void SetPrefab(string prefabName, string assetKey);
    public string GetPrefabName();
    public string GetAssetKey();
    public void ObjectSpawned();
    public void ObjectCouldNotSpawned();
    public void ObjectDestroyed();
  }
}