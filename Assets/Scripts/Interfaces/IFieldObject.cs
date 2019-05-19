using UnityEngine;
using System.Collections;

public interface IFieldObject
{
    GameObject[,] Handler(GameObject[,] matrix, Vector2 direction);
    GameObject go { get; }
}
