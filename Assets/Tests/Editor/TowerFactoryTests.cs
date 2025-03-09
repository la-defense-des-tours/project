using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers;

public class TowerFactoryTests
{
    private MachineGunFactory machineGunTowerFactory;
    private CanonFactory canonTowerFactory;
    private LaserFactory laserTowerFactory;

    private GameObject towerPrefab;
    private MachineGunTower machineGunTowerComponent;
    private CanonTower canonTowerComponent;
    private LaserTower laserTowerComponent;

    [SetUp]
    public void Setup()
    {
        // MachineGun Tower Setup
        machineGunTowerFactory = new GameObject().AddComponent<MachineGunFactory>();
        var machineGunTowerPrefab = new GameObject("MachineGunTowerPrefab");
        machineGunTowerComponent = machineGunTowerPrefab.AddComponent<MachineGunTower>();


        var field = typeof(MachineGunFactory).GetField("machineGunTower", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(machineGunTowerFactory, machineGunTowerComponent);

        // Canon Tower Setup
        canonTowerFactory = new GameObject().AddComponent<CanonFactory>();
        var canonTowerPrefab = new GameObject("CanonTowerPrefab");
        canonTowerComponent = canonTowerPrefab.AddComponent<CanonTower>();

        field = typeof(CanonFactory).GetField("canonTower", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(canonTowerFactory, canonTowerComponent);


        // Laser Tower Setup
        laserTowerFactory = new GameObject().AddComponent<LaserFactory>();
        var laserTowerPrefab = new GameObject("LaserTowerPrefab");
        laserTowerComponent = laserTowerPrefab.AddComponent<LaserTower>();



        field = typeof(LaserFactory).GetField("laserTower", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(laserTowerFactory, laserTowerComponent);

    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(machineGunTowerFactory.gameObject);
        Object.DestroyImmediate(towerPrefab);
    }

    [Test]
    public void MachineGunFactory_ReturnsMachineGunTower()
    {
        Vector3 position = new(0, 0, 0);
        Tower tower = machineGunTowerFactory.CreateTower(position);

        Assert.IsNotNull(tower);
        Assert.IsTrue(tower is MachineGunTower);
    }

    [Test]
    public void CanonFactory_ReturnsCanonTower()
    {
        Vector3 position = new(0, 0, 0);
        Tower tower = canonTowerFactory.CreateTower(position);

        Assert.IsNotNull(tower);
        Assert.IsTrue(tower is CanonTower);
    }

    [Test]
    public void LaserFactory_ReturnsLaserTower()
    {
        Vector3 position = new(0, 0, 0);
        Tower tower = laserTowerFactory.CreateTower(position);

        Assert.IsNotNull(tower);
        Assert.IsTrue(tower is LaserTower);
    }



    [Test]
    public void Notify_MachineGunTower()
    {
        string expectedMessage = "MachineGun Tower Created";

        LogAssert.Expect(LogType.Log, expectedMessage);
        machineGunTowerFactory.Notify();
    }

    [Test]
    public void Notify_CanonTower()
    {
        string expectedMessage = "Canon Tower Created";

        LogAssert.Expect(LogType.Log, expectedMessage);
        canonTowerFactory.Notify();
    }

    [Test]
    public void Notify_LaserTower()
    {
        string expectedMessage = "Laser Tower Created";

        LogAssert.Expect(LogType.Log, expectedMessage);
        laserTowerFactory.Notify();
    }

}