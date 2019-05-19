using UnityEngine;

public delegate void OnPositionChangeHandler(FieldPosition originPosition, FieldPosition targetPosition);

public interface IMovable
{
    event OnPositionChangeHandler OnPositionChange;
}
