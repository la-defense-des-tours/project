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

        var machineGunTowerUpgradePrefab = new GameObject("MachineGunTowerUpgradePrefab");
        var machineGunTowerUpgradeComponent = machineGunTowerUpgradePrefab.AddComponent<MachineGunTower>();

        var machineGunTowerUpgrade2Prefab = new GameObject("MachineGunTowerUpgrade2Prefab");
        var machineGunTowerUpgrade2Component = machineGunTowerUpgrade2Prefab.AddComponent<MachineGunTower>();

        var field = typeof(MachineGunFactory).GetField("machineGunTower", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(machineGunTowerFactory, machineGunTowerComponent);

        field = typeof(MachineGunFactory).GetField("machineGunTowerUpgrade", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(machineGunTowerFactory, machineGunTowerUpgradeComponent);

        field = typeof(MachineGunFactory).GetField("machineGunTowerUpgrade2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(machineGunTowerFactory, machineGunTowerUpgrade2Component);

        // Canon Tower Setup
        canonTowerFactory = new GameObject().AddComponent<CanonFactory>();
        var canonTowerPrefab = new GameObject("CanonTowerPrefab");
        canonTowerComponent = canonTowerPrefab.AddComponent<CanonTower>();

        var canonTowerUpgradePrefab = new GameObject("CanonTowerUpgradePrefab");
        var canonTowerUpgradeComponent = canonTowerUpgradePrefab.AddComponent<CanonTower>();

        var canonTowerUpgrade2Prefab = new GameObject("CanonTowerUpgrade2Prefab");
        var canonTowerUpgrade2Component = canonTowerUpgrade2Prefab.AddComponent<CanonTower>();

        field = typeof(CanonFactory).GetField("canonTower", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(canonTowerFactory, canonTowerComponent);

        field = typeof(CanonFactory).GetField("canonTowerUpgrade", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(canonTowerFactory, canonTowerUpgradeComponent);

        field = typeof(CanonFactory).GetField("canonTowerUpgrade2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(canonTowerFactory, canonTowerUpgrade2Component);

        // Laser Tower Setup
        laserTowerFactory = new GameObject().AddComponent<LaserFactory>();
        var laserTowerPrefab = new GameObject("LaserTowerPrefab");
        laserTowerComponent = laserTowerPrefab.AddComponent<LaserTower>();

        var laserTowerUpgradePrefab = new GameObject("LaserTowerUpgradePrefab");
        var laserTowerUpgradeComponent = laserTowerUpgradePrefab.AddComponent<LaserTower>();

        var laserTowerUpgrade2Prefab = new GameObject("LaserTowerUpgrade2Prefab");
        var laserTowerUpgrade2Component = laserTowerUpgrade2Prefab.AddComponent<LaserTower>();

        field = typeof(LaserFactory).GetField("laserTower", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(laserTowerFactory, laserTowerComponent);

        field = typeof(LaserFactory).GetField("laserTowerUpgrade", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(laserTowerFactory, laserTowerUpgradeComponent);

        field = typeof(LaserFactory).GetField("laserTowerUpgrade2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(laserTowerFactory, laserTowerUpgrade2Component);
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
    public void MachineGunFactory_UpgradeTower_ReturnsUpgradedMachineGunTower()
    {
        Vector3 position = new(0, 0, 0);

        Tower upgradedTower1 = machineGunTowerFactory.UpgradeTower(position, 1, null);
        Tower upgradedTower2 = machineGunTowerFactory.UpgradeTower(position, 2, upgradedTower1);
        Tower nullTower = machineGunTowerFactory.UpgradeTower(position, 3, upgradedTower2);

        Assert.IsNotNull(upgradedTower1);
        Assert.IsTrue(upgradedTower1 is MachineGunTower);
        Assert.IsNotNull(upgradedTower2);
        Assert.IsTrue(upgradedTower2 is MachineGunTower);
        Assert.IsNull(nullTower);
        LogAssert.Expect(LogType.Error, "Max upgrade level reached!");
    }

    [Test]
    public void CanonFactory_UpgradeTower_ReturnsUpgradedCanonTower()
    {
        Vector3 position = new(0, 0, 0);

        Tower upgradedTower1 = canonTowerFactory.UpgradeTower(position, 1, null);
        Tower upgradedTower2 = canonTowerFactory.UpgradeTower(position, 2, upgradedTower1);
        Tower nullTower = canonTowerFactory.UpgradeTower(position, 3, upgradedTower2);

        Assert.IsNotNull(upgradedTower1);
        Assert.IsTrue(upgradedTower1 is CanonTower);
        Assert.IsNotNull(upgradedTower2);
        Assert.IsTrue(upgradedTower2 is CanonTower);
        Assert.IsNull(nullTower);
        LogAssert.Expect(LogType.Error, "Max upgrade level reached!");
    }

    [Test]
    public void LaserFactory_UpgradeTower_ReturnsUpgradedLaserTower()
    {
        Vector3 position = new(0, 0, 0);

        Tower upgradedTower1 = laserTowerFactory.UpgradeTower(position, 1, null);
        Tower upgradedTower2 = laserTowerFactory.UpgradeTower(position, 2, upgradedTower1);
        Tower nullTower = laserTowerFactory.UpgradeTower(position, 3, upgradedTower2);

        Assert.IsNotNull(upgradedTower1);
        Assert.IsTrue(upgradedTower1 is LaserTower);
        Assert.IsNotNull(upgradedTower2);
        Assert.IsTrue(upgradedTower2 is LaserTower);
        Assert.IsNull(nullTower);
        LogAssert.Expect(LogType.Error, "Max upgrade level reached!");
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