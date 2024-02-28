using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/TextData", fileName = "TextData")]
public class UITextData : ScriptableObject
{
    public ThemeData theme;
    public TMP_FontAsset font;
    public float size;
}
