using System;
using UnityEngine;
using Zenject;

public class PinManager :IDisposable
{
    private Pin[] _pins;

    private int _fallenCount;
    public int FallenCount => _fallenCount;

    public PinManager(Pin[] pins)
    {
        _pins = pins;
        _fallenCount = 0;

        SubscribeToPins();
    }

    public void Dispose()
    {
        UnsubscribeFromPins();
    }

    private void OnPinFallen(Pin pin) 
    {
        _fallenCount++;
        Debug.Log($"Knocked down by pins: {_fallenCount}");
    }

    public void ResetPins() 
    { 
        _fallenCount = 0;

        foreach (var pin in _pins) 
        { 
            pin.ResetPin();
        }
    }

    private void SubscribeToPins()
    {
        foreach (var pin in _pins)
        {
            pin.OnPinFallen += OnPinFallen;
        }
    }

    private void UnsubscribeFromPins()
    {
        foreach (var pin in _pins)
        {
            pin.OnPinFallen -= OnPinFallen;
        }
    }
}
