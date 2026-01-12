using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a coffee shop establishment registered in the system.
/// </summary>
public class CoffeeShop : BaseEntity
{
    public string Name { get; private set; }
    public string GooglePlaceId { get; private set; }
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public bool IsPremium { get; private set; }

    /// <summary>
    /// Professional constructor for CoffeeShop.
    /// </summary>
    /// <param name="name">The commercial name of the coffee shop.</param>
    /// <param name="googlePlaceId">The unique ID from Google Places API.</param>
    /// <param name="address">The physical address.</param>
    /// <param name="latitude">Geographic latitude.</param>
    /// <param name="longitude">Geographic longitude.</param>
    public CoffeeShop(string name, string googlePlaceId, string address, double latitude, double longitude)
    {
        Name = name;
        GooglePlaceId = googlePlaceId;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        IsPremium = false;
    }

    /// <summary>
    /// Updates the premium status of the establishment.
    /// </summary>
    /// <param name="status">The new premium status to set.</param>
    public void SetPremiumStatus(bool status)
    {
        IsPremium = status;
    }
}