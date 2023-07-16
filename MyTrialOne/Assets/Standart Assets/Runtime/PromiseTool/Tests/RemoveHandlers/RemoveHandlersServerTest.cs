#if UNITY_EDITOR
using System.Collections;
using UnityEngine;

namespace Scripts.Runtime.Modules.Core.PromiseTool.Tests.RemoveHandlers
{
  public class RemoveHandlersServerTest : MonoBehaviour
  {
    public IPromise Request()
    {
      Debug.Log("RemoveHandlersServerTest> Request");
      Promise request = new();

      Debug.Log("request.GetHashCode 0 " + request.Id);
      Debug.Log("request.GetHashCode 1 " + request.Id);
      StartCoroutine(WaitResponse(request));

      return request;
    }

    private IEnumerator WaitResponse(Promise request)
    {
      yield return new WaitForSeconds(1);
      Debug.Log("RemoveHandlersServerTest> OnReceived");
      Debug.Log("request.GetHashCode 3 " + request.Id);

      request.Resolve();

    }
  }
}
#endif