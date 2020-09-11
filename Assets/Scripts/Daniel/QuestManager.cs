using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class QuestManager : MonoBehaviour
{
    // Volcano Prefabs
    public Tilemap tilemap;
    public Tile lavaTile;

    // Port Prefabs
    public GameObject fish;
    public GameObject net;

    // Farm Prefabs
    public ChronoObject ricePaddy;

    public bool GetQuestResult(string npcName)
    {
        switch(npcName)
        {
            case "Farmer":
                return FarmerResult();
            case "Fishing Guy":
                return FishingResult();
            case "Bath Dude":
                return SpringsResult();
            default:
                return false;
        }
    }

    private bool FarmerResult()
    {
        return ricePaddy.GetTimeState() == 1;
    }

    private bool FishingResult()
    {
        return (fish.transform.position - net.transform.position).sqrMagnitude < 1;
    }

    private bool SpringsResult()
    {
        Vector3 start = new Vector3(17.5f, -4.5f, 0f);
        for (int i = 0; i < 6; i++)
        {
            if (tilemap.GetTile(tilemap.WorldToCell(start + Vector3.right * i)) != lavaTile)
                return false;
        }
        return true;
    }
}
