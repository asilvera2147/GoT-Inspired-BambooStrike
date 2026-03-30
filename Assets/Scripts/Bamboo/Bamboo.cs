using UnityEngine;
using UnityEngine.UI;

public class Bamboo : MonoBehaviour
{
    public int id;
    public string buttonCode = "";
    private SpriteRenderer image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        image = GetComponentInChildren<SpriteRenderer>();

    }

    public void UpdateImage()
    {
        Sprite sprite = null;
        switch (buttonCode)
        {
            case "X":
                sprite = Resources.Load<Sprite>("Icons/cross");
                break;
            case "Circle":
                sprite = Resources.Load<Sprite>("Icons/circle");
                break;
            case "Triangle":
                sprite = Resources.Load<Sprite>("Icons/triangle");
                break;
            case "Square":
                sprite = Resources.Load<Sprite>("Icons/square");
                break;
            case "L1":
                sprite = Resources.Load<Sprite>("Icons/L1");
                break;
            case "R1":
                sprite = Resources.Load<Sprite>("Icons/R1");
                break;
            default:
                Debug.Log("Wrong Button Code");
                break;
        }
        image.sprite = sprite;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Sword")
        {
            try
            {
                if (buttonCode == InputIndexer.instance.inputs[id])
                {
                    //send message to the Bamboo Manager. Send this game object too so that you can change it with the sliced one if right button was pressed
                    Debug.Log("You Hit The Bamboo no:" + id.ToString() + "With the button of " + buttonCode);
                    BambooStandManager.instance.IncreaseSlicedBamboos();
                    gameObject.SetActive(false);
                }
                else
                {
                    BambooStandManager.instance.ResetTier();
                    Debug.Log("Wrong button to slice the bamboo.");
                }
            }
            catch (System.Exception)
            {
                BambooStandManager.instance.ResetTier();
                Debug.Log("Too slow to slice the bamboo.");
            }
            

        }
    }
}
