using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image bar;

    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounterOnOnProgressChanged;

        bar.fillAmount = 0;
        Hide();
    }

    private void CuttingCounterOnOnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)

    {
        bar.fillAmount = e.cuttingProgress;
        
        if(e.cuttingProgress == 0 || e.cuttingProgress ==1)
            Hide();
        else Show();
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
