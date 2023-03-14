using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSwapper : MonoBehaviour
{
    private TerrainChecker terrainChecker;
    private int currentLayer;
    Player player;
    public FootstepCollection[] footstepCollections;
    // Start is called before the first frame update
    void Start()
    {
        terrainChecker = new TerrainChecker();
        player = GameManager.instance.player;
    }

    public void CheckLayers() {
        RaycastHit hit;
        if(Physics.Raycast(player.transform.position, Vector3.down, out hit, 3)) {
            FootstepSurface surface = hit.transform.GetComponent<FootstepSurface>();
            if (surface) {
                player.SwapFootSteps(surface.footstepCollection);
            }
            else {
                Terrain terrain = hit.transform.GetComponent<Terrain>();
                if (terrain) {
                    int newLayer = terrainChecker.GetLayer(player.transform.position, terrain);
                    if (currentLayer != newLayer){
                        currentLayer = newLayer;
                        // swap
                        player.SwapFootSteps(footstepCollections[currentLayer]);
                    }
                }
            }
        }
    }
}
