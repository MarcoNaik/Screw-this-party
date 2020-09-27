using MovementAndChars;

namespace LevelProps
{
    public class AddRewindController : AbstractConsumable
    {
        private PlayerController player;
        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
        }

        protected override void PickedTrigger()
        {
            player.AddRewind();
        }
    }
}
