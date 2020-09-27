using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues
{
	public class DialogueManager : MonoBehaviour {

		public Text nameText;
		public Text dialogueText;

		public Animator animator;

		private Queue<string> sentences;

		public bool dialogueState;

		// Use this for initialization
		void Start ()
		{
			dialogueState = false;
			sentences = new Queue<string>();
		}

		public void StartDialogue (Dialogue dialogue)
		{
			dialogueState = true;
			animator.SetBool("IsOpen", true);

			nameText.text = dialogue.name;

			sentences.Clear();

			foreach (string sentence in dialogue.sentences)
			{
				sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
		}

		public void DisplayNextSentence ()
		{
			if (sentences.Count == 0)
			{
				EndDialogue();
				return;
			}

			string sentence = sentences.Dequeue();
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
		}

		IEnumerator TypeSentence (string sentence)
		{
			dialogueText.text = "";
			foreach (char letter in sentence.ToCharArray())
			{
				dialogueText.text += letter;
				yield return null;
			}
		}

		void EndDialogue()
		{
			dialogueState = false;
			animator.SetBool("IsOpen", false);
		}

	}
}
