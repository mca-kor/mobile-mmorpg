using MobileMMORPG.Prototype;
using NUnit.Framework;
using UnityEngine;

public sealed class PrototypeTouchStateTests
{
    private PrototypeTouchState state;
    private readonly Rect safeArea = new Rect(80f, 30f, 1100f, 650f);

    private static void AssertVector(Vector2 actual, Vector2 expected)
        => Assert.That(Vector2.Distance(actual, expected), Is.LessThan(0.0001f));

    [SetUp]
    public void SetUp()
    {
        state = new PrototypeTouchState();
        state.Configure(safeArea);
    }

    [Test]
    public void TwoFingersMoveAndLookIndependently()
    {
        state.Begin(5, safeArea.center + Vector2.right * 100f);
        state.Begin(9, state.MoveCenter);
        state.Move(9, state.MoveCenter + Vector2.up * state.Radius);
        state.Move(5, safeArea.center + new Vector2(130f, 20f));
        AssertVector(state.MoveAxis, Vector2.up);
        Assert.That(state.LookDelta, Is.EqualTo(new Vector2(30f, 20f)));
        Assert.That(state.Looking, Is.True);
    }

    [Test]
    public void ReleasingMoveFingerKeepsLookFinger()
    {
        state.Begin(1, state.MoveCenter);
        state.Begin(2, safeArea.center + Vector2.right * 100f);
        state.Move(1, state.MoveCenter + Vector2.right * state.Radius);
        state.End(1);
        state.Move(2, safeArea.center + Vector2.right * 120f);
        Assert.That(state.MoveAxis, Is.EqualTo(Vector2.zero));
        Assert.That(state.LookDelta.x, Is.EqualTo(20f));
        Assert.That(state.Looking, Is.True);
    }

    [Test]
    public void ReleasingLookFingerKeepsMovement()
    {
        state.Begin(1, state.MoveCenter);
        state.Begin(2, safeArea.center + Vector2.right * 100f);
        state.Move(1, state.MoveCenter + Vector2.up * state.Radius);
        state.End(2);
        AssertVector(state.MoveAxis, Vector2.up);
        Assert.That(state.Looking, Is.False);
        Assert.That(state.LookDelta, Is.EqualTo(Vector2.zero));
    }

    [Test]
    public void ThirdFingerCannotStealOrReleaseExistingGestures()
    {
        state.Begin(1, state.MoveCenter);
        state.Begin(2, safeArea.center + Vector2.right * 100f);
        state.Begin(3, state.MoveCenter);
        state.Move(3, state.MoveCenter + Vector2.up * state.Radius);
        state.End(3);
        Assert.That(state.MoveAxis, Is.EqualTo(Vector2.zero));
        Assert.That(state.Looking, Is.True);
        state.Move(1, state.MoveCenter + Vector2.up * state.Radius);
        AssertVector(state.MoveAxis, Vector2.up);
    }

    [Test]
    public void CrossingScreenMidpointDoesNotChangeFingerRole()
    {
        Vector2 start = safeArea.center + Vector2.right * 100f;
        state.Begin(1, start);
        state.Move(1, state.MoveCenter);
        Assert.That(state.LookDelta, Is.EqualTo(state.MoveCenter - start));
        Assert.That(state.MoveAxis, Is.EqualTo(Vector2.zero));
    }

    [Test]
    public void JoystickIsClampedToUnitCircle()
    {
        state.Begin(1, state.MoveCenter);
        state.Move(1, state.MoveCenter + Vector2.one * state.Radius * 5f);
        Assert.That(state.MoveAxis.magnitude, Is.EqualTo(1f).Within(0.0001f));
        Assert.That(state.MoveAxis.x, Is.EqualTo(state.MoveAxis.y).Within(0.0001f));
    }

    [Test]
    public void SafeAreaAndJoystickStartBoundsRejectUnrelatedTouches()
    {
        state.Begin(1, new Vector2(safeArea.xMax + 5f, safeArea.center.y));
        state.Begin(2, new Vector2(safeArea.xMin + 10f, safeArea.yMax - 10f));
        state.Move(2, state.MoveCenter);
        Assert.That(state.Looking, Is.False);
        Assert.That(state.MoveAxis, Is.EqualTo(Vector2.zero));
    }

    [Test]
    public void ResetStopsGesturesAndHeldFingerMustBeginAgain()
    {
        state.Begin(1, state.MoveCenter);
        state.Begin(2, safeArea.center + Vector2.right * 100f);
        state.Reset();
        state.Move(1, state.MoveCenter + Vector2.up * state.Radius);
        state.Move(2, safeArea.center + Vector2.right * 150f);
        Assert.That(state.MoveAxis, Is.EqualTo(Vector2.zero));
        Assert.That(state.LookDelta, Is.EqualTo(Vector2.zero));
        Assert.That(state.Looking, Is.False);
    }

    [Test]
    public void NewLookHasNoJumpAndNextFrameHasNoRepeatedDelta()
    {
        Vector2 start = safeArea.center + Vector2.right * 100f;
        state.Begin(1, start);
        Assert.That(state.LookDelta, Is.EqualTo(Vector2.zero));
        state.Move(1, start + new Vector2(10f, 20f));
        state.BeginFrame();
        state.Move(1, start + new Vector2(10f, 20f));
        Assert.That(state.LookDelta, Is.EqualTo(Vector2.zero));
    }

    [Test]
    public void MissingFingerIsReleasedWithoutStoppingOtherFinger()
    {
        state.Begin(1, state.MoveCenter);
        Vector2 start = safeArea.center + Vector2.right * 100f;
        state.Begin(2, start);
        state.Move(1, state.MoveCenter + Vector2.up * state.Radius);
        state.ReleaseMissing(false, true);
        state.Move(2, start + Vector2.right * 10f);
        Assert.That(state.MoveAxis, Is.EqualTo(Vector2.zero));
        Assert.That(state.Looking, Is.True);
        Assert.That(state.LookDelta.x, Is.EqualTo(10f));
        state.ReleaseMissing(false, false);
        Assert.That(state.Looking, Is.False);
        Assert.That(state.LookDelta, Is.EqualTo(Vector2.zero));
    }

    [TestCase(100f, 20f, 2200f, 1000f)]
    [TestCase(0f, 100f, 1000f, 2100f)]
    public void LayoutChangeResetsInputAndKeepsJoystickInsideSafeArea(float x, float y, float width, float height)
    {
        state.Begin(1, state.MoveCenter);
        state.Configure(new Rect(x, y, width, height));
        Assert.That(state.MoveAxis, Is.EqualTo(Vector2.zero));
        Assert.That(state.MoveCenter.x - state.Radius, Is.GreaterThanOrEqualTo(x));
        Assert.That(state.MoveCenter.x + state.Radius, Is.LessThan(x + width * 0.5f));
        Assert.That(state.MoveCenter.y - state.Radius, Is.GreaterThanOrEqualTo(y));
        Assert.That(state.MoveCenter.y + state.Radius, Is.LessThan(y + height));
    }
}
