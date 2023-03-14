using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public int resWidth = 800; 
    public int resHeight = 400;
 
    public static string ScreenShotName(int width, int height, int slotNumber) {
        return string.Format("{0}/savedslot-{1}.jpg", 
                            Application.persistentDataPath, slotNumber);
    }

    public void TakeScreenshot(int slotNumber) {
        Camera camera = GameManager.instance.player.playerCamera;
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToJPG();
        string filename = ScreenShotName(resWidth, resHeight, slotNumber);
        System.IO.File.WriteAllBytes(filename, bytes);

        new System.Threading.Thread(() =>
        {
            // create file and write optional header with image bytes
            var f = System.IO.File.Create(filename);
            f.Write(bytes, 0, bytes.Length);
            f.Close();
        }).Start();

        Destroy(rt);
        rt = null;
        screenShot = null;
    }
}
