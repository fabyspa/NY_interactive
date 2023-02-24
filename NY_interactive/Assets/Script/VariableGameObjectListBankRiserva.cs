using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


namespace AirFishLab.ScrollingList
{
    public class VariableGameObjectListBankRiserva : BaseListBank
    {

        LoadExcel loadexcel;
        //[SerializeField]
        //private InputField _contentInputField;
        private List<Riserva> _contentsList = new List<Riserva>();
        public Riserva[] _contents;
        [SerializeField]
        private CircularScrollingListRiserva _circularList;
        [SerializeField]
        private GameObject gameobjectToClone;
        
        //[SerializeField]
        //private CircularScrollingList _thirdCircular;
        // [SerializeField]
        // private CircularScrollingList _linearList;

        private readonly DataWrapper _dataWrapper = new DataWrapper();

        /// <summary>
        /// Extract the contents from the input field and refresh the list
        /// </summary>
       

        public override object GetListContent(int index)
        {
            _dataWrapper.data = _contents[index];
            return _dataWrapper;
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }

        public void ChangeInfoContents(string type)
        {
            loadexcel = GameObject.FindObjectOfType<LoadExcel>();
            _contentsList.Clear();
            
            if (type == "Tutte")
            {
                foreach (Riserva r in loadexcel.ordenList)
                {
                    _contentsList.Add(r);
                }
                loadexcel.riservaDatabaseType.Clear();
                loadexcel.riservaDatabaseType.AddRange(loadexcel.ordenList);
                _contents = _contentsList.ToArray();
                _circularList.Refresh();
                GetCenterItem();
                loadexcel.InstantiatePoints(loadexcel.ordenList);
            }
            else if (loadexcel.type.Contains(type))
            {
                loadexcel.LoadRiservaByType(type);
                _contentsList.Clear();
                foreach (Riserva r in loadexcel.riservaDatabaseType)
                { 
                    _contentsList.Add(r);
                }

                _contents = _contentsList.ToArray();
                _circularList.Refresh();
                GetCenterItem();

                loadexcel.InstantiatePoints(loadexcel.riservaDatabase);

                //Debug.Log(loadexcel.pointList.Count);
            }

            //loadexcel.AddState();
            var myKey = loadexcel.coord2position.FirstOrDefault(x => Enumerable.SequenceEqual(x.Value, Convert_coordinates.remapLatLng(loadexcel.aItem.coord))).Key;
            loadexcel._oldGameObjecct = myKey;
        }

        public Riserva GetCenterItem()
        {
           // Debug.Log("getCenterItem");
            int size = this.transform.childCount;
            Debug.Log("size " + size);
            GameObject obj = this.transform.GetChild(size - 1).gameObject;
            //Debug.Log(obj.GetComponentInChildren<Text>().text);
            foreach (Riserva r in loadexcel.riservaDatabase)
            {
                Debug.Log("obj " + obj.GetComponentInChildren<Text>().text);
                if(r.name== obj.GetComponentInChildren<Text>().text)
                {
                    loadexcel.aItem = r;
                    //Debug.Log(r.name);
                    return r;
                }
                else
                {
                    Debug.Log("Center not found");
                }
            }
            return null;
        }
        /// <summary>
        /// Used for carry the data of value type to avoid boxing/unboxing
        /// </summary>
        public class DataWrapper
        {
            public Riserva data;
        }

        
    }
}
