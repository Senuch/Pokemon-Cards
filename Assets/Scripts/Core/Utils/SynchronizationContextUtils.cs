using System.Reflection;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Core.Utils
{
#if UNITY_EDITOR
    /// <summary>
    /// Editor only script for handling tasks on exiting play mode.
    /// </summary>
    public static class SynchronizationContextUtils
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    OnPlayModeExit();
                }
            };
        }

        private static void OnPlayModeExit()
        {
            KillCurrentSynchronizationContext();
        }

        private static void KillCurrentSynchronizationContext()
        {
            var synchronizationContext = SynchronizationContext.Current;

            var constructor = synchronizationContext
                .GetType()
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(int) }, null);

            if (constructor == null)
            {
                return;
            }

            var newContext = constructor.Invoke(new object[] { Thread.CurrentThread.ManagedThreadId });
            SynchronizationContext.SetSynchronizationContext(newContext as SynchronizationContext);
        }
    }
#endif
}