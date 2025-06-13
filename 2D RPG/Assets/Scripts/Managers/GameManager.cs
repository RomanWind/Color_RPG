using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField] private Player player;

        private void Awake()
        {
            Instance = this;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.ResetPlayer();
            }
        }

        public void AddPlayerExp(float expAmount)
        {
            PlayerExp playerExp = player.GetComponent<PlayerExp>();
            playerExp.AddExp(expAmount);
        }
    }
}