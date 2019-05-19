using UnityEngine;
using System.Collections;

public class FieldPosition
{
    int width, height;
    public Vector2 origin;
    public bool jump = false;

    void CheckBorderConditions(FieldPosition prevoiusPosition)
    {
        jump = prevoiusPosition.jump;
        if (origin.x < 0) { origin.x += width; jump = true; }
        if (origin.x > width - 1) { origin.x -= width; jump = true; }
        if (origin.y < 0) { origin.y += height; jump = true; }
        if (origin.y > height - 1) { origin.y -= height; jump = true; }
    }

    public static FieldPosition operator + (FieldPosition fieldPosition, Vector2 transition)
    {
        FieldPosition newPosition = new FieldPosition(fieldPosition.origin + transition,
            fieldPosition.width, fieldPosition.height);
        newPosition.CheckBorderConditions(fieldPosition);
        return newPosition;
    }

    public static FieldPosition operator - (FieldPosition fieldPosition, Vector2 transition)
    {
        FieldPosition newPosition = new FieldPosition(fieldPosition.origin - transition,
            fieldPosition.width, fieldPosition.height);
        newPosition.CheckBorderConditions(fieldPosition);
        return newPosition;
    }

    public static implicit operator Vector2(FieldPosition position)
    {
        return position.origin;
    }

    public FieldPosition(Vector2 origin, int width, int height)
    {
        this.origin = origin;
        this.width = width; this.height = height;
    }
}
