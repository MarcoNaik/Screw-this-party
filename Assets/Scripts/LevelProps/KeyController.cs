using System.Collections.Generic;

namespace LevelProps
{
    public class KeyController : AbstractConsumable
    {
    
        public List<DoorController> doorsToOpen;

        private void Start()
        {
            foreach (DoorController doorController in doorsToOpen)
            {
                doorController.SetUp(this);
            }
        }
        protected override void PickedTrigger()
        {
            foreach (DoorController doorController in doorsToOpen)
            {
                doorController.Unlock(this);
            }
        }
    
    
    }
}
