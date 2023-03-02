using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
    public class GameObjectListBoxRiserva : ListBox
    {
        [SerializeField]
        private Text _name, _descr,_nameENG, _descrENG;
        [SerializeField]
        private Image _image;
        
        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableGameObjectListBankRiserva.DataWrapper) content;
            _name.text = dataWrapper.data.name;
            _descr.text = dataWrapper.data.descr;
            _image.sprite = dataWrapper.data.sprite;
            _nameENG.text = dataWrapper.data.name_eng;
            _descrENG.text = dataWrapper.data.descr_eng;
            
        }
       
       
    }
    
}
