using Dialogues;
using UnityEngine;

namespace MovementAndChars
{
    public class PlayerInputController : MonoBehaviour
    {
        public InputMaster controls;
        public PlayerController playerController;
        private LevelLoader levelLoader;
        private Vector2 inputVector;

        private DialogueManager _dialogueManager;
    

        private void Awake()
        {
            _dialogueManager = FindObjectOfType<DialogueManager>();
            levelLoader = FindObjectOfType<LevelLoader>();
            controls = new InputMaster();
            controls.Player.Movement.performed += aux => MovePlayer(aux.ReadValue<Vector2>());
            controls.Player.Rewind.performed += context => Rewind();
            controls.Player.Retry.performed += context => RetryLevel();
            controls.Player.NextDialogue.performed += context => NextDialogue();
            controls.Player.Skip.performed += context => SkipLevel();
        }


        private void SkipLevel()
        {
            levelLoader.LoadNextLevel();
        }
        private bool NextDialogue()
        {
            if (_dialogueManager.dialogueState)
            {
                _dialogueManager.DisplayNextSentence();
                return true;
            }

            return false;

        }
        private void Rewind()
        {
            
            if (!NextDialogue() && playerController.rewinds > 0)
            {
                playerController.RewindCommand();
                enabled = false;
            }
        }

        private void MovePlayer(Vector2 direction2)
        {
            if (_dialogueManager.dialogueState) direction2 = Vector2.zero;
            
            Vector3 direction = new Vector3(direction2.x, direction2.y, 0);

            Debug.Log(direction2);
            
            if (Mathf.Abs(direction.x + direction.y) == 1 || direction2 == Vector2.zero){
            
            
                playerController.Move(direction);
            }
        }

        private void RetryLevel()
        {
            levelLoader.ReloadCurrentLevel();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
    }
}
