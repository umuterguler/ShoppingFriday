using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.General
{
    public static class WaitExtension
    {
        public static void Wait(this MonoBehaviour mono, float delay, UnityAction action)
        {
            mono.StartCoroutine(ExecuteAction(delay, action));
        }


        private static IEnumerator ExecuteAction(float delay, UnityAction action)
        {
            yield return new WaitForSecondsRealtime(delay);
            action.Invoke();
            yield break;
        }

    }
}
