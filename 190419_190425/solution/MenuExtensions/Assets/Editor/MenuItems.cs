using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItems {
    [MenuItem("Window/新建项")]
    private static void NewMenuOption()
    {

    }
    [MenuItem("Window/a/b/c/d/e/f/g")]
    private static void NewMenuOption1() {

    }
    [MenuItem("工具/新建项")]
    private static void NewMenuOption2() {

    }

    [MenuItem("菜单分类/项1", false, 1)]
    private static void Priority1() { }
    [MenuItem("菜单分类/项2", false, 2)]
    private static void Priority2() { }
    [MenuItem("菜单分类/项14", false, 14)]
    private static void Priority14() { }
    [MenuItem("菜单分类/项51", false, 51)]
    private static void Priority51() { }
    [MenuItem("菜单分类/项52", false, 52)]
    private static void Priority52() { }

    [MenuItem("Assets/处理纹理")]
    private static void ProcessTexture() {

    }
    [MenuItem("Assets/处理纹理", true)]
    private static bool ProcessTextureValidation() {
        return Selection.activeObject?.GetType() == typeof(Texture2D);
    }
}
