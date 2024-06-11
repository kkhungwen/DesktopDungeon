using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstantiatedObjectInspecControl : MonoBehaviour
{
    public event Action<bool> OnToggleOpen;

    [SerializeField] private PlayerClickInput playerClickInput;
    [SerializeField] private Vector2 offset;
    private IInspecable inspecable;
    private IInspec mouseOverInspec;
    private IInspec clickInspec;

    private bool ableMouseOverInspec = false;

    private void Awake()
    {
        playerClickInput.OnLeftClick += PlayerClickInput_OnLeftClick;
        playerClickInput.OnStartLeftDrag += PlayerClickInput_OnStartLeftDrag;
        playerClickInput.OnLeftMouseUp += PlayerClickInput_OnLeftMouseUp;
        playerClickInput.OnEnter += PlayerClickInput_OnEnter;
        playerClickInput.OnExit += PlayerClickInput_OnExit;
        playerClickInput.OnOver += PlayerClickInput_OnOver;
    }

    private void PlayerClickInput_OnLeftClick(Vector2 obj)
    {
        if (clickInspec == null)
        {
            if (inspecable.CreateClickInspec(this.transform, offset, out clickInspec))
                OnToggleOpen?.Invoke(true);
        }
        else
        {
            clickInspec.Destroy();
            clickInspec = null;
            OnToggleOpen?.Invoke(false);
        }
    }

    private void PlayerClickInput_OnEnter()
    {
        if (mouseOverInspec == null && ableMouseOverInspec)
            inspecable.CreatMouseOverInspec(this.transform, offset, out mouseOverInspec);

        if (mouseOverInspec != null && !ableMouseOverInspec)
        {
            mouseOverInspec.Destroy();
            mouseOverInspec = null;
        }
    }

    private void PlayerClickInput_OnOver()
    {
        if (mouseOverInspec != null && !ableMouseOverInspec)
        {
            mouseOverInspec.Destroy();
            mouseOverInspec = null;
        }
    }

    private void PlayerClickInput_OnExit()
    {
        if (mouseOverInspec == null)
            return;

        mouseOverInspec.Destroy();
        mouseOverInspec = null;
    }

    private void PlayerClickInput_OnLeftMouseUp(Vector2 obj)
    {

    }

    private void PlayerClickInput_OnStartLeftDrag()
    {
        if (clickInspec != null)
            clickInspec.SetSortingOrderToTop();
    }

    public void SetUp(IInspecable inspecable)
    {
        this.inspecable = inspecable;
    }

    public void SetAbleMouseOverInpec(bool isAble)
    {
        ableMouseOverInspec = isAble;
    }
}
