using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Pluralsight.DesignPatterns.Visitor
{
	public class Program
	{
		public static void Main(string[] args)
		{

		}
	}

	public class ValidateVisitor : IVisitor
	{
		public void Visit(FareRules fareRules)
		{
			
		}

		public void Visit(Frequencies frequencies)
		{
			
		}

		public void Visit(Agency agency)
		{
			
		}
	}

	public interface IVisitor
	{
		void Visit(Agency agency);
		void Visit(FareRules fareRules);
		void Visit(Frequencies frequencies);
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
