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

	private float health;

    private void Start() {
        instance = this;
		health = maxHealth;
		InventoryCanvas.updateText();
    }

	// Health functions

	public void addHealth(float newHealth) {
		health = Math.Min(health + newHealth, maxHealth);
		InventoryCanvas.updateText();
	}

	public void removeHealth(float damage) {
		health = Math.Max(health - damage, 0);
		InventoryCanvas.updateText();
	}

	public void setHealth(float newHealth) {
		health = newHealth; // Allowing overriding max health
		InventoryCanvas.updateText();
	}

	public float getHealth() {
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
		InventoryCanvas.updateText();
	}

	public void addWood(int amount) {
		wood += amount;
		InventoryCanvas.updateText();
	}

	public void setWood(int value) {
		wood = value;
		InventoryCanvas.updateText();
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
		InventoryCanvas.updateText();
	}

	public void addFluff(int amount) {
		fluff += amount;
		InventoryCanvas.updateText();
	}

	public void setFluff(int value) {
		fluff = value;
		InventoryCanvas.updateText();
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
		InventoryCanvas.updateText();
	}

	public void addCandy(int amount) {
		candy += amount;
		InventoryCanvas.updateText();
	}

	public void setCandy(int value) {
		candy = value;
		InventoryCanvas.updateText();
	}

	public int getCandy() {
		return candy;
	}

}
