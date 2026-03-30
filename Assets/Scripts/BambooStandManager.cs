using System.Collections.Generic;
using UnityEngine;

public class BambooStandManager : MonoBehaviour
{
    public enum TierState
    {
        tier1,
        tier2,
        tier3,
    }
    public TierState tierState;
    public List<Bamboo> bamboos = new List<Bamboo>();
    public List<string> buttonCodes = new List<string>();
    public static BambooStandManager instance;
    public Animator animator;
    public int slicedBamboos = 0;
    private bool tier1Setup = false, tier2Setup = false, tier3Setup = false;
    public enum GameType
    {
        Classic,
        RandomEachFail
    }
    public GameType gameType = GameType.Classic;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        tierState = TierState.tier1;
        SetStand(tierState);
    }

    public void SetStand(TierState tierState)
    {
        slicedBamboos = 0;
        animator.speed = 1;
        if (tierState == TierState.tier1)
        {
            if (gameType == GameType.Classic)
            {

                if (!tier1Setup)
                {
                    SetUpBamboos(3);
                    tier1Setup = true;
                }
                else
                {
                    ReloadBamboos(3);
                }
            }
            else if (gameType == GameType.RandomEachFail)
            {
                SetUpBamboos(3);
            }
        }
        else if (tierState == TierState.tier2)
        {
            if (gameType == GameType.Classic)
            {

                if (!tier2Setup)
                {
                    SetUpBamboos(5);
                    tier2Setup = true;
                }
                else
                {
                    ReloadBamboos(5);
                }
            }
            else if (gameType == GameType.RandomEachFail)
            {
                SetUpBamboos(5);
            }
        }
        else if (tierState == TierState.tier3)
        {
            if (gameType == GameType.Classic)
            {

                if (!tier3Setup)
                {
                    SetUpBamboos(7);
                    tier3Setup = true;
                }
                else
                {
                    ReloadBamboos(7);
                }
            }
            else if (gameType == GameType.RandomEachFail)
            {
                SetUpBamboos(7);
            }
        }
    }

    private void ReloadBamboos(int bambooCount)
    {
        for (int i = 0; i < bambooCount; i++)
        {
            bamboos[i].gameObject.SetActive(true);
        }
    }

    private void SetUpBamboos(int bambooCount)
    {
        for (int i = 0; i < bamboos.Count; i++)
        {
            if (i < bambooCount)
            {
                bamboos[i].gameObject.SetActive(true);
                bamboos[i].buttonCode = buttonCodes[Random.Range(0, buttonCodes.Count)];
                bamboos[i].id = i;
                bamboos[i].UpdateImage();
            }
            else
            {
                bamboos[i].gameObject.SetActive(false);
            }

        }
    }

    public void IncreaseSlicedBamboos()
    {
        slicedBamboos++;
    }


    // Update is called once per frame
    void Update()
    {
        if (InputIndexer.instance.inputs.Count > 0)
        {
            animator.SetTrigger("Strike");
        }
        if (tierState == TierState.tier1 && slicedBamboos == 3)
        {
            TierPass(TierState.tier2);
        }
        else if (tierState == TierState.tier2 && slicedBamboos == 5)
        {
            TierPass(TierState.tier3);
        }
        else if (tierState == TierState.tier3 && slicedBamboos == 7)
        {
            //Player have won the mini game
            Debug.Log("Player have won the mini game");
        }
    }

    private void TierPass(TierState newTierState)
    {
        tierState = newTierState;
        Debug.Log("Victory can pass next tier:" + tierState.ToString());
        SetStand(tierState);
        ResetTier();
        Invoke("ReAnimatePlayer", 2f);
    }

    void ReAnimatePlayer()
    {
        animator.gameObject.SetActive(true);

    }
    public void ResetTier()
    {
        animator.gameObject.SetActive(false);
        InputIndexer.instance.ResetList();
        SetStand(tierState);
        Invoke("ReAnimatePlayer", 2f);
    }
}
