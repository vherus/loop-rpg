using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : UIComponent
{
    public ViewData viewData;

    public GameObject containerTop;
    public GameObject containerCenter;
    public GameObject containerBottom;

    private Image imageTop;
    private Image imageCenter;
    private Image imageBottom;

    private VerticalLayoutGroup vLayoutGroup;

    protected override void Configure()
    {
        vLayoutGroup.padding = viewData.padding;
        vLayoutGroup.spacing = viewData.spacing;

        imageTop.color = viewData.theme.primary_bg;
        imageCenter.color = viewData.theme.secondary_bg;
        imageBottom.color = viewData.theme.tertiary_bg;
    }

    protected override void Setup()
    {
        vLayoutGroup = GetComponent<VerticalLayoutGroup>();
        imageTop = containerTop.GetComponent<Image>();
        imageCenter = containerCenter.GetComponent<Image>();
        imageBottom = containerBottom.GetComponent<Image>();
    }
}
