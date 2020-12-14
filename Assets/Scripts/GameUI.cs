using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUI : MonoBehaviour
{
    [Header("Knife count display")]
    [SerializeField] private GameObject PanelKnives;
    [SerializeField] private GameObject IconKnifePrefab;
    [SerializeField] private Color usedKnifeIconColor;
    public Image FadePanel;

    private int knifeIconIndexToChange = 0;

    public void SetInitialDisplayKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(IconKnifePrefab, PanelKnives.transform);
        }
    }

    public void DecrementDisplayKnifeCount()
    {
        PanelKnives.transform.GetChild(knifeIconIndexToChange++).GetComponent<Image>().color = usedKnifeIconColor;
    }

    public void SceneInDarkness() => FadePanel.DOFade(255f, 1.3f).From(0f).OnComplete(() => { GameManager.Instance.RestartLevel(); });
    public void SceneFromDarkness() => FadePanel.DOFade(0f, 1.3f).From(255f);
}
