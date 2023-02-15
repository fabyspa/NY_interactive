using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using AirFishLab.ScrollingList;
// The bank for providing the content for the box to display
// Must be inherit from the class BaseListBank
public class IntListBank1 : BaseListBank
{
    LoadExcel database;
    private int countTypes;

    //array contenente i nomi 
    [SerializeField]
    private string[] _contents;

   


    // This function will be invoked by the `CircularScrollingList`
    // when acquiring the content to display
    // The object returned will be converted to the type `object`
    // which will be converted back to its own type in `IntListBox.UpdateDisplayContent()`
    public override object GetListContent(int index)
    {

        _contents = Populate();
        return _contents[index];
    }
    public override int GetListLength()
    {
        return _contents.Length;
    }

    private string[] Populate()
    {
        database = GameObject.FindAnyObjectByType<LoadExcel>();
        countTypes = database.type.Count;
        string[] array = new string[countTypes];

        for (int i = 0; i < database.type.Count; i++)
        {

            array[i] = database.type[i];
        }

        return array;
    }

}
