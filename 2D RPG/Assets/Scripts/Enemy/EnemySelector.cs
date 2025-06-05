using System;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject selectorSprite;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        if (enemySelected == enemyBrain)
        {
            selectorSprite.SetActive(true);
        }
        else
        {
            selectorSprite.SetActive(false);
        }
    }

    private void NoSelectedCallback()
    {
        selectorSprite.SetActive(false);
    }
    
    private void OnEnable()
    {
        SelectionManager.OnEnemySelected += EnemySelectedCallback;
        SelectionManager.OnNoSelection += NoSelectedCallback;
    }

    private void OnDisable()
    {
        SelectionManager.OnEnemySelected -= EnemySelectedCallback;
        SelectionManager.OnNoSelection -= NoSelectedCallback;
    }
}