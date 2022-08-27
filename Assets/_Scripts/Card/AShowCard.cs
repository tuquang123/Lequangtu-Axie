using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShowCard : MonoBehaviour
{
    [SerializeField] private Transform transform;
    [SerializeField] private ACard prefabsCard;
    [SerializeField] private List<ADefineCard> listDefineCards;

    private void Start()
    {
        ShowCard();
    }

    private void ShowCard()
    {
        for (int i = 0; i < listDefineCards.Count; i++)
        {
            var instatePrefabs = Instantiate(prefabsCard, transform);
            instatePrefabs.GetName(listDefineCards[i].name);
            instatePrefabs.GetInt(listDefineCards[i].id);
            instatePrefabs.GetDescription(listDefineCards[i].description);
            instatePrefabs.GetSprite(listDefineCards[i].sprite);
        }
    }
}
