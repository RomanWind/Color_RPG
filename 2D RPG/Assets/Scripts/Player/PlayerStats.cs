using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")] 
    public int Level;
    
    [Header("Health")]
    public float Health;
    public float MaxHealth;
    
    [Header("Mana")]
	public float Mana;
	public float MaxMana;

	public void ResetPlayer()
	{
		Health = MaxHealth;
		Mana = MaxMana;
	}
}
