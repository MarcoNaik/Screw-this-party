using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MovementAndChars
{
    public class BoxController : CanMove
    {
        private Stack<Vector3> movePile;

        private Animator animator;
    
        [SerializeField]
        private float rewindMoveSpeed= 1.5f;

        private void Awake()
        {
            movePile =new Stack<Vector3>();
            animator = GetComponent<Animator>();
        }
    
        private void Start()
        {
            movePoint.parent = null;
            StartCoroutine(RandomDance());
        }
    
        private void Update()
        {
            animator.SetBool("IsMoving",transform.position != movePoint.position);
            if (!animator.GetBool("IsMoving"))
            {
                animator.SetBool("Rewind",false);
                CurrentMoveSpeed = moveSpeed;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position,
                CurrentMoveSpeed * Time.deltaTime);
        }

        public void MoveThisBoxTo(Vector3 positionToMove)
        {
            movePoint.position += positionToMove;
            RotateTo(positionToMove);
            movePile.Push(positionToMove);
        }
    
        IEnumerator RandomDance()
        {
            while (true)
            {
                if (!animator.GetBool("Sit"))
                {
                    float value = Random.value;
                    if(value < 0.2) animator.SetBool("Dance", true);
                    if(value > 0.8) animator.SetBool("Dance", false);
                }
            

                yield return new WaitForSeconds(Random.Range(2,3));
            
                yield return null;
            }
        }
    
        public List<BoxController> MoveTo(Vector3 positionToMove)
        {
            List<BoxController> boxesToMove = AddToList(positionToMove, new List<BoxController>());
        
            if (boxesToMove != null)
            {
                foreach (BoxController box in boxesToMove)
                {
                    box.MoveThisBoxTo(positionToMove);
                }
            }
            return boxesToMove;
        }

        public bool Rewind()
        {
            if (movePile.Count > 0)
            {
                Vector3 dir = -movePile.Pop();
                bool isAKillMove = IsPlayerTile(dir);
                movePoint.position += dir;
                animator.SetBool("Rewind",true);
                CurrentMoveSpeed = rewindMoveSpeed;
                return isAKillMove;
            }
            return false;
        }

        private List<BoxController> AddToList(Vector3 direction, List<BoxController> list)
        {
            list.Add(this);
            if (IsBoxTile(direction))
            {
                if (list.Count > 100) return null;
                return lastCollition.GetComponent<BoxController>().AddToList(direction, list);
            
            }

            if (IsAvailableTile(direction)) return list;
            else
            {
                return null;
            }
            //Debug.LogWarning("colliding with something unexpected");
            //return null;
        }
    
    }
}