//imports UnityEngine

using System;
using System.Reflection;
using UnityEditor;

public class IconManager
{
    public enum LabelIcon
    {
        Gray = 0,
        Blue,
        Teal,
        Green,
        Yellow,
        Orange,
        Red,
        Purple
    }

    public enum Icon
    {
        CircleGray = 0,
        CircleBlue,
        CircleTeal,
        CircleGreen,
        CircleYellow,
        CircleOrange,
        CircleRed,
        CirclePurple,
        DiamondGray,
        DiamondBlue,
        DiamondTeal,
        DiamondGreen,
        DiamondYellow,
        DiamondOrange,
        DiamondRed,
        DiamondPurple
    }

    private static UnityEngine.GUIContent[] labelIcons;
    private static UnityEngine.GUIContent[] largeIcons;

    public static void SetIcon(UnityEngine.GameObject gObj, LabelIcon icon)
    {
        if (labelIcons == null)
        {
            labelIcons = GetTextures("sv_label_", string.Empty, 0, 8);
        }

        SetIcon(gObj, labelIcons[(int)icon].image as UnityEngine.Texture2D);
    }

    public static void SetIcon(UnityEngine.GameObject gObj, Icon icon)
    {
        if (largeIcons == null)
        {
            largeIcons = GetTextures("sv_icon_dot", "_pix16_gizmo", 0, 16);
        }

        SetIcon(gObj, largeIcons[(int)icon].image as UnityEngine.Texture2D);
    }

    private static void SetIcon(UnityEngine.GameObject gObj, UnityEngine.Texture2D texture)
    {
        var ty = typeof(EditorGUIUtility);
        var mi = ty.GetMethod("SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);
        mi.Invoke(null, new object[] { gObj, texture });
    }

    private static UnityEngine.GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
    {
        UnityEngine.GUIContent[] guiContentArray = new UnityEngine.GUIContent[count];

        var t = typeof(EditorGUIUtility);
        var mi = t.GetMethod("IconContent", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(string) }, null);

        for (int index = 0; index < count; ++index)
        {
            guiContentArray[index] = mi.Invoke(null, new object[] { baseName + (object)(startIndex + index) + postFix }) as UnityEngine.GUIContent;
        }

        return guiContentArray;
    }
}
