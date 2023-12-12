using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] List<RarityGroup> listOfCurrentRarity = new List<RarityGroup>();
    [SerializeField] Transform _dropPoint;

   public void Start()
    {
        StartCoroutine(Time());
    }

    // Tato část kódu je jen pro testovací účely, po přidání pravých nepřátel bude odstraněna. 
    IEnumerator Time(){
        DropItem();
        yield return new WaitForSeconds(2);
        StartCoroutine(Time());
    }

    void DropItem(){
        int randomNumber = Random.Range(0, 101);

        foreach(RarityGroup element in listOfCurrentRarity){
            if(randomNumber > element._minNumber && randomNumber <= element._maxNumeber)
            {
                randomNumber = Random.Range(0, element._rarityList.Count - 1);

                Instantiate(element._rarityList[randomNumber], _dropPoint);
                return;
            }
        }
    }
}

[System.Serializable]
public class RarityGroup{
    public RarityOfList _listRarity;
    public List<GameObject> _rarityList = new List<GameObject>();

    public int _minNumber;
    public int _maxNumeber;
}

public enum RarityOfList{
        Common,
        Rare,
        Epic,
        Legendary
    }
