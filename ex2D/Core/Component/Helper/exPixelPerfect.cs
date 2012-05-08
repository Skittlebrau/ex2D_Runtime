// ======================================================================================
// File         : exPixelPerfect.cs
// Author       : Wu Jie 
// Last Change  : 07/24/2011 | 20:19:52 PM | Sunday,July
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
// usings
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///////////////////////////////////////////////////////////////////////////////
/// 
/// A component to handle pixel perfect process
/// 
///////////////////////////////////////////////////////////////////////////////

[ExecuteInEditMode]
public class exPixelPerfect : MonoBehaviour {

    ///////////////////////////////////////////////////////////////////////////////
    // private data
    ///////////////////////////////////////////////////////////////////////////////

    // ------------------------------------------------------------------ 
    /// The cached sprite base component
    // ------------------------------------------------------------------ 

    [System.NonSerialized] public exSpriteBase sprite;
    [System.NonSerialized] public Vector3 toCamera;

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    exPixelPerfectCamera ppfCamera;

    ///////////////////////////////////////////////////////////////////////////////
    // functions
    ///////////////////////////////////////////////////////////////////////////////

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    void Awake () {
        sprite = GetComponent<exSpriteBase>();
        ppfCamera = sprite.renderCamera.GetComponent<exPixelPerfectCamera>();
        if ( ppfCamera == null ) {
            ppfCamera = sprite.renderCamera.gameObject.AddComponent<exPixelPerfectCamera>();
        }

        UpdatePixelPerfectCamera (ppfCamera);
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    void OnEnable () {
        if ( sprite == null ) {
            sprite = GetComponent<exSpriteBase>();
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    void OnDestroy () {
        if ( sprite != null )
            sprite.ppfScale = Vector2.one; 
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    public void UpdatePixelPerfectCamera ( exPixelPerfectCamera _ppfCamera ) {
        ppfCamera = _ppfCamera;
        if ( sprite.renderCamera.orthographic == false ) {
            toCamera = transform.position - sprite.renderCamera.transform.position;
        }

        //
        ppfCamera.CalculatePixelPerfectScale ( sprite, toCamera.magnitude );
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // NOTE: if in LateUpdate, it may not go into sprite.Commit while changes
    // ------------------------------------------------------------------ 

    void Update () {
        if ( sprite != null && sprite.renderCamera.orthographic == false ) {
            Vector3 newToCamera = transform.position - sprite.renderCamera.transform.position;

            if ( newToCamera.sqrMagnitude != toCamera.sqrMagnitude ) {
                toCamera = newToCamera;

                //
                if ( ppfCamera == null || ppfCamera.camera != sprite.renderCamera ) {
                    ppfCamera = sprite.renderCamera.GetComponent<exPixelPerfectCamera>();
                    if ( ppfCamera == null ) {
                        ppfCamera = sprite.renderCamera.gameObject.AddComponent<exPixelPerfectCamera>();
                    }
                }
                ppfCamera.CalculatePixelPerfectScale ( sprite, toCamera.magnitude );
            }
        }

        // Snap sprite position to pixel boundary.
        if (sprite != null && ppfCamera != null && ppfCamera.scale > 0.0f)
        {
            float ooScale = 1.0f / ppfCamera.scale;
            Vector3 currPos = sprite.transform.position;
            sprite.transform.position = new Vector3 (Mathf.Round (currPos.x * ooScale) * ppfCamera.scale, Mathf.Round (currPos.y * ooScale) * ppfCamera.scale, currPos.z);
        }
    }
}
