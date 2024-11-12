using UnityEngine;

namespace MenuScene
{
    public class DictionaryPanel : MonoBehaviour
    {
        [SerializeField] GameObject materialDictionary;
        [SerializeField] GameObject recipeDictionary;
        private void OnEnable()
        {
            SetMaterialDictionaryActive();
        }

        public void SetMaterialDictionaryActive()
        {
            materialDictionary.SetActive(true);
            recipeDictionary.SetActive(false);
        }

        public void SetRecipeDictionaryActive()
        {
            recipeDictionary.SetActive(true);
            materialDictionary.SetActive(false);
        }
    }

}
