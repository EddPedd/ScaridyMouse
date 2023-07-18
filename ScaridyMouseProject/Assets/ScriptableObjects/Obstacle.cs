using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Obstacle", menuName = "Obstacle")]
public class Obstacle : ScriptableObject
{
    public enum Shape
    {
        Circle,
        Square,
        Triangle,
    }
    public enum Colour
    {
        Green,
        Blue,
        Red,
    }
    public enum Sieze
    {
        Small,
        Medium,
        Large,
    }

    public Shape shape;
    public Colour colour;
    public Sieze sieze;

}
