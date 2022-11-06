using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour {

    public static Inventory instance;
	private void Awake() {
		instance = this;
		Setup();
	}

	public Action onInventoryUpdated;

	[Header("Starting values")]
	public int maxHealth;
	public int startWood;
	public int startFluff;
	public int startCandy;

	private float health;
	public int BuildLevel { get; set; }

    private int wood = 0;
    private int fluff = 0;
    private int candy = 0;


    private void Setup() {
		health = maxHealth;
		addWood(startWood);
		addFluff(startFluff);
		addCandy(startCandy);
		onInventoryUpdated?.Invoke();
    }

	// Health functions

	public void addHealth(float newHealth) {
		health = Math.Min(health + newHealth, maxHealth);
		onInventoryUpdated?.Invoke();
	}

	public void removeHealth(float damage) {
		health = Math.Max(health - damage, 0);
		onInventoryUpdated?.Invoke();
	}

	public void setHealth(float newHealth) {
		health = newHealth; // Allowing overriding max health
		onInventoryUpdated?.Invoke();
	}

	public float getHealth() {
		return health;
    }

	public void heal() {
		health = maxHealth;
	}

	public bool isDead() {
		return health == 0;
	}


	#region Resources

	public bool CanAfford(Building build) {
		return hasWood(build.woodCost) && hasFluff(build.fluffCost) && hasCandy(build.candyCost);
	}

	public void Purchase(Building build) {
		removeWood(build.woodCost);
		removeFluff(build.fluffCost);
		removeCandy(build.candyCost);

		onInventoryUpdated?.Invoke();
	}

	// Wood functions
	public bool hasWood(int amount) {
		return wood >= amount;
	}

	public void removeWood(int amount) {
		wood -= amount;
		onInventoryUpdated?.Invoke();
	}

	public void addWood(int amount) {
		wood += amount;
		onInventoryUpdated?.Invoke();
	}

	public void setWood(int value) {
		wood = value;
		onInventoryUpdated?.Invoke();
	}

	public int getWood() {
		return wood;
    }


	// Fluff functions
	public bool hasFluff(int amount) {
		return fluff >= amount;
	}

	public void removeFluff(int amount) {
		fluff -= amount;
		onInventoryUpdated?.Invoke();
	}

	public void addFluff(int amount) {
		fluff += amount;
		onInventoryUpdated?.Invoke();
	}

	public void setFluff(int value) {
		fluff = value;
		onInventoryUpdated?.Invoke();
	}

	public int getFluff() {
		return fluff;
    }

	// Candy functions
	public bool hasCandy(int amount) {
		return candy >= amount;
	}

	public void removeCandy(int amount) {
		candy -= amount;
		onInventoryUpdated?.Invoke();
	}

	public void addCandy(int amount) {
		candy += amount;
		onInventoryUpdated?.Invoke();
	}

	public void setCandy(int value) {
		candy = value;
		onInventoryUpdated?.Invoke();
	}

	public int getCandy() {
		return candy;
	}

	#endregion

}
