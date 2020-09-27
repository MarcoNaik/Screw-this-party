using UnityEngine;

namespace LevelProps
{
    public abstract class AbstractConsumable : MonoBehaviour
    {
        private float frequency = 3.5f;
        private float magnitude = 0.03f;
        private float index = 0;
        private Vector3 pos;
        public Transform sprite;


        private void Awake()
        {
            pos = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Box"))
            {
                gameObject.SetActive(false);
                Debug.Log("boxhitting consumable");
            }
            if (other.gameObject.CompareTag("Player"))
            {
                Picked();
            }
        }

        private void Picked()
        {
            PickedTrigger();
            gameObject.SetActive(false);
        }


        private void Update()
        {
            index += Time.deltaTime;
            sprite.position = transform.position + Vector3.up * Mathf.Sin(index * frequency) * magnitude;
        }

        protected abstract void PickedTrigger();
    }
}
