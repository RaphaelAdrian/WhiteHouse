using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
 #endif

public class UniqueID : MonoBehaviour
{
    // 0 means nothing is generated yet
    public string uniqueId;
   // static Dictionary<string, UniqueID> allGuids = new Dictionary<string, UniqueID> ();

   //  // Start is called before the first frame update
   //  // Only compile the code in an editor build
   //   #if UNITY_EDITOR
   //   // Whenever something changes in the editor (note the [ExecuteInEditMode])
   //   void Update(){
   //      if (!string.IsNullOrEmpty(uniqueId)) return;
   //      if (gameObject.scene == null) return;
   //      if (EditorApplication.isPlayingOrWillChangePlaymode) return;

   //      uniqueId = Guid.NewGuid().ToString();
   //      EditorUtility.SetDirty(this);
   //   }

   //   #endif
}
