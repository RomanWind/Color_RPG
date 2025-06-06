using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.ResetPlayer();
            }
        }
    }
}