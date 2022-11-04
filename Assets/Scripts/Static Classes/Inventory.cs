using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour {

    public static Inventory instance;
	public int maxHealth;

    private int wood = 0;
    private int fluff = 0;
    private int candy = 0;

	private int health;

    private void Start() {
        instance = this;
		health = maxHealth;
    }

	// Health functions

	public void addHealth(int newHealth) {
		health = Math.Min(health + newHealth, maxHealth);
		InventoryCanvas.instance.updateText();
	}

	public void removeHealth(int damage) {
		health = Math.Max(health - damage, 0);
		InventoryCanvas.instance.updateText();
	}

	public void setHealth(int newHealth) {
		health = newHealth; // Allowing overriding max health
		InventoryCanvas.instance.updateText();
	}

	public int getHealth() {
		return health;
    }

	public bool isDead() {
		return health == 0;
	}

	// Wood functions
	public bool hasWood(int amount) {
		return wood >= amount;
	}

	public void removeWood(int amount) {
		wood -= amount;
		InventoryCanvas.instance.updateText();
	}

	public void addWood(int amount) {
		wood += amount;
		InventoryCanvas.instance.updateText();
	}

	public void setWood(int value) {
		wood = value;
		InventoryCanvas.instance.updateText();
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
		InventoryCanvas.instance.updateText();
	}

	public void addFluff(int amount) {
		fluff += amount;
		InventoryCanvas.instance.updateText();
	}

	public void setFluff(int value) {
		fluff = value;
		InventoryCanvas.instance.updateText();
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
		InventoryCanvas.instance.updateText();
	}

	public void addCandy(int amount) {
		candy += amount;
		InventoryCanvas.instance.updateText();
	}

	public void setCandy(int value) {
		candy = value;
		InventoryCanvas.instance.updateText();
	}

	public int getCandy() {
		return candy;
	}

}
