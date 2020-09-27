using MovementAndChars;
using UnityEngine;

namespace Dialogues
{
	public class DialogueTrigger : MonoBehaviour {

		public Dialogue dialogue;


		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				TriggerDialogue();
				other.GetComponent<PlayerController>().Move(Vector3.zero);
				gameObject.SetActive(false);
			}
		
		}

		public void TriggerDialogue ()
		{
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
		}

	}
}
