using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Learn.Pluralsight.DesignPatterns.Visitor
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Gtfs gtfs = new Gtfs();
			gtfs.Entities.Add(new Agency {Id = "1", Name="Agency Name"});
			gtfs.Entities.Add(new FareRules { RouteId = "10"});
			gtfs.Entities.Add(new Frequencies());

			RequiredValidateVisitor requiredValidateVisitor = new RequiredValidateVisitor();
			gtfs.Accept(requiredValidateVisitor);

			Console.Read();
		}
	}

	public class RequiredValidateVisitor : ValidateVisitor
	{
		private const string ERROR_MESSAGE = "Property value is required";

		public override void Visit(FareRules fareRules)
		{
			SetFailedProperties(fareRules);
		}

		public override void Visit(Frequencies frequencies)
		{
			SetFailedProperties(frequencies);
		}

		public override void Visit(Agency agency)
		{
			SetFailedProperties(agency);
		}

		private void SetFailedProperties(object obj)
		{
			var requiredProperties = GetRequiredProperties(obj.GetType());
			foreach (PropertyInfo requiredProperty in requiredProperties)
			{
				var value = requiredProperty.GetValue(obj);
				if (value == null)
					FailedProperties.Add(requiredProperty, ERROR_MESSAGE);
			}
		}

		private IEnumerable<PropertyInfo> GetRequiredProperties(Type type)
		{
			// http://stackoverflow.com/a/2282254/4035
			IEnumerable<PropertyInfo> properties = type.GetProperties().Where(
				property => Attribute.IsDefined(property, typeof(RequiredAttribute)));
			return properties;
		}
	}

	public abstract class ValidateVisitor : IVisitor
	{
		public Dictionary<PropertyInfo, string> FailedProperties { get; }
			= new Dictionary<PropertyInfo, string>();

		public abstract void Visit(Agency agency);
		public abstract void Visit(FareRules fareRules);
		public abstract void Visit(Frequencies frequencies);
	}

	public interface IVisitor
	{
		void Visit(Agency agency);
		void Visit(FareRules fareRules);
		void Visit(Frequencies frequencies);
	}

	public class Gtfs : IEntity
	{
		public List<IEntity> Entities { get; set; } = new List<IEntity>();
		public void Accept(IVisitor visitor)
		{
			foreach (IEntity entity in Entities)
			{
				entity.Accept(visitor);
			}
		}
	}

	public interface IEntity
	{
		void Accept(IVisitor visitor);
	}

	public class Agency : IEntity
	{
		public string Id { get; set; }
		[Required] public string Name { get; set; }
		[Required] public string Url { get; set; }

		public void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	public class FareRules : IEntity
	{
		[Required] public string FareId { get; set; }
		public string RouteId { get; set; }

		public void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	public class Frequencies : IEntity
	{
		[Required] public string TripId { get; set; }
		[Required] public string StartTime { get; set; }

		public void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
