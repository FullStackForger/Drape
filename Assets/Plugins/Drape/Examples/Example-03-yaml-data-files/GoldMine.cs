﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Drape.Interfaces;
using Drape.YAML;

namespace Drape.Eamples.YamlDataFiles
{
	public class GoldMine : MonoBehaviour
	{
		public Text goldLabel;
		public Text outputLabel;
		public Text capacityLabel;

		private Resource _gold;
		private Stat _goldCapacity;
		private Stat _goldOutput;
		private bool _goldCapacityLimit = false;
		private bool _goldOutputBoost = false;

		private Modifier _goldOutputModifier;
		private Modifier _goldCapacityModifier;
		private Registry _registry;

		void Start()
		{

			// -------------------------
			// Creating registry factory
			// -------------------------
			// Reigstry factory constructor takes list of stat installers as parameters.
			// Installer is class converting an input data (eg: json string) to stat data structures.
			// It uses them internally to create stats and install the onto newly created registry,
			// provided with Create() mehtod.
			RegistryFactory registryFactory = new RegistryFactory(new List<IInstaller>() {
				{ new YamlInstaller<Stat, StatData>((Resources.Load("example03/stats") as TextAsset).text) },
				{ new YamlInstaller<Resource, ResourceData>((Resources.Load("example03/resources") as TextAsset).text) },
				{ new YamlInstaller<Modifier, ModifierData>((Resources.Load("example03/modifiers") as TextAsset).text) },
			});

			// --------------------
			// Creating registry
			// --------------------
			// Using factory Create() method to instantiate registry of stats.
			_registry = registryFactory.Create();

			// --------------------------
			// Accessing stat references
			// --------------------------
			_gold = _registry.Get<Resource>("gold");
			_goldCapacity = _registry.Get<Stat>("gold-capacity");
			_goldCapacityModifier = _registry.Get<Modifier>("broken-storage");
			_goldOutput = _registry.Get<Stat>("gold-output");
			_goldOutputModifier = _registry.Get<Modifier>("faster-mining");
		}

		void Update()
		{
			_gold.Update(Time.deltaTime);
			goldLabel.text = _gold.Value.ToString("0.000");
			outputLabel.text = _goldOutput.Value.ToString("0.000");
			capacityLabel.text = _goldCapacity.Value.ToString("0.000");
		}

		public void ToggleCapacity()
		{
			if (_goldCapacity.ModifierCount == 0) {
				_goldCapacity.AddModifier(_goldCapacityModifier);
			} else {
				_goldCapacity.ClearMods();
			}
		}

		public void ToggleOutput()
		{
			if (_goldOutput.ModifierCount == 0) {
				_goldOutput.AddModifier(_goldOutputModifier);
			} else {
				_goldOutput.ClearMods();
			}
		}

		public void EmptyuMine()
		{
			_gold.DisposeAll();
		}
	}
}