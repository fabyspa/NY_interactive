using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
    public class VariableStringListBankRiserva : BaseListBank
    {

        LoadExcel loadexcel;
        //[SerializeField]
        //private InputField _contentInputField;
        private List<string> _contentsList = new List<string>();
        public string[] _contents;
        [SerializeField]
        private CircularScrollingList _circularList;
       // [SerializeField]
       // private CircularScrollingList _linearList;

        private readonly DataWrapper _dataWrapper = new DataWrapper();

        /// <summary>
        /// Extract the contents from the input field and refresh the list
        /// </summary>
        public void ChangeContents()
        {
           
           // Debug.Log(_circularList.GetCenteredBox().name);
            loadexcel = GameObject.FindObjectOfType<LoadExcel>();
            _contentsList.Add("Tutte");
            foreach ( string t in loadexcel.type)
            {

                _contentsList.Add(t);
               //_contentInputField.text.Split(
               //    new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            _contents = _contentsList.ToArray();
            _circularList.Refresh();
            //_linearList.Refresh();
        }

        public override object GetListContent(int index)
        {
            
            _dataWrapper.data = _contents[index];
            //Debug.Log(_dataWrapper.data);
            return _dataWrapper;
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }

        /// <summary>
        /// Used for carry the data of value type to avoid boxing/unboxing
        /// </summary>
        public class DataWrapper
        {
            public string data;
        }
    }
}
