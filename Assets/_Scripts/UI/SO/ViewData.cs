using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ViewData", fileName = "ViewData")]
public class ViewData : ScriptableObject
{
    public ThemeData theme;
    public RectOffset padding;
    public float spacing;
}
