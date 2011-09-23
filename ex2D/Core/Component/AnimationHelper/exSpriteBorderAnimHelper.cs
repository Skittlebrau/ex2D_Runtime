// ======================================================================================
// File         : exSpriteBorderAnimHelper.cs
// Author       : Wu Jie 
// Last Change  : 09/23/2011 | 12:15:21 PM | Friday,September
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
// usings
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

///////////////////////////////////////////////////////////////////////////////
// defines
///////////////////////////////////////////////////////////////////////////////

[ExecuteInEditMode]
public class exSpriteBorderAnimHelper : exSpriteBaseAnimHelper {

    exSpriteBorder spriteBorder;
    Color lastColor = Color.white;
    float lastWidth;
    float lastHeight;

    ///////////////////////////////////////////////////////////////////////////////
    // functions
    ///////////////////////////////////////////////////////////////////////////////

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    override protected void Awake () {
        base.Awake();

        spriteBorder = GetComponent<exSpriteBorder>();
        lastColor = spriteBorder.color;
        lastWidth = spriteBorder.width;
        lastHeight = spriteBorder.height;
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    override protected void Update () {
        base.Update();

        if ( lastColor != spriteBorder.color ) {
            lastColor = spriteBorder.color;
            spriteBorder.updateFlags |= exPlane.UpdateFlags.Color;
        }
        if ( lastWidth != spriteBorder.width ) {
            lastWidth = spriteBorder.width;
            spriteBorder.updateFlags |= exPlane.UpdateFlags.Vertex;
        }
        if ( lastHeight != spriteBorder.height ) {
            lastHeight = spriteBorder.height;
            spriteBorder.updateFlags |= exPlane.UpdateFlags.Vertex;
        }
    }
}