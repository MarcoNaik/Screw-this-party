using System.Collections.Generic;
using UnityEngine;

namespace LevelProps
{
    public class DoorController : MonoBehaviour
    {
        private Collider2D _collider;
        private List<LocksData> lockers;
        private Dictionary<ButtonController,LocksData> hashButtons;
        private Dictionary<KeyController,LocksData> hashKeys;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            lockers = new List<LocksData>();
            hashButtons = new Dictionary<ButtonController, LocksData>();
            hashKeys = new Dictionary<KeyController, LocksData>();
            _collider = GetComponent<Collider2D>();
        }

        private void LockersCheck()
        {
            foreach (LocksData locksData in lockers)
            {
                if (!locksData.state)
                {
                    Close();
                    return;
                }
            }
            Open();
        }

        public void SetUp(ButtonController button)
        {
            LocksData locksData = new LocksData(button, false);

            lockers.Add(locksData);
            hashButtons.Add(button,locksData);
        }
    
        public void SetUp(KeyController key)
        {
            LocksData locksData = new LocksData(key, false);

            lockers.Add(locksData);
            hashKeys.Add(key,locksData);
        }
        public void Unlock(ButtonController buttonController)
        {
            hashButtons[buttonController].state = true;
            LockersCheck();
        }
    
        public void Unlock(KeyController keyController)
        {
            hashKeys[keyController].state = true;

            LockersCheck();
        }
    
        public void Lock(ButtonController buttonController)
        {
            hashButtons[buttonController].state = false;
            LockersCheck();
        }
    
        public void Lock(KeyController keyController)
        {
            hashKeys[keyController].state = false;
            LockersCheck();
        }
    
    
    
        private void Open()
        {
            _collider.enabled = false;
            animator.SetBool("Opened", true);
            Debug.Log("opened");
        }

        private void Close()
        {
            _collider.enabled = true;
        
            animator.SetBool("Opened", false);
        }
    
        private class LocksData
        {
            internal KeyController key;
            internal ButtonController button;
            internal bool state;

            public LocksData(ButtonController button, bool state)
            {
                this.key = null;
                this.button = button;
                this.state = state;
            }
        
            public LocksData(KeyController key, bool state)
            {
                this.key = key;
                this.button = null;
                this.state = state;
            }
        }

    
    }
}
