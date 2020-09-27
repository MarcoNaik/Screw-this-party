using LevelProps;
using UnityEngine;

namespace MovementAndChars
{
    public abstract class CanMove:MonoBehaviour
    {
        public float moveSpeed;
        protected float CurrentMoveSpeed;
        public Transform movePoint;
        protected Collider2D lastCollition;
        private Vector2 laterallWall= Vector2.zero;


        private void OnEnable()
        {
            CurrentMoveSpeed = moveSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("LateralWall"))
            {
                laterallWall += other.gameObject.GetComponent<LateralWallController>().direction;
                
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("LateralWall")) laterallWall -= other.gameObject.GetComponent<LateralWallController>().direction;
        }

        protected bool IsBoxTile(Vector3 dir)
        {
            Collider2D[] collitionArray = Physics2D.OverlapCircleAll(movePoint.position + dir, 0.2f);
            if (collitionArray == null) return false;
            foreach (var collider2D1 in collitionArray)
            {
                lastCollition = collider2D1;
                if (collider2D1.gameObject.CompareTag("Box"))
                {
                    return true;
                }
            }
            return false;
        }
    
        protected internal bool IsPlayerTile(Vector3 dir)
        {
            Collider2D[] collitionArray = Physics2D.OverlapCircleAll(movePoint.position + dir, 0.2f);
            if (collitionArray == null)
            {
            
                return false;
            }
            foreach (Collider2D collider2D1 in collitionArray)
            {
                lastCollition = collider2D1;
            
                if (collider2D1.gameObject.CompareTag("Player"))
                {
                    return true;
                }
            }
            return false;
        }

        protected void RotateTo(Vector3 dir)
        {
            if (dir != Vector3.zero)
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        protected bool IsAvailableTile(Vector3 dir)
        {
            Vector2 dir2 = new Vector2(dir.x,dir.y);
            //current object is curently on a laterall wall case
       
            if (laterallWall.x == dir2.x  && laterallWall.x != 0) return false;
            if (laterallWall.y == dir2.y  && laterallWall.y != 0) return false;
        
        
            //is direction occupied by a wall
            Collider2D[] collitionArray = Physics2D.OverlapCircleAll(movePoint.position + dir, 0.2f);
            if (collitionArray == null) return true;

            foreach (Collider2D collider2D1 in collitionArray)
            {
                lastCollition = collider2D1;
                GameObject collitionGO = collider2D1.gameObject;
                if (collitionGO.layer == LayerMask.NameToLayer("Wall")) return false;
                if (collitionGO.layer == LayerMask.NameToLayer("LateralWall"))
                {
               
                    Vector2 lateralWallCollition = collitionGO.GetComponent<LateralWallController>().direction;
                    if (-lateralWallCollition.x == dir2.x  && lateralWallCollition.x != 0) return false;
                    if (-lateralWallCollition.y == dir2.y  && lateralWallCollition.y != 0) return false;
                }
            }
            return true;
        }
    }
}