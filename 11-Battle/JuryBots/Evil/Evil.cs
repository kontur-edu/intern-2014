﻿using System.Drawing;
using Robocode;

namespace JuryBots
{
	/// <summary>
	///   Based on Target - a sample robot by Mathew Nelson, and maintained by Flemming N. Larsen
	///   <p />
	///   Sits still.  Moves every time energy drops by 20.
	///   This Robot demonstrates custom events.
	/// </summary>
	public class Evil : AdvancedRobot
	{
		private int trigger;

		public override void Run()
		{
			// Set colors
			BodyColor = (Color.Red);
			GunColor = (Color.DarkRed);
			RadarColor = (Color.LightPink);

			// Initially, we'll move when life hits 80
			trigger = 80;
			// Add a custom event named "trigger hit",
			AddCustomEvent(new Condition("triggerhit", c => Energy <= trigger));
		}

		/// <summary>
		///   onCustomEvent handler
		/// </summary>
		public override void OnCustomEvent(CustomEvent e)
		{
			// If our custom event "triggerhit" went off,
			if (e.Condition.Name == "triggerhit")
			{
				// Adjust the trigger value, or
				// else the event will fire again and again and again...
				trigger -= 20;
				Out.WriteLine("Ouch, down to " + (int) (Energy + .5) + " energy.");
				// move around a bit.
				TurnLeft(65);
				Ahead(100);
			}
		}
	}
}