using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFade : MonoBehaviour
{
    /*    public float fadePerSecond = 2.5f;
        private Color[] colors;
        public bool isFadeIn;
        private void Start()
        {
            colors = GetComponentsInChildren<Color>();
        }

        private void Update()
        {

            foreach (Color c in colors)
            {
                if (isFadeIn)
                {
                    StartCoroutine(FadeInObject(c));
                }

                else if (!isFadeIn)
                {
                    StartCoroutine(FadeOutObject(c));
                }

            }
        }


        IEnumerator FadeOutObject(Color color)
        {
            float fadeAmount = color.a - (fadePerSecond * Time.deltaTime);
            while (color.a < 1)
            {
                foreach (Color c in colors)
                {
                    color = new Color(color.r, color.g, color.b, fadeAmount);
                }
                yield return null;
            }

        }

        IEnumerator FadeInObject(Color color)
        {
            float fadeAmount = color.a + (fadePerSecond * Time.deltaTime);

            while (color.a >1)
            {

                foreach (Color c in colors)
                {
                    color = new Color(color.r, color.g, color.b, fadeAmount);

                }
                yield return null;
            }

        }
    */

/*
    public Renderer[] renderers;
    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }
    private void Update()
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }
    }*/
}
