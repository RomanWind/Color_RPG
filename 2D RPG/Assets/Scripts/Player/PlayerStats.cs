using UnityEngine;

public enum AttributeType
{
    Strength,
    Dexterity,
    Intelligence
}

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

	[Header("Experience")]
	public float CurrentExp;
	public float NextLevelExp;
	public float InitialNextLevelExp;
	[Range(1f,100f)]public float ExpMultiplier;
	
	[Header("Attack")]
	public float BaseDamage;
	public float CriticalChance;
	public float CriticalDamage;
	
	[Header("Attributes")]
	public int Strength;
	public int Dexterity;
	public int Intelligence;
	public int AttributePoints;

	[HideInInspector]public float TotalExp;
	[HideInInspector]public float TotalDamage;
	
	public void ResetPlayer()
	{
		Health = MaxHealth;
		Mana = MaxMana;
		Level = 1;
		CurrentExp = 0f;
		NextLevelExp = InitialNextLevelExp;
		TotalExp = 0f;
		BaseDamage = 2f;
		CriticalChance = 10f;
		CriticalDamage = 50f;
		Strength = 0;
		Dexterity = 0;
		Intelligence = 0;
		AttributePoints = 0;
	}
}
