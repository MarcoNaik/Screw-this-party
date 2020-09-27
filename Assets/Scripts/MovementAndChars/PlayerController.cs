using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;


namespace MovementAndChars
{
    public class PlayerController : CanMove
    {
        public int rewinds;
    

        private List<BoxController> boxes;

        private Stack<LastMoveData> lastBoxMovePile;
        private LevelLoader levelLoader;
        private Boolean hasToDie;
        public Vector3 currentPos;
        private Vector3 nextMoveDirection;
        private Vector3 currentMoveDirection;
        private AudioManager audioManager;

        private RewindGUIController rewindGui;
        private Animator animator;

        private PlayerInputController inputsController;

        public void SetInputActive()
        {
            inputsController.enabled = true;
            
        }

        private void Awake()
        {
            inputsController = GetComponent<PlayerInputController>();
            audioManager = FindObjectOfType<AudioManager>();
            rewindGui = FindObjectOfType<RewindGUIController>();
            animator = GetComponent<Animator>();
            nextMoveDirection = Vector3.zero;
            currentMoveDirection = Vector3.zero;
            levelLoader = FindObjectOfType<LevelLoader>();
            lastBoxMovePile = new Stack<LastMoveData>();
            GameObject[] boxesGO = GameObject.FindGameObjectsWithTag("Box");
            boxes = new List<BoxController>();
            foreach (GameObject box in boxesGO)
            {
                boxes.Add(box.GetComponent<BoxController>());
            }
        }

    

        private void Start()
        {
            movePoint.parent = null;
            StartCoroutine(MoveCoroutine());
        }

        private void FixedUpdate()
        {
            currentPos = transform.position = Vector3.MoveTowards(transform.position, movePoint.position,
                CurrentMoveSpeed * Time.fixedDeltaTime);
        }
    
        public void RewindCommand()
        {
            animator.SetTrigger("Rewind");
        
        }

        public void RewindAnimTrigger()
        {
            foreach (BoxController box in boxes)
            {
                if (!hasToDie)
                {
                    hasToDie = box.Rewind();
                }
                else
                {
                    box.Rewind();
                }
                Debug.Log(hasToDie);
            
            }
        
            if(hasToDie) StartCoroutine(Die());
        
        
            rewinds--;
            rewindGui.DisplayRewind(rewinds);
            audioManager.Play("Rewind2");
        } 
    
    
        public void AddRewind()
        {

            rewinds++;
        
            rewindGui.DisplayRewind(rewinds);
        }

        IEnumerator MoveCoroutine()
        {
            while (true)
            {
            
                while (Math.Abs(Vector3.SqrMagnitude(transform.position - movePoint.position)) > Double.Epsilon)
                {
                    currentMoveDirection = nextMoveDirection;
                    yield return null;
                }

                if (CanMoveTo(currentMoveDirection))
                {
                    movePoint.position += currentMoveDirection;
                }

                RotateTo(currentMoveDirection);
            
                currentMoveDirection = nextMoveDirection;
            
                yield return null;
            }
        }
        public void Move(Vector3 dir)
        {
            nextMoveDirection = dir;
        }
    

        private bool CanMoveTo(Vector3 direction)
        {
            if (IsAvailableTile(direction))
            {
                if (IsBoxTile(direction))
                {
                    BoxController box = lastCollition.GetComponent<BoxController>();
            
                    List<BoxController> boxList = box.MoveTo(direction);
            
                    if (boxList!=null)
                    {
                        lastBoxMovePile.Push(new LastMoveData(boxList, direction));
                    
                        animator.SetBool("IsPushing",true);
                        return true;
                    }
                    animator.SetBool("IsPushing",false);
                    return false;
                }
                animator.SetBool("IsPushing",false);
                return true;
            }
        
            Debug.LogWarning("colliding with something unexpected");
            return false;
        }


        private IEnumerator Die()
        {
            animator.SetTrigger("Die");
            audioManager.Play("Die1");
        
            yield return new WaitForSeconds(1.5f);
        
            levelLoader.ReloadCurrentLevel();
        }
    

    

        private struct LastMoveData
        {
            public List<BoxController> boxes;
            public Vector3 move;

            public LastMoveData(List<BoxController> boxes, Vector3 move)
            {
                this.boxes = boxes;
                this.move = move;
            }
        }

    }
}