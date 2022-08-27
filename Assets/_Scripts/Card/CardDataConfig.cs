using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardDataConfig : ScriptableObject
{
    #region Data
    public List<KeyValue> list;

    [System.Serializable]
    public class CardData
    {
        public string name;
        public string description;
        public Sprite sprite;
    }

    [System.Serializable]
    public class KeyValue
    {
        public CardID id;
        public CardData cardData;       
    }
    #endregion//Data

    #region Public - get data
    Dictionary<CardID, CardData> _fromListTomap
        = new Dictionary<CardID, CardData>();
    Dictionary<CardID, CardData> FromListToMap
    {
        get
        {
            // not convert from list to map yet
            if (_fromListTomap.Count == 0)
            {
                for (int n = 0; n < list.Count; n++)
                {
                    var item = list[n];
                    _fromListTomap.Add(item.id, item.cardData);
                }
            }
            return _fromListTomap;
        }
    }

    public CardData GetValueFromKey(CardID id)
    {
        var result = FromListToMap[id];
        // validate data
        if (result == null)
        {
            Debug.LogError($"Not add item for ID [{id}] yet");
            return null;
        }
        return result;
    }

    #endregion //Public - get data

}