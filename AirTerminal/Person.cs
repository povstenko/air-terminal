using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTerminal
{
	public class Person
	{
		public string firstName, secondName, thirdName;
		public int baggageThings;
		public double baggageWeight;
		public int flightNumber;
		public DateTime flightDate;
		public DateTime flightTime;
		public double flightDuration;
		public string destination;

		public Person()
		{
			this.firstName = "";
			this.secondName = "";
			this.thirdName = "";
			this.baggageThings = 0;
			this.baggageWeight = 0;
			this.flightNumber = 0;
			this.flightDuration = 0;
			this.destination = "";
		}
		public Person(string firstName, string secondName, string thirdName, int baggageThings, double baggageWeight, int flightNumber, DateTime flightDate, DateTime flightTime, double flightDuration, string destination)
		{
			this.firstName = firstName;
			this.secondName = secondName;
			this.thirdName = thirdName;
			this.baggageThings = baggageThings;
			this.baggageWeight = baggageWeight;
			this.flightNumber = flightNumber;
			this.flightDate = flightDate;
			this.flightTime = flightTime;
			this.flightDuration = flightDuration;
			this.destination = destination;
		}

		public override string ToString()
		{
			return $"#{flightNumber} ({destination})    |    {firstName} {secondName} {thirdName}    |    Baggage - {baggageThings} thing(s) ({baggageWeight} kg)    |    {flightDate.ToLongDateString()} {flightTime.ToShortTimeString()} ({flightDuration} h)";
		}
	}
}
