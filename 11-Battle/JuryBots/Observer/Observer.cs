using System.Drawing;
using Robocode;

namespace JuryBots
{
	public class Observer : Robot
	{
		public override void Run()
		{
			BodyColor = (Color.Yellow);
			GunColor = (Color.Yellow);
			RadarColor = (Color.Yellow);
		}
	}
}