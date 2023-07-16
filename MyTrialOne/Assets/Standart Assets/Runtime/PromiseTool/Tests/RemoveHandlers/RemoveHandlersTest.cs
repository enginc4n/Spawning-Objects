#if UNITY_EDITOR

using UnityEngine;

namespace Scripts.Runtime.Modules.Core.PromiseTool.Tests.RemoveHandlers
{
  public class RemoveHandlersTest : MonoBehaviour
  {
    public RemoveHandlersServerTest dummyServer;

    private void Awake()
    {
      Promise.EnablePromiseTracking = true;
    }

    public void Start()
    {
      IPromise request = dummyServer.Request();
      Debug.Log("request: " + request.Id);
      IPromise promise = request.Then(OnResponse);
      Debug.Log("promise.RemoveHandlers()");
      Debug.Log("promise.GetHashCode() 2 " + promise.Id);

      Debug.Log(Promise.PendingPromises);
      request.Cancel();
      Object.DestroyImmediate(gameObject);
    }

    private void OnResponse()
    {
      Debug.Log("OnResponse: " + gameObject.name);
    }
  }
}
#endif