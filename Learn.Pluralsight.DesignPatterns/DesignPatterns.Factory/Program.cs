using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Factory.Simple;
using DesignPatterns.Factory.Simple.Auto;

namespace DesignPatterns.Factory
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string carName = "hyundai";

			RunSimpleFactory(carName);
		}

		private static void RunSimpleFactory(string carName)
		{
			AutoFactory factory = new AutoFactory();
			IAuto car = factory.CreateInstance(carName);

			car.TurnOn();
			car.TurnOff();
		}
	}
}
