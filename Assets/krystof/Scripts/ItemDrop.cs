using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] List<RarityGroup> listOfCurrentRarity = new List<RarityGroup>();
    [SerializeField] Transform _dropPoint;
    [SerializeField] private Player _player;

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
        string playerClass = _player.Class;

        foreach(RarityGroup element in listOfCurrentRarity){
            if(randomNumber > element._minNumber && randomNumber <= element._maxNumeber)
            {
                foreach (var VARIABLE in element.ClassList)
                {
                    if (VARIABLE.ClassName == playerClass)
                    {
                        randomNumber = Random.Range(0, VARIABLE._rarityList.Count - 1);

                        Instantiate(VARIABLE._rarityList[randomNumber], _dropPoint);
                        return; 
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class RarityGroup{
    public RarityOfList _listRarity;
    public List<ListClass> ClassList = new List<ListClass>();

    public int _minNumber;
    public int _maxNumeber;
}
[System.Serializable]
public class ListClass
{
    public string ClassName;
    public List<GameObject> _rarityList = new List<GameObject>();
}

public enum RarityOfList{
        Common,
        Rare,
        Epic,
        Legendary
    }


