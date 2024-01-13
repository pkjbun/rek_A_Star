using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Helper class for JSon Serialization 
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class Serialization<T>
{
    [SerializeField] List<T> items;
    public List<T> ToList() { return items; }
    public Serialization(List<T> items)
    {
        this.items = items;
    }
}
