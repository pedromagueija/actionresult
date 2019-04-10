# ActionResult
Simple object to encapsulate return values, success notification and errors.

Example:

```csharp
private readonly ActionError licensePlateExistsInFleetError = new ActionError("LicensePlateAlreadyExists",
    "This license plate already exists in the fleet.");
    
private readonly ActionError vinNumberExistsInFleetError =
    new ActionError("VinNumberAlreadyExists", "This VIN already exists in the fleet.");

public ActionResult<bool> Add(Vehicle vehicle)
{
    var result = new ActionResult<bool>();

    if (vehicles.Contains(vehicle))
    {
        return result.Succeed(true);
    }

    if (vehicles.Any(c => c.LicensePlate == vehicle.LicensePlate))
    {
        result.Fail(licensePlateExistsInFleetError);
    }

    if (vehicles.Any(c => c.VinNumber == vehicle.VinNumber))
    {
        result.Fail(vinNumberExistsInFleetError);
    }

    if (result.Success)
    {
        vehicles.Add(vehicle);
    }

    return result;
}
```
