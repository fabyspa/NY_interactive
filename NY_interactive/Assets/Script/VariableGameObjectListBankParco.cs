using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


namespace AirFishLab.ScrollingList
{
    public class VariableGameObjectListBankParco : BaseListBank
    {

        LoadExcelParchi loadexcel;
        private List<Parco> _contentsList = new List<Parco>();
        public Parco[] _contents;
        [SerializeField]
        private CircularScrollingListRiserva _circularList;
        private readonly DataWrapper _dataWrapper = new DataWrapper();
        private Image _image;

        public void Start()
        {
            if (GetCenterItem() != null)
                loadexcel.ChangeStateTo(loadexcel.coord2position.FirstOrDefault(x => Enumerable.SequenceEqual(x.Value, Convert_coordinates.remapLatLng(GetCenterItem().coord))).Key, "selected");
        }

        public override object GetListContent(int index)
        {
            _dataWrapper.data = _contents[index];
            return _dataWrapper;
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }

        public void ChangeInfoContents()
        {
            loadexcel = GameObject.FindObjectOfType<LoadExcelParchi>();
            _contentsList.Clear();
           
                foreach (Parco r in loadexcel.parchiDatabase)
                {
                    _contentsList.Add(r);
                }
                _contents = _contentsList.ToArray();
                _circularList.Refresh();
                GetCenterItem();
                loadexcel.InstantiatePoints(loadexcel.parchiDatabase);
            
           

            //loadexcel.AddState();
            var myKey = loadexcel.coord2position.FirstOrDefault(x => Enumerable.SequenceEqual(x.Value, Convert_coordinates.remapLatLng(loadexcel.aItem.coord))).Key;
            loadexcel._oldGameObjecct = myKey;
        }

        

        public Parco GetCenterItem()
        {
           // Debug.Log("getCenterItem");
            int size = this.transform.childCount;
            //Debug.Log("size " + size);
            GameObject obj = this.transform.GetChild(size - 1).gameObject;
            //Debug.Log(obj.GetComponentInChildren<Text>().text);
            if(loadexcel.parchiDatabase.Count!=0) 
            {
                foreach (Parco r in loadexcel.parchiDatabase)

                {
                    //Debug.Log("obj " + obj.GetComponentInChildren<Text>().text);
                    if (r.name == obj.GetComponentInChildren<Text>().text)
                    {
                        loadexcel.aItem = r;
                        //Debug.Log(r.name);
                        return r;
                    }

                }
            }
            
            return null;
        }
        /// <summary>
        /// Used for carry the data of value type to avoid boxing/unboxing
        /// </summary>
        public class DataWrapper
        {
            public Parco data;
        }

        
    }
}
