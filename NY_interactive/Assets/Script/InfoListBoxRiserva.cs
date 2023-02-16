using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
    public class InfoListBoxRiserva : ListBox
    {
        [SerializeField]
        private Text _text;

        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableInfoListBankRiserva.DataWrapper) content;
            _text.text = dataWrapper.data;
        }
    }
}
