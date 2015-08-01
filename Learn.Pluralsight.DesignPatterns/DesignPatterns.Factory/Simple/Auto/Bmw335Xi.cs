using System;

namespace DesignPatterns.Factory.Simple.Auto
{
	class Bmw335Xi: IAuto
	{
		public void TurnOn()
		{
			Console.WriteLine("Turn on Bwm335Xi");
		}

		public void TurnOff()
		{
			Console.WriteLine("Turn off Bwm335Xi");
		}
	}
}