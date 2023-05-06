using UnityEngine;

namespace Rubbish
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void HandleEButton()
        {
        
        }

        private void HandleSpaceBar()
        {
            // Implement logic to handle space bar input from the keyboard
            // For example, you can check if the player is standing on a tile that can be interacted with and call the appropriate method to interact with it
        }
    }
}
