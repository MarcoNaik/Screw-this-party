using UnityEngine;

namespace LevelProps
{
    public class ExitController : MonoBehaviour
    {
        private LevelLoader levelLoader;

        private void Awake()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                levelLoader.LoadNextLevel();
            }
        }
    
    }
}
