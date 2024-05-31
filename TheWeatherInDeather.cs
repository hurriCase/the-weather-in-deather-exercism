using System;
using System.Collections.Generic;

public class WeatherStation
{
    private Reading reading;
    private List<DateTime> recordDates = new List<DateTime>();
    private List<decimal> temperatures = new List<decimal>();

    public void AcceptReading(Reading reading)
    {
        this.reading = reading;
        recordDates.Add(DateTime.Now);
        temperatures.Add(reading.Temperature);
    }

    public void ClearAll()
    {
        reading = new Reading();
        recordDates.Clear();
        temperatures.Clear();
    }

    public decimal LatestTemperature => reading.Temperature;

    public decimal LatestPressure => reading.Pressure;

    public decimal LatestRainfall => reading.Rainfall;

    public bool HasHistory => recordDates.Count > 1 ? true : false;

    public Outlook ShortTermOutlook =>
        reading.Equals(new Reading()) ? throw new ArgumentException() :
        reading.Pressure < 10m && reading.Temperature < 30m ? Outlook.Cool :
        reading.Temperature > 50 ? Outlook.Good : Outlook.Warm;

    public Outlook LongTermOutlook =>
        reading.WindDirection == WindDirection.Southerly || reading.WindDirection == WindDirection.Easterly && reading.Temperature > 20 ? Outlook.Good :
        reading.WindDirection == WindDirection.Northerly ? Outlook.Cool :
        reading.WindDirection == WindDirection.Easterly && reading.Temperature <= 20 ? Outlook.Warm :
        reading.WindDirection == WindDirection.Westerly ? Outlook.Rainy : throw new ArgumentException();

    public State RunSelfTest() => reading.Equals(new Reading()) ? State.Bad : State.Good;
}

/*** Please do not modify this struct ***/
public struct Reading
{
    public decimal Temperature { get; }
    public decimal Pressure { get; }
    public decimal Rainfall { get; }
    public WindDirection WindDirection { get; }

    public Reading(decimal temperature, decimal pressure,
        decimal rainfall, WindDirection windDirection)
    {
        Temperature = temperature;
        Pressure = pressure;
        Rainfall = rainfall;
        WindDirection = windDirection;
    }
}

/*** Please do not modify this enum ***/
public enum State
{
    Good,
    Bad
}

/*** Please do not modify this enum ***/
public enum Outlook
{
    Cool,
    Rainy,
    Warm,
    Good
}

/*** Please do not modify this enum ***/
public enum WindDirection
{
    Unknown, // default
    Northerly,
    Easterly,
    Southerly,
    Westerly
}