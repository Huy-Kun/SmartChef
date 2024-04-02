using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

public class ContainerCounterVisual : BaseMono
{
    private const string OPEN_CLOSE = "OpenClose";
    
    [SerializeField] private ContainerCounter containCounter;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containCounter.OnPlayerGrabbedObject += ContainCounterOnOnPlayerGrabbedObject;   
    }

    private void ContainCounterOnOnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
