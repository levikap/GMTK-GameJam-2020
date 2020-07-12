using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwitchController : MonoBehaviour
{

    public static bool isAwake;
    private GameObject dreamObjects;
    private GameObject awakeObjects;
    private GameObject player;
    private GameObject glow;
    public int timeToWait = 2;
    public static bool startGlow;
    public float lowerSwitchIntervalBound;
    public float higherSwitchIntervalBound;

    public Tilemap tilemapDream;
    public Tilemap tilemapAwake;

    public float time = 0.0f;
    public float waitTime = 1.0f;
    public float randomTimeInterval;
    public static bool glowState = false;
    public bool currentlySwapping = false;

    private bool dreamIsActivated = false;
    private bool initial;

    // Start is called before the first frame update
    void Start()
    {
        dreamObjects = GameObject.FindGameObjectWithTag("DreamObjects");
        awakeObjects = GameObject.FindGameObjectWithTag("AwakeObjects");
        player = GameObject.FindGameObjectWithTag("Player");
        glow = GameObject.FindGameObjectWithTag("Glow");
        glow.SetActive(glowState);
        isAwake = true;
        initial = true;
        dreamObjects.SetActive(false);
        awakeObjects.SetActive(true);
        awakeObjects.GetComponentInChildren<TilemapCollider2D>().enabled = true;
        dreamObjects.GetComponentInChildren<TilemapCollider2D>().enabled = true;
        dreamIsActivated = false;

        BoundsInt bounds = tilemapAwake.cellBounds;
        TileBase[] allTiles = tilemapAwake.GetTilesBlock(bounds);
        Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        foreach (Tile t in allTiles)
        {
            if (t != null)
            {
                t.color = color;
            }
        }
        BoundsInt bounds1 = tilemapDream.cellBounds;
        TileBase[] allTiles1 = tilemapDream.GetTilesBlock(bounds1);
        foreach (Tile t in allTiles1)
        {
            if (t != null)
            {
                t.color = color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SwapEveryTime();
       
    }

    private void SwapEveryTime()
    {
        Color colorHard = new Color(1.0f, 1.0f, 1.0f, 1f);
        Color color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
       // print("initial" + initial);
       // print("dreamisactivated" + dreamIsActivated);
        if (dreamIsActivated && !initial)
        {
            print("is awake");
               BoundsInt bounds = tilemapAwake.cellBounds;
            TileBase[] allTiles = tilemapAwake.GetTilesBlock(bounds);
            foreach (Tile t in allTiles)
            {
                if (t != null)
                {
                    t.color = colorHard;
                }
            }
        } else if (!dreamIsActivated && !initial)
        {
         //   print("is dreaming");
            BoundsInt bounds = tilemapDream.cellBounds;
            TileBase[] allTiles = tilemapDream.GetTilesBlock(bounds);
            foreach (Tile t in allTiles)
            {
                if (t != null)
                {
                    t.color = colorHard;
                }
            }
        }
        
        if (time >= randomTimeInterval)
        {
            time = 0;
            randomTimeInterval = CalculateRandomTime(false);
            if (!currentlySwapping)
            {
                initial = false;
                currentlySwapping = true;
                if (dreamIsActivated)
                {
                    //print("initial swap called to awake");
                    awakeObjects.SetActive(true);
                    awakeObjects.GetComponentInChildren<TilemapCollider2D>().enabled = false;
                    dreamObjects.GetComponentInChildren<TilemapCollider2D>().enabled = true;
                    // print("finished changing awake tile color to 0.3f");
                    BoundsInt bounds1 = tilemapAwake.cellBounds;
                    TileBase[] allTiles1 = tilemapAwake.GetTilesBlock(bounds1);
                    foreach (Tile t in allTiles1)
                    {
                        if (t != null)
                        {
                            //print("change");
                            t.color = color;
                            // print("dream" + t.color.a);
                        }
                    }
                }
                else
                {
                  //  print("initial swap called to dream");
                    //print(dreamObjects.activeSelf);
                    dreamObjects.SetActive(true);
                    dreamObjects.GetComponentInChildren<TilemapCollider2D>().enabled = false;
                    awakeObjects.GetComponentInChildren<TilemapCollider2D>().enabled = true;
                    BoundsInt bounds1 = tilemapDream.cellBounds;
                    TileBase[] allTiles1 = tilemapDream.GetTilesBlock(bounds1);
                    foreach (Tile t in allTiles1)
                    {
                        if (t != null)
                        {
                            //print("change");
                            t.color = color;
                            // print("dream" + t.color.a);
                        }
                    }
                    // print(dreamObjects.activeSelf);
                    // print("finished changing dream tile color to 0.3f");
                }
            }

            glow.SetActive(true);
            Invoke("Swap", 2);
        }

        time += Time.deltaTime;
    }

    public void Swap()
    {
        SoundManagerScript.PlaySound("Transition1");
        glow.SetActive(false);
        isAwake = !isAwake;
        Color color = new Color(1.0f, 1.0f, 1.0f, 1f);

        if (dreamObjects.GetComponentInChildren<TilemapCollider2D>().enabled == true)
        {
           // print("swap called to awake");
            awakeObjects.SetActive(true);
            awakeObjects.GetComponentInChildren<TilemapCollider2D>().enabled = true;
            dreamObjects.GetComponentInChildren<TilemapCollider2D>().enabled = false;
            dreamObjects.SetActive(false);
            //BoundsInt bounds = tilemapAwake.cellBounds;
            //TileBase[] allTiles = tilemapAwake.GetTilesBlock(bounds);
            //foreach (Tile t in allTiles)
            //{
            //    if (t != null)
            //    {
            //        t.color = color;
            //    }
            //}
          //  print("finished changing awake tile color to 1.0f");
        }
        else
        {
           // print("swap called to dream");
            dreamObjects.SetActive(true);
            dreamObjects.GetComponentInChildren<TilemapCollider2D>().enabled = true;
            awakeObjects.GetComponentInChildren<TilemapCollider2D>().enabled = false;
            awakeObjects.SetActive(false);
            //BoundsInt bounds = tilemapDream.cellBounds;
            //TileBase[] allTiles = tilemapDream.GetTilesBlock(bounds);
            //foreach (Tile t in allTiles)
            //{
            //    if (t != null)
            //    {
            //        t.color = color;
            //    }
            //}
        //    print("finished changing dream tile color to 1.0f");
        }
        dreamIsActivated = !dreamIsActivated;
        currentlySwapping = false;
    }

    public float CalculateRandomTime(bool doItNow)
    {
        if(doItNow)
        {
            randomTimeInterval = 2.0f;
            return 2.0f;
        } else
        {
            float random = Random.Range(lowerSwitchIntervalBound, higherSwitchIntervalBound);
            return random;
        }
    }

    private IEnumerator WaitABit()
    {
        glow.SetActive(!glowState);

        Debug.Log("waiting!");
        yield return new WaitForSeconds(time);
        
    }
}


