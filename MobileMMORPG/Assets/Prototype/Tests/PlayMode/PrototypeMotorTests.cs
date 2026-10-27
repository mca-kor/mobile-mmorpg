using System.Collections;
using MobileMMORPG.Prototype;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public sealed class PrototypeMotorTests
{
    private GameObject player;
    private GameObject ground;
    private GameObject wall;
    private GameObject view;
    private PrototypeMotor motor;

    [SetUp]
    public void SetUp()
    {
        ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.transform.position = new Vector3(0f, -0.5f, 0f);
        ground.transform.localScale = new Vector3(50f, 1f, 50f);
        player = new GameObject("Test player");
        CharacterController body = player.AddComponent<CharacterController>();
        body.height = 2f;
        body.radius = 0.45f;
        body.center = Vector3.up;
        motor = player.AddComponent<PrototypeMotor>();
        motor.enabled = false; // 테스트에서는 실제 키보드 입력 없이 직접 이동을 진행한다.
        Physics.SyncTransforms();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(player);
        Object.Destroy(ground);
        if (wall != null) Object.Destroy(wall);
        if (view != null) Object.Destroy(view);
        yield return null;
    }

    [Test]
    public void DiagonalTravelMatchesStraightTravel()
    {
        motor.Step(Vector2.up, 0.1f);
        float straight = new Vector2(player.transform.position.x, player.transform.position.z).magnitude;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = Vector3.zero;
        player.GetComponent<CharacterController>().enabled = true;
        Physics.SyncTransforms();
        motor.Step(Vector2.one, 0.1f);
        float diagonal = new Vector2(player.transform.position.x, player.transform.position.z).magnitude;
        Assert.That(straight, Is.GreaterThan(0.3f));
        Assert.That(diagonal, Is.EqualTo(straight).Within(0.02f));
    }

    [Test]
    public void ForwardFollowsCameraYawAndReleasingInputStopsPlanarMovement()
    {
        view = new GameObject("View");
        view.transform.rotation = Quaternion.Euler(45f, 90f, 0f);
        motor.SetView(view.transform);
        motor.Step(Vector2.up, 0.1f);
        Assert.That(player.transform.position.x, Is.GreaterThan(0.3f));
        Assert.That(Mathf.Abs(player.transform.position.z), Is.LessThan(0.02f));
        Vector3 before = player.transform.position;
        motor.Step(Vector2.zero, 0.1f);
        Assert.That(player.transform.position.x, Is.EqualTo(before.x).Within(0.001f));
        Assert.That(player.transform.position.z, Is.EqualTo(before.z).Within(0.001f));
    }

    [Test]
    public void CharacterCannotWalkThroughWallOrFallThroughGround()
    {
        wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = new Vector3(0f, 1f, 2f);
        wall.transform.localScale = new Vector3(6f, 2f, 1f);
        Physics.SyncTransforms();
        for (int i = 0; i < 120; i++) motor.Step(Vector2.up, 1f / 60f);
        Assert.That(player.transform.position.z, Is.GreaterThan(0.8f));
        Assert.That(player.transform.position.z, Is.LessThan(1.2f));
        Assert.That(player.transform.position.y, Is.GreaterThan(-0.1f));
        Assert.That(player.GetComponent<CharacterController>().isGrounded, Is.True);
    }

    [UnityTest]
    public IEnumerator CameraFollowsPlayerWithoutChangingOrbit()
    {
        view = new GameObject("Follow camera");
        PrototypeCamera orbit = view.AddComponent<PrototypeCamera>();
        orbit.SetTarget(player.transform);
        Vector3 offset = view.transform.position - player.transform.position;
        Quaternion rotation = view.transform.rotation;
        motor.Step(Vector2.up, 0.1f);
        yield return null;
        yield return null;
        Assert.That(Vector3.Distance(view.transform.position - player.transform.position, offset), Is.LessThan(0.001f));
        Assert.That(Quaternion.Angle(view.transform.rotation, rotation), Is.LessThan(0.001f));
    }
}
