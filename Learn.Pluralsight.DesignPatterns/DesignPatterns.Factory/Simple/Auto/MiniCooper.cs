using System;

namespace DesignPatterns.Factory.Simple.Auto
{
	class MiniCooper : IAuto
	{
		public void TurnOn()
		{
			Console.WriteLine("Turn on Minicooper");
		}

		public void TurnOff()
		{
			Console.WriteLine("Turn off Minicooper");
		}
	}
}