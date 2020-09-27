using System.Collections.Generic;
using UnityEngine;

namespace LevelProps
{
    public class ButtonController: MonoBehaviour
    {
        public List<DoorController> doorsToOpen;

        private void Start()
        {
            foreach (DoorController doorController in doorsToOpen)
            {
                doorController.SetUp(this);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Box"))
            {
                other.gameObject.GetComponent<Animator>().SetBool("Sit", true);
                foreach (DoorController doorController in doorsToOpen)
                {
                    doorController.Unlock(this);
                    Debug.Log("unlocked a button");
                }
            }
        
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Box"))
            {
                other.gameObject.GetComponent<Animator>().SetBool("Sit", false);
                foreach (DoorController doorController in doorsToOpen)
                {
                    doorController.Lock(this);
                }
            }
        }
    }
}
