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
        machineGunTowerFactory = new GameObject().AddComponent<MachineGunFactory>();
        towerPrefab = new GameObject("MachineGunTowerPrefab");
        machineGunTowerComponent = towerPrefab.AddComponent<MachineGunTower>();
        var field = typeof(MachineGunFactory).GetField("machineGunTower", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(machineGunTowerFactory, machineGunTowerComponent);

        canonTowerFactory = new GameObject().AddComponent<CanonFactory>();
        towerPrefab = new GameObject("CanonTowerPrefab");
        canonTowerComponent = towerPrefab.AddComponent<CanonTower>();
        field = typeof(CanonFactory).GetField("canonTower", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(canonTowerFactory, canonTowerComponent);

        laserTowerFactory = new GameObject().AddComponent<LaserFactory>();
        towerPrefab = new GameObject("LaserTowerPrefab");
        laserTowerComponent = towerPrefab.AddComponent<LaserTower>();
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
        Tower tower = machineGunTowerFactory.CreateTower();

        Assert.IsNotNull(tower);
        Assert.IsTrue(tower is MachineGunTower);
    }

    [Test]
    public void CanonFactory_ReturnsCanonTower()
    {
        Tower tower = canonTowerFactory.CreateTower();

        Assert.IsNotNull(tower);
        Assert.IsTrue(tower is CanonTower);
    }

    [Test]
    public void LaserFactory_ReturnsLaserTower()
    {
        Tower tower = laserTowerFactory.CreateTower();

        Assert.IsNotNull(tower);
        Assert.IsTrue(tower is LaserTower);
    }

    [Test]
    public void Notify_MachineGunTower()
    {
        string expectedMessage = "Machine Gun Tower Created";

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