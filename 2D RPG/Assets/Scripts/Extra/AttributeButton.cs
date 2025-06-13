using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AttributeButton : MonoBehaviour
{
    public static event Action<AttributeType> OnAttributeSelectedEvent;
    [FormerlySerializedAs("attribute")]
    [Header("Config")]
    [SerializeField] private AttributeType attributeType;

    public void SelectAttribute()
    {
        OnAttributeSelectedEvent?.Invoke(attributeType);
    }
}
