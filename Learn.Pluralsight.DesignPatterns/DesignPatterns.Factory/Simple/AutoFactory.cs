using System;
using System.Collections.Generic;
using System.Reflection;
using DesignPatterns.Factory.Simple.Auto;

namespace DesignPatterns.Factory.Simple
{
	internal class AutoFactory
	{
		private Dictionary<string, Type> _autos;

		public AutoFactory()
		{
			LoadTypesICanReturn();
		}

		public IAuto CreateInstance(string carName)
		{
			Type t = GetTypeToCreate(carName);
			if (t == null)
				return new NullAuto();

			return Activator.CreateInstance(t) as IAuto;
		}

		private Type GetTypeToCreate(string carName)
		{
			foreach (var auto in _autos)
			{
				if (auto.Key.Contains(carName))
					return _autos[auto.Key];
			}
			return null;
		}

		private void LoadTypesICanReturn()
		{
			_autos = new Dictionary<string, Type>();
			var typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

			foreach (Type type in typesInThisAssembly)
			{
				if (type.GetInterface(typeof (IAuto).ToString()) != null)
					_autos.Add(type.Name.ToLower(), type);
			}
		}
	}
}