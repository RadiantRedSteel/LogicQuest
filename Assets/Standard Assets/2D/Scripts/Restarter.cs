using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        public UnityEvent onPlayerEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                onPlayerEnter?.Invoke();
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            }
        }
    }
}
